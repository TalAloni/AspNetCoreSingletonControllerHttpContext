using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace AspNetCoreSingletonControllerHttpContext
{
    /// <summary>
    /// Represents a Kestrel-based web host.
    /// </summary>
    public class WebHost : IDisposable
    {
        private readonly IPAddress m_hostAddress;
        private readonly int m_port;
        private readonly IStartup m_startupConfiguration;

        private IWebHost m_webHost;

        public WebHost(
            IPAddress hostAddress,
            int port,
            IStartup startupConfiguration)
        {
            m_hostAddress = hostAddress;
            m_port = port;
            m_startupConfiguration = startupConfiguration;

            m_webHost = CreateWebHost();
        }

        /// <summary>
        /// Start the host.
        /// </summary>
        public void Start()
        {
            m_webHost.Start();
        }

        /// <summary>
        /// Stop the host.
        /// </summary>
        public void Stop()
        {
            m_webHost.StopAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Dispose the host.
        /// </summary>
        public void Dispose()
        {
            m_webHost?.Dispose();
            m_webHost = null;
        }

        private IWebHost CreateWebHost()
        {
            return CreateWebHostBuilder()
                .ConfigureServices(services => m_startupConfiguration.ConfigureServices(services))
                .Configure(m_startupConfiguration.Configure)
                .Build();
        }

        private IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .UseKestrel(options =>
                {
                    options.Listen(m_hostAddress, m_port);
                });
        }
    }
}