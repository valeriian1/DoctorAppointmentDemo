using System;
using System.Text.Json;

namespace DoctorAppointmentDemo.Data.Interfaces
{ 
    public interface ISerializationService
    {
        public void Serialize<T>(T data, string path);
        public T Deserialize<T>(string path);
    }
}