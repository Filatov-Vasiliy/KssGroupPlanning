using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using KssGroupPlanning.Entities.Src;
using KssGroupPlanning.Interfaces.EntityInterfaces.Src;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    public async Task InsertProducts(DateOnly date, bool isArchive)
    {
        //string currentFile = GetFileName();
        //string currentFile = "./csv/Product 15.12.2025.csv";
        string currentFile = "./csv/Product " + date.ToString("dd-MM-yyyy").Replace('-', '.') + ".csv";
        if (isArchive)
        {
            currentFile = "./csv/archive/Product " + date.ToString("dd-MM-yyyy").Replace('-', '.') + ".csv";
        }
        await FixFile(currentFile);

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
        await _srcProductRepository.RemoveDuplicates();
    }
    public async Task TruncateTable()
    {
        await _srcProductRepository.TruncateTable();
    }
    public async Task FixFile(string filepath)
    {
        string text = "";
        bool next = false;
        using (var streamReader = new StreamReader(filepath))
        {
            string? line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                if (next) 
                {
                    text += line + '\r' + '\n';
                    continue;
                }
                if (line.Count(c => c == ';') != 9)
                {
                    _logger.LogInformation($"{line.Substring(0, line.Length - 2)}");
                    text += line;
                    next = true;
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