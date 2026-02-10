using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace KssGroupPlanning.Services;


public class SrcProductService
{
    private readonly ISrcProductRepository _srcProductRepository;
    private readonly ILogger<SrcProductService> _logger;
    public SrcProductService(ISrcProductRepository srcProductRepository, ILogger<SrcProductService> logger)
    {
        _srcProductRepository = srcProductRepository;
        _logger = logger;
    }
    public async Task<List<SrcProductEntity>> GetAll()
    {

        return await _srcProductRepository.GetSrcProducts();
    }
    private string GetFileName()
    {
        DateTime date = DateTime.Now;
        return ".csv/Product " + date.ToString("dd-MM-yyyy") + ".csv";
    }
    public async Task InsertProducts()
    {
        //string currentFile = GetFileName();
        string currentFile = "./csv/Product 15.12.2025.csv";
        _logger.LogWarning("Название файла! = " + currentFile);
        var entities = new List<SrcProductEntity>();
        List<string> badRecord = new List<string>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", BadDataFound = context => badRecord.Add(context.RawRecord) };
        using (var streamReader = new StreamReader(currentFile))
        {
            using (var csvReader = new CsvReader(streamReader, config))
            {
                var records = csvReader.GetRecords<SrcProductEntity>().ToList();
                entities.AddRange(records);
            }
        }
        foreach (var entity in entities)
        {
            entity.Id = Guid.NewGuid();
        }
        await _srcProductRepository.Add(entities);
    }
    public async Task RemoveDuplicates() 
    {
        _logger.LogInformation("Удаляем дупликаты по продуктам полученые из csv");
        await _srcProductRepository.RemoveDuplicates();
    }
}