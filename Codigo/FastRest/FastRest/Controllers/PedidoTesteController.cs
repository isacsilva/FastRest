using Core.DTO;
using Core.Server;
using Microsoft.AspNetCore.Mvc;

namespace FastRest.Controllers
{
    public class PedidoTesteController : Controller
    {
        [HttpGet]
        public IActionResult Enviar()
        {
            var dto = new OrderStatusUpdateDTO
            {
                IdPedido = 1,
                Status = "Em Preparo"
            };

            var resposta = PeerClient.EnviarStatusPedido(dto);

            return Content($"Resposta do servidor socket: {resposta}");
        }
    }
}