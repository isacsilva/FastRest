using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Core.DTO;
using Core.Service;
using Microsoft.Extensions.DependencyInjection;
namespace Core.Server
{
    public class PeerServer
    {
        private readonly IServiceProvider _serviceProvider;
        private TcpListener _servidor;
        private static bool _emExecucao = false;
        //private readonly IOrdertableService _orderService; 
        //private TcpListener _servidor;
        //private static bool _emExecucao = false;

        public PeerServer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Start(int porta = 5050)
        {
            if (_emExecucao) return;

            _servidor = new TcpListener(IPAddress.Any, porta);
            _servidor.Start();
            _emExecucao = true;

            Console.WriteLine($"[Servidor] Iniciado na porta {porta}");

            Thread threadServidor = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        var cliente = _servidor.AcceptTcpClient();
                        Thread t = new Thread(() => TratarCliente(cliente));
                        t.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("[Servidor] Erro ao aceitar cliente: " + ex.Message);
                    }
                }
            });

            threadServidor.IsBackground = true;
            threadServidor.Start();
        }

        private void TratarCliente(TcpClient cliente)
        {
            using var stream = cliente.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                string jsonRecebido = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                Console.WriteLine($"[Servidor] Dados recebidos: {jsonRecebido}");

                var orderUpdate = JsonSerializer.Deserialize<OrderStatusUpdateDTO>(jsonRecebido);

                if (orderUpdate != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var orderService = scope.ServiceProvider.GetRequiredService<IOrdertableService>();

                    orderService.AtualizarStatus(orderUpdate.IdPedido, orderUpdate.Status);
                    Console.WriteLine($"[Servidor] Pedido #{orderUpdate.IdPedido} atualizado para: {orderUpdate.Status}");

                    string resposta = $"Status do pedido #{orderUpdate.IdPedido} atualizado para '{orderUpdate.Status}'";
                    byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);
                    stream.Write(respostaBytes, 0, respostaBytes.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[Servidor] Erro: " + e.Message);
            }

            cliente.Close();
        }
    }
}