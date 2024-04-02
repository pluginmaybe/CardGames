using CardGames;

namespace PokerConsApp;
public class TexasHoldem : IPokerGame
{
    readonly PlayingCardDeck _deck = new();
    readonly Player _table = new(99, "Table");
    
    public List<PlayingCard> GetTableHand()
    {
        return _table.Hand;
    }

    public void SetPlayersBestHand(Player player)
    {
        // TODO Assumes table hand is fully dealt. Be mindful, or tweak, if using to show players before
        List<PlayingCard> best = _table.Hand;
        List<PlayingCard> current = [];
        
        for (int i = 0; i < 7; i++)
        {
            for (int j = i + 1; j < 7; j++)
            {
                // TODO check if method of copy is correct
                current = _table.Hand.ToList();
                current.AddRange(player.Hand);
                // remove later index first
                current.RemoveAt(j);
                current.RemoveAt(i);
                current = PokerHandHelpers.SortHand(current);
                if (PokerHandHelpers.CompareHand(current, best) == 1)
                {
                    best = current.ToList();
                }
            }
        }
        player.BestHoldemHand = best;
    }
    public List<Player> GetWinningHands(List<Player> players)
    {
        List<Player> bestHands = [];
        foreach (Player player in players)
        {
            if (player.Fold) continue;
            if (bestHands.Count == 0)
            {
                bestHands.Add(player);
                continue;
            }
            int n = PokerHandHelpers.CompareHand(player.BestHoldemHand, bestHands[0].BestHoldemHand);
            if (n == 1)
            {
                bestHands.Clear();
                bestHands.Add(player);
            }
            else if (n == 0)
            {
                bestHands.Add(player);
            }
        }
        return bestHands;
    }

    public void Deal(List<Player> players)
    {
        _deck.GetShuffledPack();
        for (int i = 0; i < 2; i++)
        {
            foreach (Player p in players)
            {
                p.Hand.Add(_deck.GetCard(false));
            }
        }
        for (int i = 0;i < 5; i++)
            _table.Hand.Add(_deck.GetCard(false));
        _table.Hand = PokerHandHelpers.SortHand(_table.Hand);
        foreach (Player p in players)
            p.Fold = false;
    }

    public void DeclareWinner(List<Player> winners)
    {
        if (winners.Count == 0)
        {
            Console.WriteLine("No Winners");
        }
        else if (winners.Count == 1)
        {
            Console.WriteLine($"{winners.First()} Won!!");
        }
        else
        {
            Console.WriteLine("Winners are :");
            foreach (Player player in winners)
            {
                Console.WriteLine(player.ToString());
            }
        }
    }
}
