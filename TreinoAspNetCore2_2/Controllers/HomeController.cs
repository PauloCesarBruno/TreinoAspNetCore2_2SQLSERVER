using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;// Importar para o uso do "SetString"
using Microsoft.AspNetCore.Mvc;
using TreinoAspNetCore2_2.Models;

namespace TreinoAspNetCore2_2.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login(int? Id) //Rotina para faser logout.
        {
            if (Id != null)
            {
                // Pega o parâmetro 0 que esta no JS do Layout e zera a string.
                if (Id == 0)
                {
                    HttpContext.Session.SetString("IdUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("NomeUsuarioLogado", string.Empty);
                }
            }
            return View();
        }
        //Final da realização do logout.

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            //Abaixo verifica se o estado do LoginModel é Valido.
            if (ModelState.IsValid)
            {
                /*Abaixo cria-se uma variável booleana que verifica se deu tudo certo no login,
                 * caso contrário no "else" é criada uma variavel temporária
                 * (TempData["ErrorLogin"] = "E-Mail ou Senha Incorreto(s), favor verificar!";) 
                 * que vai levar o aviso de erro para a View Login,cshtml.*/
                bool loginOK = login.ValidarLogin();
                if (loginOK)
                {
                    HttpContext.Session.SetString("IdUsuarioLogado", login.Id);
                    HttpContext.Session.SetString("NomeUsuarioLogado", login.Nome);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorLogin"] = "E-Mail ou Senha Incorreto(s), favor verificar!";
                }
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
