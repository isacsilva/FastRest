using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Core.DTO;
using Core.Service;

namespace Core.Server
{
    public class PeerServer
    {
        private static TcpListener _servidor;
        private static bool _emExecucao = false;

        public static void Start(int porta = 5050)
        {
            if (_emExecucao) return; // evita iniciar duas vezes

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
                        TcpClient cliente = _servidor.AcceptTcpClient();
                        Console.WriteLine("[Servidor] Novo cliente conectado.");

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

        private static void TratarCliente(TcpClient cliente)
        {
            using NetworkStream stream = cliente.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                string jsonRecebido = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                Console.WriteLine($"[Servidor] Dados recebidos: {jsonRecebido}");

                var orderUpdate = JsonSerializer.Deserialize<OrderStatusUpdateDTO>(jsonRecebido);

                if (orderUpdate != null)
                {
                    AtualizarStatus(orderUpdate.IdPedido, orderUpdate.Status);

                    string resposta = $"Status do pedido #{orderUpdate.IdPedido} atualizado para '{orderUpdate.Status}'";
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

        private static void AtualizarStatus(int idOrdertable, string newStatus)
        {
            try
            {
                using var context = new FastRestContext();

                var pedido = context.Ordertable.Find(idOrdertable);
                if (pedido != null)
                {
                    pedido.Status = newStatus;
                    context.SaveChanges();

                    Console.WriteLine($"[Servidor] Pedido #{idOrdertable} atualizado com sucesso para: {newStatus}");
                }
                else
                {
                    Console.WriteLine($"[Servidor] Pedido #{idOrdertable} não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Servidor] Erro ao atualizar o pedido no banco: {ex.Message}");
            }
        }
    }
}