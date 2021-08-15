using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProdutosComprados.Data;
using ProdutosComprados.Models;
using Highsoft.Web.Mvc.Charts;

namespace ProdutosComprados.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ProdutosCompradosContext _context;

        public ProdutosController(ProdutosCompradosContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index(string ProdutoComprado)
        {
            List<Produtos> produtos = new List<Produtos>();

            ViewBag.Comprado = new SelectList(Comprado(), ProdutoComprado);

            produtos = _context.Produtos.Where(x => x.ListaDeProdutosId > 0).ToList();

            List<ColumnSeriesData> PrecoData = new List<ColumnSeriesData>();
            List<LineSeriesData> TotalPreco = new List<LineSeriesData>();

            //produtos = _context.Produtos.Where(x => x.NomeProduto != "").ToList();
            produtos.ForEach(p => PrecoData.Add(new ColumnSeriesData { Y = p.PrecoProduto }));
            List<string> precoDataString = new List<string>();
            produtos.ForEach(p => precoDataString.Add(p.NomeProduto));
            List<double> totalPreco = new List<double>();
            double soma = 0.0;
            //produtos.ForEach(p => TotalPreco.Add(new LineSeriesData { Y = p.PrecoProduto }));
            foreach (var item in produtos)
            {
                soma += item.PrecoProduto;
                TotalPreco.Add(new LineSeriesData { Y = Math.Round(soma, 2)});
            }



            ViewData["PrecoData"] = PrecoData;
            ViewData["PrecoDataString"] = precoDataString;
            ViewData["TotalPreco"] = TotalPreco;

            if (ProdutoComprado == "Sim")
            {
                //var comprados = _context.Produtos.Where(x => x.ProdutoComprado == true).ToList();
                produtos = _context.Produtos.Where(x => x.ProdutoComprado == true).ToList();
                return View(produtos);
            }
            else if (ProdutoComprado == "Não")
            {
                produtos = _context.Produtos.Where(x => x.ProdutoComprado == false).ToList();
                return View(produtos);
            }

            //List<ColumnSeriesData> PrecoData = new List<ColumnSeriesData>();

            //produtos = _context.Produtos.Where(x => x.NomeProduto != "").ToList();
            //produtos.ForEach(p => PrecoData.Add( new ColumnSeriesData { Y = p.PrecoProduto }));
            //List<string> precoDataString = new List<string>();
            //produtos.ForEach(p => precoDataString.Add(p.NomeProduto));
            //ViewData["PrecoData"] = PrecoData;
            //ViewData["PrecoDataString"] = precoDataString;


            return View(await _context.Produtos.ToListAsync());
        }

        private List<string> Comprado()
        {
            List<string> comprado = new List<string>
            {
                "Sim","Não"
            };

            return comprado;
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ListaDeProdutosId == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListaDeProdutosId,NomeProduto,PrecoProduto,ProdutoComprado")] Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtos);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos.FindAsync(id);
            if (produtos == null)
            {
                return NotFound();
            }
            return View(produtos);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListaDeProdutosId,NomeProduto,PrecoProduto,ProdutoComprado")] Produtos produtos)
        {
            if (id != produtos.ListaDeProdutosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutosExists(produtos.ListaDeProdutosId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produtos);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtos = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ListaDeProdutosId == id);
            if (produtos == null)
            {
                return NotFound();
            }

            return View(produtos);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtos = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produtos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutosExists(int id)
        {
            return _context.Produtos.Any(e => e.ListaDeProdutosId == id);
        }
    }
}
