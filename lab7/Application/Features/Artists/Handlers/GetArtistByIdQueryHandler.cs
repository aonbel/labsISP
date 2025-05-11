using Application.Features.Artists.Queries;

namespace Application.Features.Artists.Handlers;

public class GetArtistByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetArtistByIdQuery, Artist>
{
    public async Task<Artist> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var artist = await unitOfWork.Artists.FirstOrDefaultAsync(artist => artist.Id == request.Id, cancellationToken);

        if (artist is null)
        {
            throw new KeyNotFoundException();
        }

        return artist;
    }
}