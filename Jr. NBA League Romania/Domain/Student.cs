namespace Jr._NBA_League_Romania.Domain;

public class Student : Entity<int>
{
    public Student() { }
    public Student(int id, string name, string school) : base(id)
    {
        this.name = name;
        this.school = school;
    }

    private string name;
        
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string school;
    public string School
    {
        get { return school; }
        set { school = value; }
    }

    public override string ToString()
    {
        return   Name + ";" + School;
    }
}