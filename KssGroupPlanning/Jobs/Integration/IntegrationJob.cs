using KssGroupPlanning.Services;
using KssGroupPlanning.Services.DQServices;
using KssGroupPlanning.Services.IntergrationServices;
using Quartz;

namespace KssGroupPlanning.Jobs.Integration
{
    public class IntegrationJob : IJob
    {
        private readonly IntegrationService _integrationService;
        private readonly SrcOrderService _srcOrderService;
        private readonly SrcProductService _srcProductService;
        private readonly SrcMaterialService _srcMaterialService;
        private readonly LoadSrcService _loadSrcService;

        private readonly ILogger<IntegrationJob> _logger;

        public IntegrationJob(LoadSrcService loadSrcService,IntegrationService integrationService,SrcOrderService srcOrderService, SrcProductService srcProductService,SrcMaterialService srcMaterialService, ILogger<IntegrationJob> logger)
        {
            _integrationService = integrationService;
            _srcOrderService = srcOrderService;
            _srcProductService = srcProductService;
            _srcMaterialService = srcMaterialService;
            _loadSrcService = loadSrcService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Очищаем Src слой", DateTime.Now);
                await _srcOrderService.TruncateTable();
                await _srcProductService.TruncateTable();
                await _srcMaterialService.TruncateTable();
                _logger.LogInformation("Очистка Src слоя успешно завершена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении очистки Src слоя");
            }
            try
            {
                _logger.LogInformation("Заполняем Src слой новыми данными", DateTime.Now);

                await _srcOrderService.InsertOrders(DateOnly.Parse("15.12.2025"), false);
                await _srcProductService.InsertProducts(DateOnly.Parse("15.12.2025"), false);
                await _srcMaterialService.InsertMaterials(DateOnly.Parse("15.12.2025"), false);

                //await _loadSrcService.InsertFiles();
                _logger.LogInformation("Заполнение Src слоя успешно завершено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении очистки Src слоя");
            }

            try
            {
                _logger.LogInformation("Удаляем дубликаты", DateTime.Now);
                await _srcOrderService.RemoveDuplicates();
                await _srcProductService.RemoveDuplicates();
                await _srcMaterialService.RemoveDuplicates(); 
                _logger.LogInformation("Удаление дубликатов успешно завершено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении удалений дубликатов");
            }
            try
            {
                _logger.LogInformation("Запуск IntegrationService.LoadSrcToMain() в {time}", DateTime.Now);
                await _integrationService.LoadSrcToMain();
                _logger.LogInformation("IntegrationService.LoadSrcToMain() успешно завершен");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении IntegrationService.LoadSrcToMain()");
            }
        }
    }
}