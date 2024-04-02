using CardGames.Enums;

namespace CardGames;
public class PlayingCardDeck : IDeck
{
    readonly int _packSize;
    readonly List<PlayingCard> _sortedDeck;
    List<PlayingCard> _shuffled = [];

    public PlayingCardDeck()
    {
        _packSize = 52;
        _sortedDeck = InitialisePack();
        ShufflePack();
    }
    public int GetSize()
    {
        return _packSize;
    }
    public PlayingCard GetCard(bool ShuffleIfEmpty)
    {
        if (_shuffled.Count == 0)
        {
            if (ShuffleIfEmpty)
                ShufflePack();
            else
            {
                return new PlayingCard() { Value = 0, Suit = Suit.Heart };
            }
        }
        PlayingCard card = _shuffled.ElementAt(0);
        _shuffled.RemoveAt(0);
        return card;
    }
    public List<PlayingCard> GetShuffledPack()
    {
        return _shuffled;
    }
    private List<PlayingCard> InitialisePack()
    {
        List<PlayingCard> pack = [];
        for (int i = 0;  i < _packSize; i++)
        {
            PlayingCard card = new()
            {
                Value = (i % 13) + 2,
                Suit = (Suit)(i / 13)
            };
            pack.Add(card);
        }
        return pack;
    }
    public void ShufflePack()
    {
        List<PlayingCard> deck = _sortedDeck;
        _shuffled = [];
        Random rnd = new();
        while (deck.Count > 0)
        {
            int idx = rnd.Next(deck.Count);
            _shuffled.Add(deck[idx]);
            deck.RemoveAt(idx);
        }
    }
    public string ShuffledString()
    {
        string s = "";
        foreach (var card in _shuffled)
        {
            s += card.ToString() + "\n";
        }
        s += $"size: {_shuffled.Count}";
        return s;
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
