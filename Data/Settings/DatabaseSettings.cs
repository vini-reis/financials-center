using System;

namespace Data.Settings
{
    public class DatabaseSettings
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
