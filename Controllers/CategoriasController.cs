using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class CategoriasController: Controller
    {
        private readonly BancoContext _context;

        public CategoriasController(BancoContext context)
        {
            _context = context;
        } 
        public IActionResult Index()
        {
            return View();
        } 

        public IActionResult Form()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Cadastro(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            var resultado = _context.SaveChanges();
            
            TempData["AlertaTipo"] = "success";
            TempData["AlertaMsg"] = "Cadastro realizado com sucesso!";            

            return RedirectToAction("Index");
        }      
    }
}