using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ISerializationService serializationService;
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public AppointmentRepository(string appSettings, ISerializationService serializationService)
        {
            this.serializationService = serializationService;
            var result = ReadFromAppSettings();

            Path = result.Database.Appointments.Path;
            LastId = result.Database.Appointments.LastId;
        }

        public override void ShowInfo(Appointment appointment)
        {
            Console.WriteLine(); 
        }

        protected override void SaveLastId()
        {
            var result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;
            serializationService.Serialize(result, Path);
        }
    }
}
