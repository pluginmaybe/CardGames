namespace CardGames;
public static class PontoonHelpers
{
    public static int RateCard(PlayingCard card)
    {
        if (card.Value == 14) return 0;
        if (card.Value > 10) return 10;
        return card.Value;
    }

    public static int HandScore(List<PlayingCard> hand)
    {
        int score = 0;
        foreach (PlayingCard card in hand)
        {
            int cardValue = RateCard(card);
            if (cardValue == 0)
            {
                if (score < 11) cardValue = 11;
                else cardValue = 1;
            }
            score += cardValue;
        }
        return score;
    }
 
}
