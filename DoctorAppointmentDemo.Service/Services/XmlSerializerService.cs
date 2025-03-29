using System.Xml.Serialization;
using DoctorAppointmentDemo.Data.Interfaces;

namespace DoctorAppointmentDemo.Service.Services
{
    public class XmlSerializerService: ISerializationService
    {
        public T Deserialize<T>(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path), "Cannot be null");

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                return (T)serializer.Deserialize(fs);
            }
        }

        public void Serialize<T>(T data, string path)
        {
            if (data == null || string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(data), "Cannot be null");

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, data);
            }
        }
    }
}