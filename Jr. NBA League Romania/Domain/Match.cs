namespace Jr._NBA_League_Romania.Domain;

public class Match : Entity<int>
{
    public Match() { }
    public Match(int id, Team team1, Team team2, DateTime date) : base(id)
    { 
        this._team1 = team1;
        this._team2 = team2;
        this.date = date;
    }

    private Team _team1;
        
    public Team Team1 
    { 
        get { 
            return this._team1;
        } 
        set {
            this._team1 = value;
        } 
    }

    private Team _team2;

    public Team Team2 
    { 
        get { 
            return this._team2;
        } 
        set 
        { 
            this._team2 = value; 
        }
    }
    private DateTime date;

    public DateTime Date 
    { 
        get
        {
            return this.date;
        }
        set
        {
            this.date = value;
        }
    }

    
    public override string ToString()
    {
        return this.Team1.ToString() + ", " + this.Team2.ToString() + ", " + this.Date.ToString();
    }
}