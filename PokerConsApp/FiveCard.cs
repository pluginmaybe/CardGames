using CardGames;

namespace PokerConsApp;
public class FiveCard : IPokerGame
{
    readonly PlayingCardDeck _deck = new();
    public void Deal(List<Player> players)
    {
        _deck.GetShuffledPack();
        for (int i = 0; i < 5; i++)
        {
            foreach (Player p in players)
            {
                p.Hand.Add(_deck.GetCard(false));
            }
        }
        foreach (Player p in players)
            p.Fold = false;
    }

    public void ReplaceCards(Player player)
    {
        List<PlayingCard> cardsToDrop = GetCardsToDrop(player);
        while (cardsToDrop.Count > 0)
        {
            PlayingCard cardToRemove = cardsToDrop.First();
            cardsToDrop.Remove(cardToRemove);
            player.Hand.Remove(cardToRemove);
            player.Hand.Add(_deck.GetCard(false));
        }
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
            int n = PokerHandHelpers.CompareHand(player.Hand, bestHands[0].Hand);
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
    List<PlayingCard> GetCardsToDrop(Player player)
    {
        List<PlayingCard> hand = player.Hand;
        List<PlayingCard> ChosenCards = [];
        while (true)
        {
            Console.Clear();
            int i = 1;
            foreach (PlayingCard h in hand)
            {
                string t = "";
                if (ChosenCards.Contains(h))
                    t = "X";
                Console.WriteLine($"{t}{i} : {h}");
                i++;
            }
            Console.WriteLine("Enter card number to remove (0 to end / F to fold) : ");
            string num = Console.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(num) && num.ToLower().StartsWith('f'))
            {
                player.Fold = true;
            }
            if (!int.TryParse(num, out int n) || (n < 0 || n > hand.Count))
            {
                Console.WriteLine("Invalid input : Press A key to try again");
                Console.ReadKey();
            };
            if (n == 0) break;
            if (ChosenCards.Contains(hand[n]))
            {
                ChosenCards.Remove(hand[n]);
            }
            else
            {
                ChosenCards.Add(hand[n]);
            }
        }
        return ChosenCards;
    }
}
