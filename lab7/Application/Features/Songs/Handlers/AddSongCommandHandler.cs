using Application.Features.Songs.Commands;

namespace Application.Features.Songs.Handlers;

public class AddSongCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddSongCommand, Song>
{
    public async Task<Song> Handle(AddSongCommand request, CancellationToken cancellationToken)
    {
        var newSong = new Song(request.Name, new SongData(request.Length), request.Rating, request.ArtistId);
        
        await unitOfWork.Songs.AddAsync(newSong, cancellationToken);
        
        await unitOfWork.SaveChanges(cancellationToken);
        
        return newSong;
    }
}