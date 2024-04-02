using CardGames;

namespace PokerConsApp;
public static class PontoonDemoMain
{
    public static void Demo()
    {
        Player player1 = new(1, "Abe");
        Player player2 = new(2, "Betty");

        PlayingCardDeck deck = new();
        deck.ShufflePack();

        for (int i = 0; i < 2; i++)
        {
            player1.Hand.Add(deck.GetCard(false));
            player2.Hand.Add(deck.GetCard(false));
        }
        int p1Score = PontoonMethods.ShowScore(player1);

        Console.WriteLine($"Score : {p1Score}");
        while (p1Score < 16)
        {
            Console.WriteLine("Twist");
            player1.Hand.Add(deck.GetCard(false));
            p1Score = PontoonMethods.ShowScore(player1);
            Console.WriteLine($"Score : {p1Score}");
        }
        Console.WriteLine("***\n");
        int p2Score = PontoonMethods.ShowScore(player2);
        Console.WriteLine($"Score : {p2Score}");
        while (p2Score < 16)
        {
            Console.WriteLine("Twist");
            player2.Hand.Add(deck.GetCard(false));
            p2Score = PontoonMethods.ShowScore(player2);
            Console.WriteLine($"Score : {p2Score}");
        }
        Console.WriteLine("***");

        int winner = PontoonMethods.CompareScores(p1Score, p2Score);
        if (winner > 0) Console.WriteLine("Player One Wins");
        else if (winner < 0) Console.WriteLine("Player Two Wins");
        else Console.WriteLine("A Draw");
    }
}
