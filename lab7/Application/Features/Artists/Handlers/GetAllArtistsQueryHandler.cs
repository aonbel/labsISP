using Application.Features.Artists.Queries;

namespace Application.Features.Artists.Handlers;

public class GetAllArtistsQueryHandler (IUnitOfWork unitOfWork) : IRequestHandler<GetAllArtistsQuery, IReadOnlyList<Artist>>
{
    public async Task<IReadOnlyList<Artist>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        var artists = await unitOfWork.Artists.ListAllAsync(cancellationToken);

        return artists;
    }
}