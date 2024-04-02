using CardGames.Enums;

namespace CardGames;
public class PlayingCard : ICard
{
    public int Value { get; set; }
    public Suit Suit { get; set; }

    public override string ToString()
    {
        string card = Value.ToString();
        if (Value == 11) card = "Jack";
        else if (Value == 12) card = "Queen";
        else if (Value == 13) card = "King";
        else if (Value == 14) card = "Ace";
        return $"{card} of {Suit}s";
    }
}
