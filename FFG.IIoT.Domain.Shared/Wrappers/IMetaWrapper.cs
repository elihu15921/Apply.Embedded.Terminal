namespace IIoT.Domain.Shared.Wrappers;
public interface IMetaWrapper
{
    IInformationTrunk Information { get; }
    ref struct BucketTag
    {
        public static string Trunk => "trunks";
        public static string Spindle => "spindles";
    }
}