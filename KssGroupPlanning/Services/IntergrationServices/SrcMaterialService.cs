using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using KssGroupPlanning.Entities.Src;
using KssGroupPlanning.Interfaces.EntityInterfaces.Src;
using System.IO.Compression;

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
    public async Task InsertMaterials(DateOnly date,bool isArchive)
    {
        //string currentFile = GetFileName();
        string currentFile = "./csv/Materials " + date.ToString("dd-MM-yyyy").Replace('-','.') + ".csv";
        if (isArchive)
        {
            currentFile = "./csv/archive/Materials " + date.ToString("dd-MM-yyyy").Replace('-', '.') + ".csv";
        }
        await FixFile(currentFile);
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
        await _srcMaterialRepository.RemoveDuplicates();
    }
    public async Task TruncateTable()
    {
        await _srcMaterialRepository.TruncateTable();
    }
    public async Task FixFile(string filepath)
    {
        string text = "";
        using (var streamReader = new StreamReader(filepath))
        {
            string? line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                if (line.Count(c => c == ';') != 10)
                {
                    _logger.LogInformation($"{line.Substring(0, line.Length - 2)}");
                    text += line;
                }
                else
                {
                    text += line + '\r' + '\n';
                }
            }
        }
        File.Create(filepath).Dispose();
        using (var streamWriter = new StreamWriter(filepath))
        {
            streamWriter.Write(text);
        }
    }
}