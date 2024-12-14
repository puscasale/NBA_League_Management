namespace Jr._NBA_League_Romania.Domain;

public class Team : Entity<int>
{
    public Team() { }
    public Team(int id, string name) : base(id)
    {
        this.name = name;
    }

    private string name;
        
    public string Name
    { 
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public override string ToString()
    {
        return Name;
    }
}