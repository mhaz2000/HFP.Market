namespace HFP.Shared.Abstractions.Domain
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
