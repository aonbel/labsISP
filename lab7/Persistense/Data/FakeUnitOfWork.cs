using Persistence.Repositories;

namespace Persistence.Data;

public class FakeUnitOfWork : IUnitOfWork
{
    public IRepository<Song> Songs { get; } = new FakeSongRepository();
    public IRepository<Artist> Artists { get; } = new FakeArtistRepository();
    
    public Task SaveChanges(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteDatabase(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CreateDatabase(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}