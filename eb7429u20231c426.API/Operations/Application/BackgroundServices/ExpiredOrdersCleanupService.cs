using eb7429u20231c426.API.Assets.Domain.Repositories;
using eb7429u20231c426.API.Operations.Domain.Repositories;
using eb7429u20231c426.API.Shared.Domain.Repositories;

namespace eb7429u20231c426.API.Operations.Application.BackgroundServices;

public class ExpiredOrdersCleanupService : BackgroundService
{
    private readonly ILogger<ExpiredOrdersCleanupService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1); // Chequear cada hora

    public ExpiredOrdersCleanupService(
        ILogger<ExpiredOrdersCleanupService> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Expired Orders Cleanup Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanupExpiredOrdersAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cleaning up expired orders.");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }

        _logger.LogInformation("Expired Orders Cleanup Service is stopping.");
    }

    private async Task CleanupExpiredOrdersAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var ordersRepository = scope.ServiceProvider.GetRequiredService<IOrdersRepository>();
        var lockerRepository = scope.ServiceProvider.GetRequiredService<ILockerRepository>();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        // Encontrar todos los pedidos expirados
        var expiredOrders = await ordersRepository.FindExpiredOrdersAsync();

        foreach (var order in expiredOrders)
        {
            try
            {
                // Liberar el locker asociado
                var locker = await lockerRepository.FindByIdAsync(order.LockerId);
                if (locker != null && !locker.IsAvailable)
                {
                    locker.UpdateReleased();
                    lockerRepository.Update(locker);
                    _logger.LogInformation($"Locker {order.LockerId} released due to expired order {order.Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing expired order {order.Id}");
            }
        }

        if (expiredOrders.Any())
        {
            await unitOfWork.CompleteAsync();
            _logger.LogInformation($"Cleaned up {expiredOrders.Count()} expired orders.");
        }
    }
}