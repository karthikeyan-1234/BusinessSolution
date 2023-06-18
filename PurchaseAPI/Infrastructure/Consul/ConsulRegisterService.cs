using Consul;

namespace PurchaseAPI.Infrastructure.Consul
{
    public class ConsulRegisterService : IConsulRegisterService
    {
        private IConsulClient _consulClient;
        private IConfiguration _configuration;

        public ConsulRegisterService(IConsulClient consulClient, IConfiguration configuration)
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
                Name = _configuration.GetSection("Consul").GetValue<string>("ServiceName"),
                Port = _configuration.GetSection("Consul").GetValue<int>("ServicePort"),
                ID = _configuration.GetSection("Consul").GetValue<string>("ServiceID"),
                Tags = new[] { _configuration.GetSection("Consul").GetValue<string>("ServiceName") } 
            };

            string serviceID = _configuration.GetSection("Consul").GetValue<string>("ServiceID");
            await _consulClient.Agent.ServiceDeregister(serviceID, cancellationToken);
            await _consulClient.Agent.ServiceRegister(serviceRegistration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            string serviceID = _configuration.GetSection("Consul").GetValue<string>("ServiceID");
            await _consulClient.Agent.ServiceDeregister(serviceID, cancellationToken);
        }
    }
}
