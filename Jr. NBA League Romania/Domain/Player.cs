namespace Jr._NBA_League_Romania.Domain;

public class Player : Student
{
    public Player() { }
    public Player(int id, string name, string school, Team team) : base(id, name, school)
    {
        this._team = team;
    }

    private Team _team;
        
    public Team Team 
    { 
        get
        {
            return this._team;
        }
        set
        {
            this._team = value;
        }
    }
}