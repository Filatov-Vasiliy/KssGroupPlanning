using KssGroupPlanning.Interfaces.EntityInterfaces.Src;
using System;

namespace KssGroupPlanning.Services.IntergrationServices;

public class LoadSrcService
{
    private readonly SrcMaterialService _srcMaterialService;
    private readonly SrcProductService _srcProductService;
    private readonly SrcOrderService _srcOrderService;
    private readonly IntegrationService _integrationService;

    private readonly ILogger<LoadSrcService> _logger;
    public LoadSrcService(ILogger<LoadSrcService> logger,IntegrationService integrationService, SrcOrderService srcOrderService, SrcProductService srcProductService,SrcMaterialService srcMaterialService)
    {
        _logger = logger;
        _srcOrderService = srcOrderService;
        _srcProductService = srcProductService;
        _srcMaterialService = srcMaterialService;
        _integrationService = integrationService;
    }
    public async Task InsertFilesArchive()
    {
        DateOnly dateOnly = new DateOnly(); // установить в аргумент
        try
        {
            // Получить все файлы в указанной папке (без подпапок)
            string[] files = Directory.GetFiles("./csv/archive");
            HashSet<DateOnly> filenames = new HashSet<DateOnly>();
            foreach (string file in files)
            {
                filenames.Add(DateOnly.Parse(file.Split(' ')[1].Split(".csv")[0]));
                //Console.WriteLine(Path.GetFileName(file)); // Вывод имени файла
            }
            foreach (DateOnly filename in filenames.Order()) 
            {
                _logger.LogInformation($"Начинаем загружать файлы за {filename}");
                try
                {
                    await TruncateSrc();
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"Ошибка: {e.Message}");
                    continue;
                }

                await _srcOrderService.InsertOrders(filename,true);
                await _srcProductService.InsertProducts(filename, true);
                await _srcMaterialService.InsertMaterials(filename, true);

                await _srcOrderService.RemoveDuplicates();
                await _srcProductService.RemoveDuplicates();
                await _srcMaterialService.RemoveDuplicates();

                try
                {
                    await _integrationService.LoadSrcToMain();
                    _logger.LogInformation($"Интеграция за {filename} завершена успешно");
                }
                catch (Exception e)
                {
                    _logger.LogError($"Интеграция за {filename} завершена неудачей");
                    _logger.LogWarning($"Ошибка: {e.Message}");
                    continue;
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Ошибка: {e.Message}");
        }
    }
    public async Task InsertFiles()
    {
        DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now); // установить в аргумент
        try
        {
            _logger.LogError($"Запускаем инсерт {dateOnly.ToString()}");
            await _srcOrderService.InsertOrders(dateOnly,false);
            await _srcProductService.InsertProducts(dateOnly, false);
            await _srcMaterialService.InsertMaterials(dateOnly, false);
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Ошибка: {e.Message}");
        }
        _logger.LogError($"Удаляем дубликаты {dateOnly.ToString()}");
        await _srcOrderService.RemoveDuplicates();
        await _srcProductService.RemoveDuplicates();
        await _srcMaterialService.RemoveDuplicates();
    }
    public async Task TruncateSrc()
    {
        await _srcOrderService.TruncateTable();
        await _srcProductService.TruncateTable();
        await _srcMaterialService.TruncateTable();
    }
}
