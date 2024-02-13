using Kolokwium_apbd_s20414.Data;
using Kolokwium_apbd_s20414.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_apbd_s20414.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KolokwiumController : ControllerBase
    {
        private readonly KolokwiumContext _context;

        public KolokwiumController(KolokwiumContext context)
        {
            _context = context;
        }

        // Końcówka GET na adres api/clients/{clientId}/orders
        [HttpGet("clients/{clientId}/orders")]
        public IActionResult GetClientOrders(int clientId)
        {
            // Klient
            var client = _context.Clients.Find(clientId);
            if (client == null)
            {
                return NotFound("Klient o podanym ID nie został znaleziony.");
            }

            // Order
            var orders = _context.Orders
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .Include(o => o.Status)
                .Where(o => o.Client_ID == clientId)
                .ToList();

            // Zwracanie orderu
            var result = new List<object>();
            foreach (var order in orders)
            {
                var orderData = new
                {
                    OrderID = order.ID,
                    ClientsLastName = client.LastName,
                    CreatedAt = order.CreatedAt,
                    FulfilledAt = order.FulfilledAt,
                    Status = order.Status.Name,
                    Products = order.ProductOrders.Select(po => new
                    {
                        Name = po.Product.Name,
                        Price = po.Product.Price,
                        Amount = po.Amount
                    }).ToList()
                };
                result.Add(orderData);
            }

            return Ok(result);
        }

        // Końcówka POST na adres api/clients/{clientId}/orders
        [HttpPost("clients/{clientId}/orders")]
        public IActionResult AddOrderForClient(int clientId, [FromBody] OrderRequest orderRequest)
        {
            // Klient
            var client = _context.Clients.Find(clientId);
            if (client == null)
            {
                return NotFound("Klient o podanym ID nie został znaleziony.");
            }

            // Czy jest created
            var createdStatus = _context.Statuses.FirstOrDefault(s => s.Name == "Created");
            if (createdStatus == null)
            {
                return NotFound("Status 'Created' nie został znaleziony.");
            }

            // Walidacja danych
            if (orderRequest == null || orderRequest.CreatedAt == null)
            {
                return BadRequest("Nieprawidłowe dane zamówienia.");
            }

            // Nowy order
            var order = new Order
            {
                CreatedAt = orderRequest.CreatedAt.Value,
                FulfilledAt = orderRequest.FulfilledAt,
                Client_ID = clientId,
                Status_ID = createdStatus.ID
            };

            // Dodaj order do bazy
            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok();
        }
    }

    // Pomocnicza klasa do danych Orderu które przekazujemy przy tworzeniu nowego (w AddOrderForClient)
    public class OrderRequest
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? FulfilledAt { get; set; }
    }
}

