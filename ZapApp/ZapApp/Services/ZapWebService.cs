using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace ZapApp.Services
{
    public class ZapWebService
    {
        private static HubConnection _connection { get; set; }

        private static ZapWebService _instance;

        private ZapWebService()
        {
        }

        public static ZapWebService GetInstance()
        {
            if(_connection == null)
            {
                _connection = new HubConnectionBuilder().WithUrl("/ZapWebHub").Build();
            }
            if(_connection.State == HubConnectionState.Disconnected)
            {
                _connection.StartAsync();
            }
            _connection.Closed += async (error) =>
            {
                await Task.Delay(5000);
                await _connection.StartAsync();
            };

            if (_instance == null)
            {
                _instance = new ZapWebService();
            }

            return _instance;
        }
    }
}
