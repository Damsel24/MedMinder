using Microsoft.EntityFrameworkCore;
using MedMinder_Api.Models;

namespace MedMinder_Api.Data
{
    public class PatientRepo : IPatientRepo
    {
        private readonly AppDbContext _context;

        public PatientRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients!.ToListAsync();
        }

        public async Task CreatePatient(Patient cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            await _context.Patients!.AddAsync(cmd);
        }

        public async Task<Patient?> GetPatientById(int id)
        {
            return await _context.Patients!.FirstOrDefaultAsync(c => c.PatientId == id); // Changed Id to PatientId
        }

        public void DeletePatient(Patient cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Patients.Remove(cmd);
        }
    }
}
