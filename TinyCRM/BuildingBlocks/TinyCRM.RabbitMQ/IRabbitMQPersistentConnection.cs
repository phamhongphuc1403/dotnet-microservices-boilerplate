using RabbitMQ.Client;

namespace TinyCRM.RabbitMQ;

public interface IRabbitMQPersistentConnection
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}