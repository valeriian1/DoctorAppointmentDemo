using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Service.Interfaces;
using DoctorAppointmentDemo.Service.Services;
namespace DoctorAppointmentDemo.Data.Configuration;


    public class DoctorAppointment
    {
        private readonly string _appSettings;
        private static DoctorAppointment? doctorAppointment;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService; // later

        private enum MenuOption
        {
            ViewDoctors = 1,
            AddDoctor,
            ViewPatients,
            AddPatient,
            Exit
        }

        public DoctorAppointment(string appSettings, ISerializationService serializationService)
        {
            _appSettings = appSettings;
            _doctorService = new DoctorService();
            _patientService = new PatientService();
            _appointmentService = new AppointmentService();
        }

        public static DoctorAppointment StorageType()
        {
            Console.WriteLine("Choose storage type: 1-xml, 2-json");
            int storageChoice = int.Parse(Console.ReadLine()!);

            switch (storageChoice)
            {
                case 1:
                    doctorAppointment = new DoctorAppointment(Constants.XmlSettingsPath, new XmlSerializerService());
                    break;
                case 2:
                    doctorAppointment = new DoctorAppointment(Constants.AppSettingsPath, new JsonSerializerService());
                    break;
                default:
                    Console.WriteLine("Invalid choice, defaulting to JSON.");
                    doctorAppointment = new DoctorAppointment(Constants.AppSettingsPath, new JsonSerializerService());
                    break;
            }

            return doctorAppointment;
        }


        public void Menu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Menu ===");
                Console.WriteLine("1. Check doctor list");
                Console.WriteLine("2. Add doctor");
                Console.WriteLine("3. Check patient list");
                Console.WriteLine("4. Add patient");
                Console.WriteLine("5. Exit");
                Console.Write("Your choice: ");

                int choice = int.Parse(Console.ReadLine()!);
                MenuOption option = (MenuOption)choice;

                switch (option)
                {
                    case MenuOption.ViewDoctors:
                        ViewDoctors();
                        break;
                    case MenuOption.AddDoctor:
                        AddDoctor();
                        break;
                    case MenuOption.ViewPatients:
                        ViewPatients();
                        break;
                    case MenuOption.AddPatient:
                        AddPatient();
                        break;
                    case MenuOption.Exit:
                        Console.WriteLine("Successful exit...");
                        isRunning = false;
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nPress any key...");
                    Console.ReadKey();
                }
            }
        }

        private void ViewDoctors()
        {
            Console.WriteLine("\n=== Doctor List ===");
            var doctors = _doctorService.GetAll();
            foreach (var doc in doctors)
            {
                Console.WriteLine($"{doc.Name} {doc.Surname} - {doc.DoctorType}, Exp: {doc.Experience} years");
            }
        }

        private void AddDoctor()
        {
            Console.WriteLine("\n=== Add Doctor ===");

            Console.Write("Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Surname: ");
            string surname = Console.ReadLine()!;

            Console.Write("Experience year: ");
            byte experience = byte.Parse(Console.ReadLine()!);

            Console.Write("Email: ");
            string email = Console.ReadLine()!;

            Console.Write("Phone: ");
            string phone = Console.ReadLine()!;

            Console.WriteLine("Choose doctor type:");
            foreach (var type in Enum.GetValues(typeof(Domain.Enums.DoctorTypes)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            Console.Write("Your choice:");
            int doctorType = int.Parse(Console.ReadLine()!);

            var newDoctor = new Doctor
            {
                Name = name,
                Surname = surname,
                Experience = experience,
                DoctorType = (Domain.Enums.DoctorTypes)doctorType
            };

            _doctorService.Create(newDoctor);
            Console.WriteLine("Doctor is created successfully!");
        }

        private void ViewPatients()
        {
            Console.WriteLine("\n=== Patient List ===");
            var patients = _patientService.GetAll();

            foreach (var patient in patients)
            {
                Console.WriteLine($"{patient.Name} {patient.Surname}");
            }
        }

        private void AddPatient()
        {
            Console.WriteLine("\n=== Add patient ===");

            Console.Write("Name: ");
            string name = Console.ReadLine()!;

            Console.Write("Surname: ");
            string surname = Console.ReadLine()!;

            Console.Write("Email: ");
            string email = Console.ReadLine()!;

            Console.Write("Phone: ");
            string phone = Console.ReadLine()!;

            Console.Write("Illness type: ");
            foreach (var type in Enum.GetValues(typeof(Domain.Enums.IllnessTypes)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            string illnessType = Console.ReadLine()!;

            var newPatient = new Patient
            {
                Name = name,
                Surname = surname
            };

            _patientService.Create(newPatient);
            Console.WriteLine("Patient is created successfully!");
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = DoctorAppointment.StorageType();
            
            doctorAppointment.Menu();
        }
    }

