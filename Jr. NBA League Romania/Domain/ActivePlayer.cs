namespace Jr._NBA_League_Romania.Domain;


public class ActivePlayer : Entity<int>
{
    public ActivePlayer() { }

    private int idPlayer;
        
    public int IdPlayer 
    { 
        get { return idPlayer; }
        set { idPlayer = value; }
    }

    private int idMatch;
    public int IdMatch
    { 
        get { return idMatch; } 
        set { idMatch = value; } 
    }

    private int nrPoints;

    public int NrPoints
    {
        get { return nrPoints;  }
        set { nrPoints = value;}
    }

    private string type;
    public string Type
    { 
        get { return type; }
        set { type = value; } 
    }

    public ActivePlayer(int id, int idPlayer, int idMatch, int nrPoints, string type) : base(id) 
    {
        this.idPlayer = idPlayer;
        this.idMatch = idMatch;
        this.nrPoints = nrPoints;
        this.type = type;
    }

    public override string ToString()
    {
        return idPlayer.ToString() + ' ' +type ;
    }
}