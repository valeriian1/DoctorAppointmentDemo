using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;

namespace DoctorAppointmentDemo.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly ISerializationService serializationService;
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public PatientRepository(string appSettings, ISerializationService serializationService)
        {
            this.serializationService = serializationService;
            var result = ReadFromAppSettings();

            Path = result.Database.Patients.Path;
            LastId = result.Database.Patients.LastId;
        }

        public override void ShowInfo(Patient patient)
        {
            Console.WriteLine($"ID: {patient.Id}, Type: {patient.IllnessType}");
        }
        protected override void SaveLastId()
        {
            var result = ReadFromAppSettings();
            result.Database.Doctors.LastId = LastId;
            serializationService.Serialize(result, Path);
        }
    }
}
