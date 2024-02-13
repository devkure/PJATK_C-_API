using Excercise_6.Data;
using Excercise_6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Excercise_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SixController : ControllerBase
    {
        private readonly SixContext _context;

        public SixController(SixContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            List<Doctor> doctors = _context.Doctors.ToList();
            return Ok(doctors);
        }

        [HttpPost]
        public IActionResult AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return Ok(doctor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDoctor(int id, Doctor updatedDoctor)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(d => d.IdDoctor == id);
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.FirstName = updatedDoctor.FirstName;
            doctor.LastName = updatedDoctor.LastName;
            doctor.Email = updatedDoctor.Email;

            _context.SaveChanges();

            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(d => d.IdDoctor == id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetPrescription(int id)
        {
            Prescription prescription = _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
                .FirstOrDefault(p => p.IdPrescription == id);

            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(prescription);
        }
    }
}
