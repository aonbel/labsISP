using Application.Features.Artists.Commands;

namespace Application.Features.Artists.Handlers;

public class AddArtistCommandHandler( IUnitOfWork unitOfWork ) : IRequestHandler<AddArtistCommand, Artist>
{
    public async Task<Artist> Handle(AddArtistCommand request, CancellationToken cancellationToken)
    {
        var newArtist = new Artist(request.Name, request.Age);
        
        await unitOfWork.Artists.AddAsync(newArtist, cancellationToken);

        await unitOfWork.SaveChanges(cancellationToken);
        
        return newArtist;
    }
}