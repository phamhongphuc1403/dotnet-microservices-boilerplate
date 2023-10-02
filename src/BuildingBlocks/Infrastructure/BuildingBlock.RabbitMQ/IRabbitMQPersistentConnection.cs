using RabbitMQ.Client;

namespace BuildingBlock.RabbitMQ;

public interface IRabbitMQPersistentConnection
{
    bool IsConnected { get; }
    bool TryConnect();
    IModel CreateModel();
}