namespace IIoT.Domain.Shared.Accessories.Queues;
public interface ITangramQueue
{
    void Push(ITangramConnection.Entity.Meta meta);
    ref struct Label
    {
        public static string Status => "status";
        public static string Connection => "connection";
        public static string SensorData => "sensorData";
    }
}