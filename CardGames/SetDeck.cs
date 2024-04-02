namespace CardGames;
public class SetDeck : IDeck
{
    readonly int _packSize;
    readonly List<SetCard> _sortedDeck;

    public SetDeck()
    {
        _packSize = 81;
        _sortedDeck = InitialisePack();
    }
    public int GetSize()
    {
        return _packSize;
    }
    public void ShufflePack()
    {
        throw new NotImplementedException();
    }
    private List<SetCard> InitialisePack()
    {
        List<SetCard> res = [];
        int shp = 0;
        int clr = 0;
        int num = 0;
        int shd = 0;
        for (int i = 0; i < _packSize; i++)
        {
            SetCard card = new()
            {
                Shape = (Shape)shp,
                Colour = (Colour)clr,
                Number = (Number)num,
                Shading = (Shading)shd
            };

            shp++;
            if (shp == 3)
            {
                shp = 0;
                clr++;
                if (clr == 3)
                {
                    clr = 0;
                    num++;
                    if (num == 3)
                    {
                        num = 0;
                        shd++;
                    }
                }
            }
            res.Add(card);
        }
        return res;
    }
    public override string ToString()
    {
        string s = "";
        foreach (var card in _sortedDeck)
        {
            s += card.ToString() + "\n";
        }
        return s;
    }
}
public enum Shape
{
    Square = 0,
    Circle = 1,
    Triangle = 2
}
public enum Colour
{
    Red = 0,
    Green = 1,
    Blue = 2
}
public enum Number
{
    One = 0,
    Two = 1,
    Three = 2
}
public enum Shading
{
    Clear = 0,
    Hashed = 1,
    Solid = 2
}
