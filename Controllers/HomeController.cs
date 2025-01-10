using Peokutse.Models;
using System;
using System.Web.Mvc;
using System.Web.Helpers;

namespace Peokutse.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            // Приветствие в зависимости от времени суток
            ViewBag.Message = "Ootan sind minu poele! Palun tule!!!";
            int hour = DateTime.Now.Hour;
            if (hour >= 6 && hour < 12)
            {
                ViewBag.Greeting = "Tere hommikust!";  // Утро
            }
            else if (hour >= 12 && hour < 18)
            {
                ViewBag.Greeting = "Tere päevast!";  // День
            }
            else if (hour >= 18 && hour < 22)
            {
                ViewBag.Greeting = "Tere õhtust!";  // Вечер
            }
            else
            {
                ViewBag.Greeting = "Head ööd!";  // Ночь
            }

            return View();
        }

        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_Mail(guest);
            if (ModelState.IsValid)
            {
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }

        public void E_Mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "kirillsats30@gmail.com";
                WebMail.Password = "hnle xanj dbpk rfdk ";
                WebMail.From = "kirillsats30@gmail.com";
                WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " Vastus " + ((guest.WillAttend ?? false) ?
                    "tuleb poele " : "ei tule poele"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Mul on kuhju! Ei saa kirja saada!!!";
            }
        }
    }
}