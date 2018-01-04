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

        public IActionResult Form(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            return View(categoria);
        }

        public bool NoDuplicateCad(string nome)
        {
            var categoriaCount = _context.Categorias.Where(c => c.Nome == nome).Count();

            if(categoriaCount > 0){
                return true;
            }
            return false;
        }

        public bool NoDuplicateEdit(int id, string nome){
            var categoriaCount = _context.Categorias.Where(c => c.Id != id).Where(c => c.Nome == nome).Count();

            if(categoriaCount > 0){
                return true;
            }
            return false;
        }
        
        [HttpPost]
        public IActionResult Cadastro(Categoria categoria)
        {
            
            if(categoria.Id != 0){

                if(this.NoDuplicateEdit(categoria.Id, categoria.Nome)){
                    TempData["AlertaTipo"] = "error";
                    TempData["AlertaMsg"] = "Categoria já possui cadastro!";

                    return View("Form");

                }else{
                    _context.Categorias.Update(categoria);
                    TempData["AlertaTipo"] = "success";
                    TempData["AlertaMsg"] = "Cadastro alterado com sucesso!";

                }                

            }else{

                if(this.NoDuplicateCad(categoria.Nome)){
                    TempData["AlertaTipo"] = "error";
                    TempData["AlertaMsg"] = "Categoria já possui cadastro!";

                    return View("Form");

                }else{
                    _context.Categorias.Add(categoria);
                    TempData["AlertaTipo"] = "success";
                    TempData["AlertaMsg"] = "Cadastro realizado com sucesso!";
                }
                
            }

            _context.SaveChanges();                       

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