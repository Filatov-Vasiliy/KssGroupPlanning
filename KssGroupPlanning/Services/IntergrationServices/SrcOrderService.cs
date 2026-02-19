using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace KssGroupPlanning.Services;


public class SrcOrderService
{
    private readonly ISrcOrderRepository _srcOrderRepository;
    private readonly ILogger<SrcOrderService> _logger;
    public SrcOrderService(ISrcOrderRepository srcOrderRepository, ILogger<SrcOrderService> logger)
    {
        _srcOrderRepository = srcOrderRepository;
        _logger = logger;
    }
    public async Task<List<SrcOrderEntity>> GetAll()
    {

        return await _srcOrderRepository.GetSrcOrders();
    }
    private string GetFileName() 
    {
        DateTime date = DateTime.Now;
        return ".csv/Order " + date.ToString("dd-MM-yyyy")+".csv";
    }
    public async Task InsertOrders()
    {
        //string currentFile = GetFileName();
        string currentFile = "./csv/Order 15.12.2025.csv";
        _logger.LogWarning("Название файла! = "+currentFile);
        var entities = new List<SrcOrderEntity>();
        List<string> badRecord = new List<string>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", BadDataFound = context => badRecord.Add(context.RawRecord) };
        using (var streamReader = new StreamReader(currentFile))
        {
            using (var csvReader = new CsvReader(streamReader, config))
            {
                var records = csvReader.GetRecords<SrcOrderEntity>().ToList();
                entities.AddRange(records);
            }
        }
        foreach (var entity in entities)
        {
            entity.Id = Guid.NewGuid();
        }
        await _srcOrderRepository.Add(entities);
    }
    public async Task RemoveDuplicates()
    {
        _logger.LogInformation("Удаляем дупликаты по заказам полученые из csv");
        await _srcOrderRepository.RemoveDuplicates();
    }
}