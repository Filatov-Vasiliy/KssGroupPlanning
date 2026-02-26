using KssGroupPlanning.Interfaces.EntityInterfaces;
using KssGroupPlanning.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace KssGroupPlanning.Services;


public class SrcMaterialService
{
    private readonly ISrcMaterialRepository _srcMaterialRepository;
    private readonly ILogger<SrcMaterialService> _logger;
    public SrcMaterialService(ISrcMaterialRepository srcMaterialRepository, ILogger<SrcMaterialService> logger)
    {
        _srcMaterialRepository = srcMaterialRepository;
        _logger = logger;
    }
    public async Task<List<SrcMaterialEntity>> GetAll()
    {

        return await _srcMaterialRepository.GetSrcMaterials();
    }
    private string GetFileName()
    {
        DateTime date = DateTime.Now;
        return ".csv/Materials " + date.ToString("dd-MM-yyyy") + ".csv";
    }
    public async Task InsertMaterials()
    {
        //string currentFile = GetFileName();
        string currentFile = "./csv/Materials 15.12.2025.csv";
        _logger.LogWarning("Название файла! = " + currentFile);
        var entities = new List<SrcMaterialEntity>();
        List<string> badRecord = new List<string>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", BadDataFound = context => badRecord.Add(context.RawRecord) };
        using (var streamReader = new StreamReader(currentFile))
        {
            using (var csvReader = new CsvReader(streamReader, config))
            {
                var records = csvReader.GetRecords<SrcMaterialEntity>().ToList();
                entities.AddRange(records);
            }
        }
        foreach (var entity in entities)
        {
            entity.Id = Guid.NewGuid();
        }
        await _srcMaterialRepository.Add(entities);
    }
    public async Task RemoveDuplicates()
    {
        _logger.LogInformation("Удаляем дупликаты по материалам полученые из csv");
        await _srcMaterialRepository.RemoveDuplicates();
    }
    public async Task TruncateTable()
    {
        _logger.LogInformation("Очищаем таблицу SrcMaterial");
        await _srcMaterialRepository.TruncateTable();
    }
}