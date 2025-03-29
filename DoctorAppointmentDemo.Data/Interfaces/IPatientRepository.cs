using DoctorAppointmentDemo.Domain.Entities;


namespace DoctorAppointmentDemo.Data.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        // you can add more specific doctor's methods
    }
}