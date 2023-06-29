using Consul;

using Microsoft.Extensions.Configuration;


namespace CommonLibrary.Infrastructure.Consul
{
    public class ConsulRegisterService : IConsulRegisterService
    {
        private IConsulClient _consulClient;
        public IConfiguration _configuration;

        public ConsulRegisterService(IConsulClient consulClient, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _consulClient = consulClient;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            var myUri = new Uri("https://localhost:7252");

            var serviceRegistration = new AgentServiceRegistration()
            {
                Address = myUri.Host,
                Name = _configuration.GetSection("Consul").GetSection("ServiceName").Value,
                Port = Convert.ToInt16(_configuration.GetSection("Consul").GetSection("ServicePort").Value),
                ID = _configuration.GetSection("Consul").GetSection("ServiceID").Value,
                Tags = new[] { _configuration.GetSection("Consul").GetSection("ServiceName").Value } 
            };

            string serviceID = _configuration.GetSection("Consul").GetSection("ServiceID").Value;
            await _consulClient.Agent.ServiceDeregister(serviceID, cancellationToken);
            await _consulClient.Agent.ServiceRegister(serviceRegistration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            string serviceID = _configuration.GetSection("Consul").GetSection("ServiceID").Value;
            await _consulClient.Agent.ServiceDeregister(serviceID, cancellationToken);
        }
    }
}
