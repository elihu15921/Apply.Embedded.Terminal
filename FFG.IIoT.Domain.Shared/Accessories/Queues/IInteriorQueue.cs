namespace IIoT.Domain.Shared.Accessories.Queues;
public interface IInteriorQueue
{
    public ref struct Model
    {
        public static string VTM415 => "VTM415_TC";
        public static string VUX400 => "VUX400_TC";
        public static string UCT600 => "UCT600_TC";
    }
}