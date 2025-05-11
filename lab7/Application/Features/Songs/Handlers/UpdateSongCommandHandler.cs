using Application.Features.Songs.Commands;

namespace Application.Features.Songs.Handlers;

public class UpdateSongCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateSongCommand, Song>
{
    public async Task<Song> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        var song = await unitOfWork.Songs.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (song is null)
        {
            throw new KeyNotFoundException();
        }
        
        song.ChangeName(request.Name);
        song.ChangeData(new SongData(request.Length));
        song.ChangeArtistId(request.ArtistId);
        song.ChangeRating(request.Rating);
        
        await unitOfWork.SaveChanges(cancellationToken);
        
        return song;
    }
}