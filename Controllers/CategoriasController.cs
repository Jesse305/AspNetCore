using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
        public IActionResult Duplicidade(Categoria categoria)
        {
            var count = 0;
            var retorno = 0;
            if(categoria.Id == 0){
                count = _context.Categorias.Where(c => c.Nome == categoria.Nome).Count();
                if(count > 0){
                    retorno = 1;
                }
            }
            else{
                count = _context.Categorias.Where(c => c.Id != categoria.Id).Where(c => c.Nome == categoria.Nome).Count();
                if(count > 0){
                    retorno = 1;
                }
            }
            return Json(retorno);
        }
        
        [HttpPost]
        public IActionResult Cadastro(Categoria categoria)
        {
            
            if(categoria.Id == 0){ 
                _context.Categorias.Add(categoria);
                TempData["AlertaTipo"] = "success";
                TempData["AlertaMsg"] = "Cadastro realizado com sucesso!";
            }else{
                _context.Categorias.Update(categoria);
                TempData["AlertaTipo"] = "success";
                TempData["AlertaMsg"] = "Cadastro alterado com sucesso!";
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

        public IActionResult Produtos(int id)
        {
            
            var produtos = _context.Produtos.Where(p => p.CategoriaId == id).Include(p => p.Categoria).ToList();
            return View(produtos);
        } 

    }
}