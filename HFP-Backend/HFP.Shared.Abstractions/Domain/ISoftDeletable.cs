namespace HFP.Shared.Abstractions.Domain
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
        void SoftDelete();
    }
}
