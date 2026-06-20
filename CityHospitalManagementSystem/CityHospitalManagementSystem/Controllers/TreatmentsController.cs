
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityHospitalManagementSystem.Models;
using CityHospitalManagementSystem.Data;

public class TreatmentsController : Controller
{
    private readonly HospitalDbContext _context;

    public TreatmentsController(HospitalDbContext context)
    {
        _context = context;
    }

    // GET: TREATMENTS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Treatments.ToListAsync());
    }

    // GET: TREATMENTS/Details/5
    public async Task<IActionResult> Details(int? treatmentid)
    {
        if (treatmentid == null)
        {
            return NotFound();
        }

        var treatment = await _context.Treatments
            .FirstOrDefaultAsync(m => m.TreatmentId == treatmentid);
        if (treatment == null)
        {
            return NotFound();
        }

        return View(treatment);
    }

    // GET: TREATMENTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TREATMENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TreatmentId,AdmissionId,TreatmentDate,TreatmentDetails,MedicineGiven,Notes,Admission")] Treatment treatment)
    {
        if (ModelState.IsValid)
        {
            _context.Add(treatment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(treatment);
    }

    // GET: TREATMENTS/Edit/5
    public async Task<IActionResult> Edit(int? treatmentid)
    {
        if (treatmentid == null)
        {
            return NotFound();
        }

        var treatment = await _context.Treatments.FindAsync(treatmentid);
        if (treatment == null)
        {
            return NotFound();
        }
        return View(treatment);
    }

    // POST: TREATMENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? treatmentid, [Bind("TreatmentId,AdmissionId,TreatmentDate,TreatmentDetails,MedicineGiven,Notes,Admission")] Treatment treatment)
    {
        if (treatmentid != treatment.TreatmentId)
        {
            return NotFound();
        }

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
        return View(treatment);
    }

    // GET: TREATMENTS/Delete/5
    public async Task<IActionResult> Delete(int? treatmentid)
    {
        if (treatmentid == null)
        {
            return NotFound();
        }

        var treatment = await _context.Treatments
            .FirstOrDefaultAsync(m => m.TreatmentId == treatmentid);
        if (treatment == null)
        {
            return NotFound();
        }

        return View(treatment);
    }

    // POST: TREATMENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? treatmentid)
    {
        var treatment = await _context.Treatments.FindAsync(treatmentid);
        if (treatment != null)
        {
            _context.Treatments.Remove(treatment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TreatmentExists(int? treatmentid)
    {
        return _context.Treatments.Any(e => e.TreatmentId == treatmentid);
    }
}
