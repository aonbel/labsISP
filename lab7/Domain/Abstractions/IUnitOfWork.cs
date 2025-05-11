using Domain.Entities;

namespace Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<Song> Songs { get; }
    IRepository<Artist> Artists { get; }
    
    public Task SaveChanges(CancellationToken cancellationToken = default);
    public Task DeleteDatabase(CancellationToken cancellationToken = default);
    public Task CreateDatabase(CancellationToken cancellationToken = default);
}