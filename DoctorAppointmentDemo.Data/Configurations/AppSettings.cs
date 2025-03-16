public class AppSettings
{
    public DatabaseConfig Database { get; set; } = new();

    public class DatabaseConfig
    {
        public EntityConfig Doctors { get; set; } = new();
        public EntityConfig Patients { get; set; } = new();
        public EntityConfig Appointments { get; set; } = new();
    }

    public class EntityConfig
    {
        public int LastId { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
