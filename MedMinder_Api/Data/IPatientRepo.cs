using MedMinder_Api.Models;

namespace MedMinder_Api.Data
{
    public interface IPatientRepo
    {
        Task SaveChanges();

        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient?> GetPatientById(int id);
        Task CreatePatient(Patient cmd);
    
        void DeletePatient(Patient cmd);
    }
}