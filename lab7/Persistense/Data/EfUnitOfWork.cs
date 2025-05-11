using Persistence.Repositories;

namespace Persistence.Data;

public class EfUnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public IRepository<Song> Songs { get; } = new EfRepository<Song>(context);
    public IRepository<Artist> Artists { get; }= new EfRepository<Artist>(context);
    
    public async Task SaveChanges(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteDatabase(CancellationToken cancellationToken)
    {
        await context.Database.EnsureDeletedAsync(cancellationToken);
    }

    public async Task CreateDatabase(CancellationToken cancellationToken)
    {
        await context.Database.EnsureCreatedAsync(cancellationToken);
    }
}