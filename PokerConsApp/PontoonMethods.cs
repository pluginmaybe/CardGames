using CardGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConsApp;
public static class PontoonMethods
{
    public static int ShowScore(Player player)
    {
        Console.WriteLine(player.ToString());
        foreach (PlayingCard card in player.Hand)
        {
            Console.WriteLine(card.ToString());
        }
        int score = PontoonHelpers.HandScore(player.Hand);
        return score;
    }
    public static int CompareScores(int p1Score, int p2Score)
    {
        if (HasScoreBust(p1Score)) p1Score = 0;
        if (HasScoreBust(p2Score)) p2Score = 0;
        return p1Score - p2Score;
    }
    public static bool HasScoreBust(int score)
    {
        return score > 21;
    }
}
