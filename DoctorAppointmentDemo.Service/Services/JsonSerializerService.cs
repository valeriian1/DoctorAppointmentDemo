using DoctorAppointmentDemo.Data.Interfaces;
using Newtonsoft.Json;

namespace DoctorAppointmentDemo.Service.Services
{
    public class JsonSerializerService : ISerializationService
    {
        public T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException(nameof(json), "Cannot be null");
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Serialize<T>(T data, string path)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "Cannot be null");
            File.WriteAllText(path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}
