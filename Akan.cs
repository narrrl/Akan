using System;
using System.Net.Http;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lavalink4NET.Logging;
using Lavalink4NET.MemoryCache;
using Lavalink4NET;
using Lavalink4NET.DiscordNet;
using Akan.Services;

namespace Akan
{
    class Akan
    {
        public readonly static Color _color = new Color(224, 151, 193);
        private readonly IConfiguration _config;
        private DiscordSocketClient _client;
        private IServiceProvider _provider;
        private CommandHandler _commandHandler;

        static void Main(string[] args) 
            =>  new Akan().MainAsync().GetAwaiter().GetResult();
        

        public Akan()
        {
            // create the configuration
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");  

            // build the configuration and assign to _config          
            _config = _builder.Build();
        }

        public async Task MainAsync()
        {
            // call ConfigureServices to create the ServiceCollection/Provider for passing around the services
            using (var services = ConfigureServices())
            {
                // get the client and assign to client 
                // you get the services via GetRequiredService<T>
                var client = services.GetRequiredService<DiscordSocketClient>();
                var audio = services.GetRequiredService<IAudioService>();
                var commandHandler = services.GetRequiredService<CommandHandler>();
                _client = client;
                _provider = services;
                _commandHandler = commandHandler;
        

                // setup logging and the ready event
                client.Log += LogAsync;
                client.Ready += ReadyAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                // this is where we get the Token value from the configuration file, and start the bot
                await client.LoginAsync(TokenType.Bot, _config["Token"]);
                await client.StartAsync();
                await audio.InitializeAsync();
                await commandHandler.InitializeAsync();
                await Task.Delay(-1);
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [{_client.CurrentUser}]");
            return Task.CompletedTask;
        }

        // this method handles the ServiceCollection creation/configuration, and builds out the service provider we can call on later
        private ServiceProvider ConfigureServices()
        {
            // this returns a ServiceProvider that is used later to call for those services
            // we can add types we have access to here, hence adding the new using statement:
            // using csharpi.Services;
            // the config we build is also added, which comes in handy for setting the command prefix!
            return new ServiceCollection()
                .AddSingleton(_config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<HttpClient>()
                .AddSingleton<PictureService>()

                // Lavalink
                .AddSingleton<IAudioService, LavalinkNode>()
                .AddSingleton<IDiscordClientWrapper, DiscordClientWrapper>()
                .AddSingleton<ILogger, EventLogger>()

                .AddSingleton(new LavalinkNodeOptions
                {
                    RestUri = "http://localhost:8080/",
                    WebSocketUri = "ws://localhost:8080/",
                    Password = "youshallnotpass",
                    AllowResuming = true,
                    BufferSize = 1024 * 1024,
                    DisconnectOnStop = false,
                    ReconnectStrategy = ReconnectStrategies.DefaultStrategy,
                    DebugPayloads = false
                })

                // Request Caching for Lavalink
                .AddSingleton<ILavalinkCache, LavalinkCache>()
                .BuildServiceProvider();
        }
    }
}
