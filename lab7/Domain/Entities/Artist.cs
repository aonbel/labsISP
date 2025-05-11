using Domain.Entities.Core;

namespace Domain.Entities;

public class Artist : Entity
{
    private Artist() : base(null!) { }
    
    public Artist(string name, int age) : base(name)
    {
        Age = age;
    }
    
    private ICollection<Song> _songs = [];
    
    public ICollection<Song> Songs { get => _songs; set => _songs = value; }
    public int Age { get; private set; }

    public void ChangeAge(int age)
    {
        if (age < 18)
        {
            return;
        }
        
        Age = age;
    }

    public override string ToString()
    {
        return Name;
    }
}