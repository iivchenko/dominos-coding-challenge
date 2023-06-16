namespace CodingChallenge.Domain.Common;

public interface IAggregateRoot<TId>
{
    TId Id { get; }
}
