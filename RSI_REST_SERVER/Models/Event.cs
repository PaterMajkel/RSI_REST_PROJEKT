using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RSI_REST_SERVER.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
   public Details Details { get; set; } = new Details();
    public List<Link> Links { get; set; }
    public Event() { }

    public Event(int id, string name, string type, DateTime date, string description)
    {
        this.Id = id; 
        this.Name = name;
        this.Details = new Details() { Type = type, Date = date, Description = description };
    }
}
