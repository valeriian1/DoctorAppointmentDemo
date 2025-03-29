using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Data.Repositories;


namespace DoctorAppointmentDemo.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private readonly ISerializationService serializationService;
        public override string Path { get; set; }
        public override int LastId { get; set; }

        public DoctorRepository(string appSettings, ISerializationService serializationService) 
        {
            this.serializationService = serializationService;
            var result = ReadFromAppSettings();
            Path = result.Database.Doctors.Path; 
            LastId = result.Database.Doctors.LastId;
        }

        public override void ShowInfo(Doctor doctor)
        {
            Console.WriteLine($"ID: {doctor.Id}, Type: {doctor.DoctorType}");
        }

        protected override void SaveLastId()
        {
            var result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;
            serializationService.Serialize(result, Path);
        }
    }
}
