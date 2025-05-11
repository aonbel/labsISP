namespace Domain.Entities.Core;

public class Entity : BaseEntity
{
    private Entity() { }

    protected Entity(string name)
    {
        Name = name;
    }
    
    public string Name { get; private set; }
    
    public void ChangeName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return;
        }
        
        Name = name;
    }
}