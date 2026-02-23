using Quartz;

namespace KssGroupPlanning.Services.IntergrationServices
{
    public class IntegrationJob : IJob
    {
        private readonly IntegrationService _integrationService;
        private readonly SrcOrderService _srcOrderService;
        private readonly SrcProductService _srcProductService;
        private readonly SrcMaterialService _srcMaterialService;

        private readonly ILogger<IntegrationJob> _logger;

        public IntegrationJob(IntegrationService integrationService,SrcOrderService srcOrderService, SrcProductService srcProductService,SrcMaterialService srcMaterialService, ILogger<IntegrationJob> logger)
        {
            _integrationService = integrationService;
            _srcOrderService = srcOrderService;
            _srcProductService = srcProductService;
            _srcMaterialService = srcMaterialService;
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
                await _srcOrderService.InsertOrders();
                await _srcProductService.InsertProducts();
                await _srcMaterialService.InsertMaterials();
                _logger.LogInformation("Заполнение Src слоя успешно завершено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при выполнении очистки Src слоя");
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