using API_Prctice_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using API_Prctice_6.Models.DTO;
using System.IO;
using System.Web;

namespace API_Prctice_6.Controllers
{
    [Authorize(Roles ="admin,user")]
    public class OrdersController : ApiController
    {
        private readonly OrderDBContext db = new OrderDBContext();

        [HttpGet]
        public IHttpActionResult GetOrder()
        {
            IEnumerable<Order> list = db.Orders.Include(i=>i.OrderItems).ToList();
            return Ok(list);
        }

        public IHttpActionResult GetOrderById(int id)
        {
            Order byId =db.Orders.Include(i=>i.OrderItems).Where(o=>o.OrderId == id).FirstOrDefault();
            return Ok(byId);
        }

        public IHttpActionResult DeleteOrder(int id)
        {
            Order dltOrder = db.Orders.Find(id);
            var items = db.OrderItems.Where(o => o.OrderId == id).ToList();
            db.OrderItems.RemoveRange(items);
            db.Orders.Remove(dltOrder);
            db.SaveChanges();
            return Ok("deleted");
        }

        [HttpPost]

        public IHttpActionResult PostOrder(OrderRequest request)
        {
            if (request == null || request.Order == null)
                return BadRequest("Order Missing");

            var obj = request.Order;

            if(request.ImageFile  != null || request.ImageFile.Length > 0)
            {
                obj.ImageUrl = GetImageUrl(request.ImageFile);
            }
            db.Orders.Add(obj);
            db.SaveChanges();
            return Ok("Saved");
        }

        private string GetImageUrl(byte[] imageFile)
        {
            string imageUrl = "";
            if(imageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string path = Path.Combine("/images/",fileName);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(path), imageFile);
                imageUrl = path;
            }
            return imageUrl;
        }

        [HttpPut]
        public IHttpActionResult PutOrder(int id,OrderRequest request)
        {
            if (request == null || request.Order == null)
                return BadRequest();

            if (id != request.Order.OrderId)
                return BadRequest("Id Mismatch");

            Order existOrder = db.Orders.Include(i => i.OrderItems).FirstOrDefault(o => o.OrderId == id);

            if(existOrder == null)
                return NotFound();

            if(request.ImageFile != null && request.ImageFile.Length>0)
            {
                existOrder.ImageUrl = GetImageUrl(request.ImageFile);
            }

            existOrder.OrderNo = request.Order.OrderNo;
            existOrder.CustomerName = request.Order.CustomerName;
            existOrder.IsPaid = request.Order.IsPaid;

            var existItems = db.OrderItems.Where(o=>o.OrderId ==id).ToList();
            db.OrderItems.RemoveRange(existItems);

            if(request.Order.OrderItems != null)
            {
                foreach (var item in request.Order.OrderItems)
                {
                    db.OrderItems.Add(new OrderItem
                    {
                        MenuItemId = item.MenuItemId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        OrderId = item.OrderId,
                    });
                }
               
            }
            db.SaveChanges();
            return Ok("Updated");
        }
    }
}
