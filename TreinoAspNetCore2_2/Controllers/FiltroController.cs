using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreinoAspNetCore2_2.Models;

namespace TreinoAspNetCore2_2.Controllers
{
    public class FiltroController : Controller
    {               
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Filtro()
        {   //Renderiza e traz todos os Clientes
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(ClienteModel cliente)
        {       
            try
            {
                //Filtra o Cliente pelo CPF
                String cpf = cliente.CPF.ToString();
                ViewBag.ListaClientes = new ClienteModel().ListagemClientes(cpf);
            }
            catch
            {
                //
            }
            return View();                  
        }
    }
}
