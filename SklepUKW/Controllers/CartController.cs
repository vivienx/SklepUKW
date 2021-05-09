using SklepUKW.DAL;
using SklepUKW.Infrastructure;
using SklepUKW.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SklepUKW.Controllers
{
    public class CartController : Controller
    {
        //dostep do koszyka, bazy i sesje
        CartManager cartManager;
        FilmsContext db;
        ISessionManager session;

        //inicjalizacja pol w konstruktorze
        public CartController()
        {
            db = new FilmsContext();
            session = new SessionManager();
            cartManager = new CartManager(db, session); //uzupelnienie
        }
        // GET: Cart
        public ActionResult Index()
        {
            CartViewModel cvm = new CartViewModel()//wyswietlanie w koszyku tabeli, info dot. sztuk, cena
            {
                CartItems = cartManager.GetCartItems(),
                TotalPrice = cartManager.GetCartValue()
            };
            
            return View(cvm);
        }

        public ActionResult AddToCart(int id) //akcja wywolania koszyka. przekazanie filmu o danym id
        {
            cartManager.AddToCart(id); //do koszyka dodajemy film o danym id

            return RedirectToAction("Index"); //przekierowanie
        }

        public ActionResult RemoveFromCart(int id)
        {
            ItemRemoveViewModel model = new ItemRemoveViewModel()
            {
                ItemId = id,
                ItemQuantity = cartManager.RemoveFromCart(id),
                CartValue = cartManager.GetCartValue(),
                CartQuantity = cartManager.GetCartQuantity()
            };

            return Json(model);
        }

        public int GetCartQuantity()
        {
            return cartManager.GetCartQuantity();
        }
    }
}