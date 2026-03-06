using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using KssGroupPlanning.Entities.Src;
using KssGroupPlanning.Interfaces.EntityInterfaces.Src;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using System.IO;

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
    public async Task InsertOrders(DateOnly date, bool isArchive)
    {
        //string currentFile = GetFileName();
        //string currentFile = "./csv/Order 15.12.2025.csv";
        string currentFile = "./csv/Order " + date.ToString("dd-MM-yyyy").Replace('-', '.') + ".csv";
        if (isArchive) 
        {
            currentFile = "./csv/archive/Order " + date.ToString("dd-MM-yyyy").Replace('-', '.') + ".csv";
        }
        var entities = new List<SrcOrderEntity>();
        List<string> badRecord = new List<string>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", BadDataFound = context => badRecord.Add(context.RawRecord) };
        await FixFile(currentFile);
        using (var streamReader = new StreamReader(currentFile))
        {
            using (var csvReader = new CsvReader(streamReader, config))
            {
                try
                {
                    var records = csvReader.GetRecords<SrcOrderEntity>().ToList();
                    entities.AddRange(records);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"{ex.Message}");
                    string line;
                    bool next;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        /*
                        if (next) 
                        { 
                            line.
                        }
                        */
                        if (line.Count(c => c == ';') != 13) 
                        { 
                            next = true;
                        }
                    }
                }
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
        await _srcOrderRepository.RemoveDuplicates();
    }
    public async Task TruncateTable()
    {
        await _srcOrderRepository.TruncateTable();
    }
    public async Task FixFile(string filepath)
    {
        string text = "";
        using (var streamReader = new StreamReader(filepath))
        {
            string? line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                if (line.Count(c => c == ';') != 13)
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