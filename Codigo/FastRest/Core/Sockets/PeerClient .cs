using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Server
{
    public class PeerClient
    {
        static void Main()
        {
            string ipServidor = "127.0.0.1"; // IP do servidor (localhost)
            int porta = 5000; // Porta usada na comunicação

            try
            {
                // Cria uma conexão com o servidor
                TcpClient cliente = new TcpClient(ipServidor, porta);
                NetworkStream stream = cliente.GetStream();

                // Coleta dados do usuário (simula envio do terminal)
                Console.Write("Digite o ID do pedido: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Digite o novo status (Pendente, Em Preparo, Finalizado): ");
                string status = Console.ReadLine();

                // Cria o objeto com as informações do pedido
                PedidoStatusUpdate pedido = new PedidoStatusUpdate
                {
                    IdPedido = id,
                    Status = status
                };

                // Converte o objeto para JSON
                string json = JsonSerializer.Serialize(pedido);
                byte[] dados = Encoding.UTF8.GetBytes(json);

                // Envia o JSON para o servidor
                stream.Write(dados, 0, dados.Length);

                // Espera e lê a resposta do servidor
                byte[] buffer = new byte[1024];
                int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                string resposta = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                // Exibe resposta no console
                Console.WriteLine("[Cliente] Resposta do servidor: " + resposta);

                // Fecha a conexão
                stream.Close();
                cliente.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("[Cliente] Erro: " + e.Message);
            }
        }
    }

}
