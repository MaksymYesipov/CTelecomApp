using CTelecomApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTelecomApp.Controllers
{
    public class AdminController : Controller
    {
        telecom_dbContext dbContext = new telecom_dbContext();

        public ActionResult Orders()
        {
            IEnumerable<Orders> Orders = dbContext.Orders.OrderBy(o => -o.Id).ToList();
            OrdersBean orders = new OrdersBean(Orders);
            List<string> statuses = new List<string>();
            List<string> packs = new List<string>();
            foreach (Orders order in Orders)
            {
                statuses.Add(dbContext.OrderStatus.ToList().Where(s => s.Id == order.StatusId).First().Name);
                packs.Add(dbContext.StarterPacks.ToList().Where(p => p.PackId == order.PackId).First().PackName);
            }
            orders.Statuses = statuses;
            orders.Packages = packs;
            return View(orders);
        }

        public ActionResult ProcessOrder(int orderId, int RequiredStatus)
        {
            dbContext.Orders.Where(o => o.Id == orderId).First().StatusId = RequiredStatus;
            dbContext.SaveChanges();
            return Redirect("/Admin/Orders");
        }
    }
}
