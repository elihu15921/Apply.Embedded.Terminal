namespace IIoT.Domain.Shared.Wrappers;
public interface IQueueWrapper
{
    IIcpdasQueue Icpdas { get; }
    ITangramQueue Tangram { get; }
}