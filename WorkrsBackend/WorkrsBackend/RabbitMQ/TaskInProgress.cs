using WorkrsBackend.DTOs;

namespace WorkrsBackend.RabbitMQ
{
    public class TaskInProgress
    {
        public ServiceTaskDTO ServiceTask { get; set; }
        public Worker? Worker { get; set; }

        public TaskInProgress(ServiceTaskDTO serviceTask, Worker worker)
        {
            ServiceTask = serviceTask;
            Worker = worker;
        }
    }
}
