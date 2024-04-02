using CardGames;
using CardGames.Enums;

namespace PokerConsApp;
public class Player(int id, string name)
{
    int Id { get; set; } = id;
    string Name { get; set; } = name;
    public List<PlayingCard> Hand { get; set; } = [];
    public List<PlayingCard> BestHoldemHand { get; set; } = [];
    public HandRating BestHandType { get; set; }
    public int HandScore { get; set; }
    public bool Fold { get; set; } = false;
    public override string ToString()
    {
        return Name;
    }
}
