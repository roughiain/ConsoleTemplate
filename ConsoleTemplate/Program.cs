Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddSimpleConsole(options =>
        {
            // https://learn.microsoft.com/en-us/dotnet/core/extensions/console-log-formatter
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "HH:mm:ss ";
        });
        // Add any 3rd party loggers like NLog or Serilog
    })
    .ConfigureAppConfiguration((hostingContext, builder) =>
    {
        builder
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true,
                reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Main Program
        services.AddHostedService<HostedService>();

        // Add Context

        // var connectionString = hostContext.Configuration.GetConnectionString("Default") ??
        //                        throw new InvalidOperationException("Connection string 'Default' not found.");
        // services.AddDbContext<Context>(options =>
        // {
        //     options.UseSqlServer(connectionString);
        // });

        // https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-in-dotnet-6/

        services.AddOptions<ExampleSettings>()
            .BindConfiguration(ExampleSettings.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // Explicitly register the settings object by delegating to the IOptions object
        services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<ExampleSettings>>().Value);

    })
    .RunConsoleAsync();
