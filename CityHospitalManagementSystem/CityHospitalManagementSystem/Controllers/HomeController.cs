using Microsoft.AspNetCore.Mvc;
using CityHospitalManagementSystem.Data;

namespace CityHospitalManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly HospitalDbContext _context;

        public HomeController(HospitalDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalPatients = _context.Patients.Count();
            ViewBag.TotalDoctors = _context.Doctors.Count();
            ViewBag.TotalAdmissions = _context.Admissions.Count();
            ViewBag.AvailableBeds = _context.Beds.Count(b => b.IsOccupied == false);
            ViewBag.OccupiedBeds = _context.Beds.Count(b => b.IsOccupied == true);

            ViewBag.DischargedPatients = _context.Admissions.Count(a => a.Status == "Discharged");
            ViewBag.EmergencyAdmissions = _context.Admissions.Count(a => a.AdmissionType == "Emergency");
            ViewBag.MonthlyAdmissions = _context.Admissions.Count(a => a.AdmissionDate.Month == DateTime.Now.Month);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}