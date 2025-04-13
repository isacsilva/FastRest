using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Server
{
    public class PeerClient
    {
        public static string EnviarStatusPedido(OrderStatusUpdateDTO pedido, string ipServidor = "127.0.0.1", int porta = 5050)
        {
            try
            {
                using TcpClient cliente = new TcpClient(ipServidor, porta);
                using NetworkStream stream = cliente.GetStream();

                // Serializa o objeto em JSON
                string json = JsonSerializer.Serialize(pedido);
                byte[] dados = Encoding.UTF8.GetBytes(json);

                // Envia o JSON para o servidor
                stream.Write(dados, 0, dados.Length);

                // Recebe a resposta
                byte[] buffer = new byte[1024];
                int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                string resposta = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                return resposta;
            }
            catch (Exception e)
            {
                Console.WriteLine("[PeerClient] Erro ao enviar pedido via socket: " + e.Message);
                return "Erro na comunicação com o servidor.";
            }
        }
    }
}