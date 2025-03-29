using DoctorAppointmentDemo.Domain.Entities;


namespace DoctorAppointmentDemo.Data.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        // you can add more specific doctor's methods
    }
}