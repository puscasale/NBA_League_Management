namespace Jr._NBA_League_Romania.Domain;

public class Entity<ID>
{
    public Entity() { }
    public Entity(ID id) 
    { 
        this.id = id;   
    }

    private ID id;
        
    public ID Id 
    { 
        get
        {
            return id;
        }
        set
        {
            id = value;
        } 
    }
}