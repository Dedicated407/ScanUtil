using ScanUtil.Models;
using ServiceApplication.DI;

namespace ServiceApplication;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceLocator _serviceLocator = ServiceLocator.GetInstance();

    public Worker(ILogger<Worker> logger) => _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var operation = Console.ReadLine();

            if (operation.Contains("scan_util scan"))
            {
                var directory = string.Join(" ", operation.Split(' ').Skip(2)).Replace("\"", string.Empty);
                
                var request = new ScanRequestModel {Directory = directory};
                var scanModel = ScanUtil.Program.ScanDirectory(request.Directory);

                Console.WriteLine($"Scan task was created with ID: {request.Id}");
                
                await _serviceLocator.GetRepository().Save(new ScanResponseModel
                {
                    Id = request.Id,
                    Directory = request.Directory,
                    IsFinished = true,
                    ProcessedFiles = scanModel.ProcessedFiles,
                    JSDetects = scanModel.JSDetects,
                    RmRfDetects = scanModel.RmRfDetects,
                    RunDllDetects = scanModel.RunDllDetects,
                    Errors = scanModel.Errors,
                    ExecutionTime = scanModel.ExecutionTime
                });
            }

            if (operation.Contains("scan_util status"))
            {
                var id = Guid.Parse(string.Join("", operation.Split(' ').Skip(2)));
                
                var model = await _serviceLocator.GetRepository().Find(id);

                if (model.IsFinished)
                {
                    Console.WriteLine(model);
                }
                else
                {
                    Console.WriteLine("Scan task in progress, please wait");
                }
                
            }
        }
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scan service was started.\n\tPress <Enter> to exit...");
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scan service stopping.");
        return base.StopAsync(cancellationToken);
    }
}