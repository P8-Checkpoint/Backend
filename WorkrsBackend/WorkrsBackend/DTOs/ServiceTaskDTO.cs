namespace WorkrsBackend.DTOs
{
    public enum ServiceTaskStatus
    {
        Created,
        Starting,
        InProgress,
        Canceled,
        Cancel,
        Failed,
        Completed
    }

    public class ServiceTaskDTO
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public ServiceTaskStatus Status { get; set; }
        public string SourcePath { get; set; }
        public string BackupPath { get; set; }
        public string ResultPath { get; set; }

        public ServiceTaskDTO(Guid id, Guid clientId, string name, ServiceTaskStatus status, string sourcePath, string backupPath, string resultPath)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
            Status = status;
            SourcePath = sourcePath;
            BackupPath = backupPath;
            ResultPath = resultPath;
        }
        public ServiceTaskDTO(Guid id, Guid clientId, string name, ServiceTaskStatus status)
        {
            Id = id;
            ClientId = clientId;
            Name = name;
            Status = status;
        }
    }
}
