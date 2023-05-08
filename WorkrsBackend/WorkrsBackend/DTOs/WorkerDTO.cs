namespace WorkrsBackend.DTOs
{
    public enum WorkerStatus
    {
        Available = 0,
        Busy = 1,
        Done = 2,
        MIA
    }

    public class WorkerDTO
    {
        public Guid WorkerId { get; set; }
        public WorkerStatus Status { get; set; }
        public string ServerName { get; set; }
        public Guid JobId { get; set; } = Guid.Empty;

        public WorkerDTO()
        {

        }
        public WorkerDTO(Guid workerId, WorkerStatus status, string serverName)
        {
            WorkerId = workerId;
            Status = status;
            ServerName = serverName;
        }
        public WorkerDTO(Guid workerId, WorkerStatus status, string serverName, Guid jobId)
        {
            WorkerId = workerId;
            Status = status;
            ServerName = serverName;
            JobId = jobId;
        }
    }
}
