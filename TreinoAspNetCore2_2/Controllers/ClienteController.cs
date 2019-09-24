using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// Importado
using TreinoAspNetCore2_2.Models;

namespace TreinoAspNetCore2_2.Controllers
{
    #region Métodos de teste

    public class ClienteController : Controller
    {
        /*===================================================================//
        Esse public (Teste()) é so para direcionar à uma view criada p/ testes.*/
        public IActionResult Teste()
        {
            return View();
        }
        //===================================================================//

        /*===================================================================//
         * 
         Esse public (Post()) é so para simular uma View de um site de POSTAGEM.*/
        public IActionResult Post()
        {
            return View();
        }
        //===================================================================//

        // Esse public (Teste_Cadastro()) é so para simular uma View de um site de POSTAGEM.*/
        public IActionResult Teste_Cadastro()
        {
            return View();
        }
        //===================================================================//

        // Esse public (Teste_Cadastro()) é so para simular uma View de um site de POSTAGEM.*/
        public IActionResult Lista()
        {
            return View();
        }
        //===================================================================//


        // Esse public (Teste_Cadastro()) é so para simular uma View de um site de POSTAGEM.*/
        public IActionResult Mixto()
        {
            return View();
        }
        //===================================================================//

        #endregion

        public IActionResult Index()
        {
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id != null)
            {
                // Carregar o registro do cliente numa ViewBag
                ViewBag.Cliente = new ClienteModel().RetornarCliente(Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Gravar();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Excluir Registro
        public IActionResult Excluir(int Id)
        {
            ViewData["IdExcluir"] = Id;
            return View();
        }

        public IActionResult ExcluirCliente(int Id)
        {
            // Tratamento para que se o Vendedor não estiver vinculado a uma venda Exclui
            // Se Caso não, Não Exclui... Redireciona para View (Tratamento).
            try
            {
                new ClienteModel().Excluir(Id);
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}