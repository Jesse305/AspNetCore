using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly BancoContext _db;

        public ProdutosController(BancoContext context)
        {
            _db = context;            
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _db.Produtos.Include(p => p.Categoria).ToListAsync();
            return View(produtos);
        }

        public async Task<IActionResult> Form(int? id)
        {
            if(id == null){
                ViewData["CategoriaId"] = new SelectList(_db.Categorias, "Id", "Nome");
                return View();
            }else{                
                var produto = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == id);
                ViewData["CategoriaId"] = new SelectList(_db.Categorias, "Id", "Nome", produto.CategoriaId);
                return View(produto);
            }            
        }

        public IActionResult Duplicidade(Produto produto)
        {
            var count = 0;
            var retorno = 0;

            if(produto.Id == 0){
                count =  _db.Produtos.Where(p => p.Nome == produto.Nome).Count();
               
            }else{
                count = _db.Produtos.Where(p => p.Nome == produto.Nome).Where(p => p.Id != produto.Id).Count();
            }

            if(count > 0){
                retorno = 1;
            }

            return Json(retorno);
        }

        public async Task<IActionResult> Cadastro(Produto produto)
        {
            if(produto.Id == 0){
                _db.Produtos.Add(produto);
                TempData["AlertaTipo"] = "success";
                TempData["AlertaMsg"] = "Cadastro realizado com sucesso!";
            }else{
                _db.Produtos.Update(produto);
                TempData["AlertaTipo"] = "success";
                TempData["AlertaMsg"] = "Cadastro alterado com sucesso!";
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _db.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            _db.Produtos.Remove(produto);
            await _db.SaveChangesAsync();
            TempData["AlertaTipo"] = "success";
            TempData["AlertaMsg"] = "Cadastro apagado com sucesso!";

            return RedirectToAction("Index");
        }
        
    }
}