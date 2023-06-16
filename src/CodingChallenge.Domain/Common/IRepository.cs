namespace CodingChallenge.Domain.Common;

public interface IRepository<TAggregate, TId> where TAggregate : IAggregateRoot<TId>
{
}
