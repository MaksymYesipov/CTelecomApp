using CTelecomApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTelecomApp.Controllers
{
    public class HomeController : Controller
    {
        private telecom_dbContext DbContext = new telecom_dbContext();

        public ActionResult Index()
        {
            ViewBag.Title = "SP";
            ViewBag.Packs = DbContext.StarterPacks;

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public ActionResult Tariffs()
        {
            IEnumerable<Tariffs> tariffs = DbContext.Tariffs.ToList();
            return View(tariffs);
        }

        public ActionResult Services()
        {
            IEnumerable<Services> services = DbContext.Services.ToList();
            return View(services);
        }

        public ActionResult Order(int id)
        {
            if (Session["userId"] != null)
            {
                Users user = DbContext.Users.ToList().Where(u => u.IdCard.Equals(Session["userId"])).First();
                SimCards card = DbContext.SimCards.Where(c => c.CustomerId.Equals(user.IdCard)).First();
                Orders NewOrder = new Orders() { ClientName = user.Name, ClientPhone = card.PhoneNumber, PackId = id };
                DbContext.Orders.Add(NewOrder);
                DbContext.SaveChanges();

                ViewBag.OrderMessage = "Thenk you for your order, " + user.Name + ", please wait for the callback on " + card.PhoneNumber;
                ViewBag.Packs = DbContext.StarterPacks;
                return View("Index");
            }
            return RedirectToAction("OrderPage", new { id });
        }

        public ActionResult EnableService(int id)
        {
            Users user = DbContext.Users.ToList().Where(u => u.IdCard.Equals(Session["userId"])).First();
            SimCards card = DbContext.SimCards.Where(c => c.CustomerId.Equals(user.IdCard)).First();
            DbContext.SimServices.Add(new SimServices() { ActivationDate = DateTime.Now, ServiceId = id, SimId = card.SimId, StatusCode = 1});
            DbContext.SaveChanges();
            Services service = DbContext.Services.ToList().Where(s => s.ServiceId == id).First();
            ViewBag.Message = "You've been successfully enabled " + service.ServiceName + " for $" + service.Price;
            ViewBag.Status = "success";
            return View("Services", DbContext.Services.ToList());
        }

        public ActionResult DisableService(int id)
        {
            Users user = DbContext.Users.ToList().Where(u => u.IdCard.Equals(Session["userId"])).First();
            SimCards card = DbContext.SimCards.Where(c => c.CustomerId.Equals(user.IdCard)).First();
            DbContext.SimServices.Where(ss => ss.SimId.Equals(card.SimId) && ss.StatusCode == 1 && ss.ServiceId == id).First().StatusCode = 0;
            DbContext.SaveChanges();
            return Redirect("/Home/Cabinet");
        }

        public ActionResult ChangeTariff(int id)
        {
            Users user = DbContext.Users.ToList().Where(u => u.IdCard.Equals(Session["userId"])).First();
            Tariffs tarif = DbContext.Tariffs.ToList().Where(t => t.TariffId == id).First();
            if (DbContext.SimCards.ToList().Where(c => c.CustomerId.Equals(user.IdCard)).First().TariffId != id)
            {
                DbContext.SimCards.Where(c => c.CustomerId.Equals(user.IdCard)).First().TariffId = id;
                double newBalance = user.Balance - tarif.Price;
                user.Balance = newBalance;
                DbContext.SaveChanges();
                Session["balance"] = newBalance;
                ViewBag.Message = "You're succesfully changed your tariff to " + tarif.TariffName;
                ViewBag.Status = "success";
                return View("Tariffs", DbContext.Tariffs.ToList());
            }
            ViewBag.Message = "There was an arror during changing a tariff, please try again later";
            ViewBag.Status = "danger";
            return View("Tariffs", DbContext.Tariffs.ToList());
        }

        [HttpPost]
        public ActionResult OrderAnon(int id, string Name, string Phone)
        {
            DbContext.Orders.Add(new Orders() { ClientName = Name, ClientPhone = Phone, PackId = id });
            DbContext.SaveChanges();
            ViewBag.OrderMessage = "Thank you for your order, " + Name + ", please wait for the callback on " + Phone;
            ViewBag.Packs = DbContext.StarterPacks;
            return View("Index");
       
        }

        public ActionResult OrderPage(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Cabinet()
        {
            Users user = DbContext.Users.ToList().Where(u => u.IdCard.Equals(Session["userId"])).First();
            SimCards card = DbContext.SimCards.Where(c => c.CustomerId.Equals(user.IdCard)).First();
            Tariffs tariff = DbContext.Tariffs.ToList().Where(t => t.TariffId == card.TariffId).First();
            ViewBag.Tariff = tariff;
            IEnumerable<SimServices> activations = DbContext.SimServices.ToList().Where(ss => ss.SimId.Equals(card.SimId) && ss.StatusCode == 1);
            List<Services> services = new List<Services>();
            foreach (SimServices activation in activations)
            {
                services.Add(DbContext.Services.ToList().Where(s => s.ServiceId == activation.ServiceId).First());
            }
            return View(services);
        }
    }
}