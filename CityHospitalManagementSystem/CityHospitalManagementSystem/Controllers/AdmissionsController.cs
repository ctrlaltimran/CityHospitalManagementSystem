using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityHospitalManagementSystem.Models;
using CityHospitalManagementSystem.Data;

namespace CityHospitalManagementSystem.Controllers
{
    public class AdmissionsController : Controller
    {
        private readonly HospitalDbContext _context;

        public AdmissionsController(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var admissions = _context.Admissions
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Bed);

            return View(await admissions.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var admission = await _context.Admissions
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Bed)
                .FirstOrDefaultAsync(m => m.AdmissionId == id);

            if (admission == null)
                return NotFound();

            return View(admission);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdmissionId,PatientId,DoctorId,WardId,BedId,AdmissionType,AdmissionDate,DischargeDate,Status")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admission);

                var bed = await _context.Beds.FindAsync(admission.BedId);
                if (bed != null)
                {
                    bed.IsOccupied = true;
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(admission);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var admission = await _context.Admissions.FindAsync(id);

            if (admission == null)
                return NotFound();

            LoadDropdowns();
            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdmissionId,PatientId,DoctorId,WardId,BedId,AdmissionType,AdmissionDate,DischargeDate,Status")] Admission admission)
        {
            if (id != admission.AdmissionId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admission);

                    var bed = await _context.Beds.FindAsync(admission.BedId);
                    if (bed != null)
                    {
                        bed.IsOccupied = admission.Status == "Admitted";
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionExists(admission.AdmissionId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(admission);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var admission = await _context.Admissions
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Ward)
                .Include(a => a.Bed)
                .FirstOrDefaultAsync(m => m.AdmissionId == id);

            if (admission == null)
                return NotFound();

            return View(admission);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admission = await _context.Admissions.FindAsync(id);

            if (admission != null)
            {
                var bed = await _context.Beds.FindAsync(admission.BedId);
                if (bed != null)
                {
                    bed.IsOccupied = false;
                }

                _context.Admissions.Remove(admission);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadDropdowns()
        {
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "FullName");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FullName");
            ViewData["WardId"] = new SelectList(_context.Wards, "WardId", "WardName");
            ViewData["BedId"] = new SelectList(_context.Beds.Where(b => !b.IsOccupied), "BedId", "BedNumber");
        }

        private bool AdmissionExists(int id)
        {
            return _context.Admissions.Any(e => e.AdmissionId == id);
        }
    }
}