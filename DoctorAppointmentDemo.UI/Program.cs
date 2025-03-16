using System;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;

namespace MyDoctorAppointment
{
    public class DoctorAppointment
    {
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

        public DoctorAppointment()
        {
            _doctorService = new DoctorService();
            _patientService = new PatientService();
            _appointmentService = new AppointmentService();
        }

        public void Menu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Меню запису до лікаря ===");
                Console.WriteLine("1. Переглянути список лікарів");
                Console.WriteLine("2. Додати лікаря");
                Console.WriteLine("3. Переглянути список пацієнтів");
                Console.WriteLine("4. Додати пацієнта");
                Console.WriteLine("5. Вийти");
                Console.Write("Оберіть опцію: ");

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
                        Console.WriteLine("Завершення роботи...");
                        isRunning = false;
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        private void ViewDoctors()
        {
            Console.WriteLine("\n=== Список лікарів ===");
            var doctors = _doctorService.GetAll();
            foreach (var doc in doctors)
            {
                Console.WriteLine($"{doc.Name} {doc.Surname} - {doc.DoctorType}, Досвід: {doc.Experience} років");
            }
        }

        private void AddDoctor()
        {
            Console.WriteLine("\n=== Додавання нового лікаря ===");

            Console.Write("Ім'я: ");
            string name = Console.ReadLine()!;

            Console.Write("Прізвище: ");
            string surname = Console.ReadLine()!;

            Console.Write("Досвід (років): ");
            byte experience = byte.Parse(Console.ReadLine()!);

            Console.WriteLine("Оберіть тип лікаря:");
            foreach (var type in Enum.GetValues(typeof(Domain.Enums.DoctorTypes)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }

            int doctorType = int.Parse(Console.ReadLine()!);

            var newDoctor = new Doctor
            {
                Name = name,
                Surname = surname,
                Experience = experience,
                DoctorType = (Domain.Enums.DoctorTypes)doctorType
            };

            _doctorService.Create(newDoctor);
            Console.WriteLine("Лікар успішно доданий!");
        }

        private void ViewPatients()
        {
            Console.WriteLine("\n=== Список пацієнтів ===");
            var patients = _patientService.GetAll();

            foreach (var patient in patients)
            {
                Console.WriteLine($"{patient.Name} {patient.Surname}");
            }
        }

        private void AddPatient()
        {
            Console.WriteLine("\n=== Додавання нового пацієнта ===");

            Console.Write("Ім'я: ");
            string name = Console.ReadLine()!;

            Console.Write("Прізвище: ");
            string surname = Console.ReadLine()!;

            var newPatient = new Patient
            {
                Name = name,
                Surname = surname
            };

            _patientService.Create(newPatient);
            Console.WriteLine("Пацієнт успішно доданий!");
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
}
