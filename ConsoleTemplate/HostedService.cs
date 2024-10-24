namespace ConsoleTemplate;

public class HostedService : IHostedService
{
    private readonly ILogger<HostedService> _logger;
    private readonly ExampleSettings _exampleSettings;
   // private readonly DataContext _dataContext;

    public HostedService(
        ILogger<HostedService> logger,
        ExampleSettings exampleSettings) //, DataContext dataContext
    {
        _logger = logger;
        _exampleSettings = exampleSettings;
        // _dataContext = dataContext
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Run Migrations
        // await _dataContext.Database.MigrateAsync()

        // First arg is the DLL so we skip it.
        var arguments = Environment.GetCommandLineArgs()[1..];

        _logger.LogInformation("Starting with arguments: {arguments}",  string.Join(", ", arguments));
        _logger.LogInformation("Example Setting: {debug}", _exampleSettings.Debug);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
       _logger.LogInformation("Stopping");
       return Task.CompletedTask;
    }
}
