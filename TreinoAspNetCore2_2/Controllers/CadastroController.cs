using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TreinoAspNetCore2_2.Models;

namespace TreinoAspNetCore2_2.Controllers
{
    public class CadastroController : Controller
    {
        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id != null)
            {
                ViewBag.User = new ModelCadastro();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ModelCadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                cadastro.Inserir();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}
