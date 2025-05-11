using Domain.Entities.Core;

namespace Domain.Entities;

public class Song : Entity
{
    private Song() : base(null!) { }

    public Song(string name, SongData data, int rating, int artistId) : base(name)
    {
        Data = data;
        Rating = rating;
        ArtistId = artistId;
    }

    public SongData Data { get; private set; }

    public int Rating { get; private set; }

    public int ArtistId { get; private set; }
    
    public void ChangeArtistId(int newArtistId)
    {
        if (newArtistId < 0)
        {
            return;
        }

        ArtistId = newArtistId;
    }

    public void ChangeRating(int newRating)
    {
        if (newRating < 0)
        {
            return;
        }

        Rating = newRating;
    }

    public void ChangeData(SongData data)
    {
        Data = data;
    }
}