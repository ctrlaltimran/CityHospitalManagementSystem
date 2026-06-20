using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityHospitalManagementSystem.Models;
using CityHospitalManagementSystem.Data;

namespace CityHospitalManagementSystem.Controllers
{
    public class TreatmentsController : Controller
    {
        private readonly HospitalDbContext _context;

        public TreatmentsController(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Treatments.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var treatment = await _context.Treatments
                .FirstOrDefaultAsync(m => m.TreatmentId == id);

            if (treatment == null)
                return NotFound();

            return View(treatment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreatmentId,AdmissionId,TreatmentDate,TreatmentDetails,MedicineGiven,Notes")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(treatment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var treatment = await _context.Treatments.FindAsync(id);

            if (treatment == null)
                return NotFound();

            return View(treatment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreatmentId,AdmissionId,TreatmentDate,TreatmentDetails,MedicineGiven,Notes")] Treatment treatment)
        {
            if (id != treatment.TreatmentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentExists(treatment.TreatmentId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(treatment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var treatment = await _context.Treatments
                .FirstOrDefaultAsync(m => m.TreatmentId == id);

            if (treatment == null)
                return NotFound();

            return View(treatment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);

            if (treatment != null)
            {
                _context.Treatments.Remove(treatment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TreatmentExists(int id)
        {
            return _context.Treatments.Any(e => e.TreatmentId == id);
        }
    }
}