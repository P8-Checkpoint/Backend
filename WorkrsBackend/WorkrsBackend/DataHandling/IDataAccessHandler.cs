using WorkrsBackend.DTOs;
using WorkrsBackend.RabbitMQ;

namespace WorkrsBackend.DataHandling
{
    public interface IDataAccessHandler
    {
        public void AddClientToClientDHT(ClientDTO client);
        //public Guid CreateClient(string username);
        public ClientDTO? FindClientByUserName(string username);
        public void DeleteClientFromClientDHT(Guid id);
        public Dictionary<Guid, ClientDTO> GetClientDHT();
        public void UpdateClientDHT(ClientDTO client);
        public void UpdateWorkerDHT(Worker worker);
        public Dictionary<string, Server> GetPrimaryServers();
        public Server? GetServerInfo(string serverName);
        public Dictionary<Guid, Worker> GetWorkerDHT();
        public void AddWorkerToWorkerDHT(Worker worker);
        public void AddServerToServerDHT(Server serverName);
        public void UpdateServerDHT(Server serverName);
        public void AddTask(ServiceTask task);
        public void UpdateTask(ServiceTask task);
        public ServiceTask? GetTaskFromId(Guid id);
        public List<ServiceTask> GetTaskForClient(Guid clientId);
        public List<ServiceTask> GetTasksFromStatus(ServiceTaskStatus status);
    }
}