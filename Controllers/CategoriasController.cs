using System.Linq;
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
            var categorias = _context.Categorias.ToList();
            return View(categorias);
        } 

        public IActionResult Form()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Cadastro(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            
            TempData["AlertaTipo"] = "success";
            TempData["AlertaMsg"] = "Cadastro realizado com sucesso!";            

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            TempData["AlertaTipo"] = "success";
            TempData["AlertaMsg"] = "Cadastro apagado com sucesso!";

            return RedirectToAction("Index");
        }    
    }
}