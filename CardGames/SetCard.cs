namespace CardGames;
public class SetCard : ICard
{
    public Shape Shape { get; set; }
    public Colour Colour { get; set; }
    public Number Number { get; set; }
    public Shading Shading { get; set; }

    public override string ToString()
    {
        return  $"{Shape} {Colour} {Number} {Shading}";
    }
}
