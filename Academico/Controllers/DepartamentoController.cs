using Academico.Data;
using Academico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academico.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly AcademicoContext _context;

        public DepartamentoController(AcademicoContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _context.Departamentos.OrderBy(i => i.Nome).ToListAsync());
        }
        public bool DepartamentoExists(long? id)
        {
            return _context.Departamentos.Any(i => i.Id == id);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome", "Endereco")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar o Departamento.");

            }
            return View(departamento);

        }

        public async Task<ActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(long? id, [Bind("Id", "Nome", "Endereco")] Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(departamento);


        }

        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.Id == id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }
    }
}