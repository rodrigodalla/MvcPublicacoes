using MvcPublicacoes.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPublicacoes.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Publicacoes");
        }

        public ActionResult Sobre()
        {
            ViewBag.Disciplina = "EES14 - Engenharia de Software para Aplicações Web";
            ViewBag.Objetivo = "Desenvolver uma aplicação Web de acordo com a modelagem/projeto desenvolvido no trabalho I";

            return View();
        }

        public ActionResult Contato()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
