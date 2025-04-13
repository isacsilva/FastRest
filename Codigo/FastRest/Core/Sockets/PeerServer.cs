using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Core.DTO;

namespace Core.Server
{
    public class PeerServer
    {
        public static void Main()
        {

            int porta = 5000;
            TcpListener servidor = new TcpListener(IPAddress.Any, porta);
            servidor.Start();
            Console.WriteLine("[Servidor] Iniciado na porta " + porta);

            while (true)
            {
                TcpClient cliente = servidor.AcceptTcpClient();
                Console.WriteLine("[Servidor] Novo cliente conectado.");

                Thread t = new Thread(() => TratarCliente(cliente));
                t.Start();
            }
        }

        static void TratarCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                string jsonRecebido = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                Console.WriteLine($"[Servidor] Dados recebidos: {jsonRecebido}");

                var pedido = JsonSerializer.Deserialize<OrderStatusUpdateDTO>(jsonRecebido);

                if (pedido != null)
                {
                    AtualizarStatusNoBanco(pedido.IdPedido, pedido.Status);

                    string resposta = $"Status do pedido #{pedido.IdPedido} atualizado para '{pedido.Status}'";
                    byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);
                    stream.Write(respostaBytes, 0, respostaBytes.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[Servidor] Erro: " + e.Message);
                string erro = "Erro ao processar solicitação.";
                byte[] erroBytes = Encoding.UTF8.GetBytes(erro);
                stream.Write(erroBytes, 0, erroBytes.Length);
            }

            cliente.Close();
        }

        static void AtualizarStatusNoBanco(int idPedido, string novoStatus)
        {
            try
            {
                using (var context = new FastRestContext())
                {
                    var service = new OrdertableService(context);
                    service.AtualizarStatus(idPedido, novoStatus);
                    Console.WriteLine($"[Servidor] Pedido #{idPedido} atualizado com sucesso para: {novoStatus}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Servidor] Erro ao atualizar o pedido no banco: {ex.Message}");
            }
        }
    }
}