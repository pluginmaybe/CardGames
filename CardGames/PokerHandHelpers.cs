using CardGames.Enums;

namespace CardGames;
public static partial class PokerHandHelpers
{
    public static List<PlayingCard> SortHand(List<PlayingCard> hand)
    {
        hand = [.. hand.OrderByDescending(x => x.Value)];
        return hand;
    }
    /// <summary>
    /// Compares two poker hands
    /// </summary>
    /// <param name="hand1"></param>
    /// <param name="hand2"></param>
    /// <returns>
    /// 1 or 2 if one hand is better, or
    /// 0 if both hands equal
    /// </returns>
    public static int CompareHand(List<PlayingCard > hand1, List<PlayingCard> hand2)
    {
        (Enum hand1value, int h1Score) = RateHand(hand1);
        (Enum hand2value, int h2Score) = RateHand(hand2);
        if (hand1value.CompareTo(hand2value) > 0)
            return 1;
        if (hand1value.CompareTo(hand2value) < 0)
            return 2;
        if (h1Score > h2Score)
            return 1;
        if (h1Score < h2Score)
            return 2;
        return 0;
    }

    public static (Enum, int) RateHand(List<PlayingCard> hand)
    {
        if (CheckRoyalFlush(hand))
            return (HandRating.RoyalFlush, 0);
        if (CheckFlush(hand) && CheckStraight(hand))
            return (HandRating.StraightFlush, NumericHandValue(hand));
        if (CheckFourOfAKind(hand) != -1)
            return (HandRating.FourOfAKind, CheckFourOfAKind(hand));
        if (CheckFullHouse(hand) != -1)
            return (HandRating.FullHouse, CheckThreeOfAKind(hand));
        if (CheckFlush(hand))
            return (HandRating.Flush, NumericHandValue(hand));
        if (CheckStraight(hand))
            return (HandRating.Straight, NumericHandValue(hand));
        if (CheckThreeOfAKind(hand) != -1)
            return (HandRating.ThreeOfAKind, CheckThreeOfAKind(hand));
        (int pairOne, List<PlayingCard> remaining) = CheckTwoOfAKind(hand);
        if (pairOne != -1)
        {
            (int pairTwo, List<PlayingCard> rem) = CheckTwoOfAKind(remaining);
            if (pairTwo != -1)
            {
                int val;
                if (Math.Max(pairOne, pairTwo) == pairOne)
                    val = pairOne * 10000 + pairTwo * 100;
                else
                    val = pairTwo * 10000 + pairOne * 100;
                return (HandRating.TwoPair, val + NumericHandValue(rem));
            }
            return (HandRating.Pair, (pairOne * 1000000 + NumericHandValue(remaining)));
        }
        //
        return (HandRating.HighCard, NumericHandValue(hand));
    }
    public static int CompareCard(PlayingCard card1, PlayingCard card2)
    {
        if (card1.Value > card2.Value) return 1;
        if (card1.Value < card2.Value) return -1;
        return 0;
    }
    public static bool CheckRoyalFlush(List<PlayingCard> hand)
    {
        if (CheckFlush(hand) && CheckStraight(hand) && hand[0].Value == 14)
        {
            return true;
        }
        return false;
    }
    public static bool CheckFlush(List<PlayingCard> hand)
    {
        foreach (int suit in Enum.GetValues(typeof(Suit)))
        {
            if (hand.All(x => (int)x.Suit == suit))
                return true;
        }
        return false;
    }
    public static bool CheckStraight(List<PlayingCard> hand)
    {
        List<int> values = [];
        foreach (var h in hand)
        {
            values.Add(h.Value);
        }
        // Allow for ace to be either high or low
        if (values[0] == 14 && values[1] != 13)
            values[0] = 1;
        for (int i = 0; i < (values.Count - 1); i++)
        {
            if (values[i] != values[i + 1] + 1)
                return false;
        }
        return true;
    }
    public static int CheckFourOfAKind(List<PlayingCard> hand)
    {
        return CheckForMultiples(hand, 4);
    }
    public static int CheckFullHouse(List<PlayingCard> hand)
    {
        int three = CheckForMultiples(hand, 3);
        if (three != -1)
        {
            int n = 0;
            foreach (var h in hand)
            {
                if (h.Value == three) continue;
                if (n != 0 && n == h.Value)
                    return three;
                n = h.Value;
            }
        }
        return -1;
    }
    public static int CheckThreeOfAKind(List<PlayingCard> hand)
    {
        return CheckForMultiples(hand, 3);
    }
    public static (int, List<PlayingCard>) CheckTwoOfAKind(List<PlayingCard> hand)
    {
        int two = CheckForMultiples(hand, 2);
        List<PlayingCard> remaining = [];
        if (two != -1)
        {
            foreach (var h in hand)
            {
                if (h.Value == two) continue;
                remaining.Add(h);
            }
        }
        return (two, remaining);
    }
  
    private static int CheckForMultiples(List<PlayingCard> hand, int goal)
    {
        Dictionary<int, int> ValueCount = [];
        for (int i = 0; i < hand.Count; i++)
        {
            if (ValueCount.TryGetValue(hand[i].Value, out int value))
                ValueCount[hand[i].Value] = ++value;
            else
                ValueCount[hand[i].Value] = 1;
        }
        foreach (var (k, v) in ValueCount)
        {
            if (v == goal)
                return k;
        }
        return -1;
    }
    /// <summary>
    /// Creates a value for the hand, used to split a tie
    /// between hands with the same HandRating
    /// e.g. 
    /// a hand of J, 9, 5, 3, 2
    ///  return a value of 11 09 05 03 02
    /// !NOTE! Works for 5 cards max. Attempting to use for
    /// larger hands could result in overflow
    /// </summary>
    /// <param name="hand"></param>
    /// <returns>the value as an int</returns>
    private static int NumericHandValue(List<PlayingCard> hand)
    {
        int value = 0;
        int i = 0;
        while (true)
        {
            value += hand[i].Value;
            i++;
            if (i == hand.Count) break;
            value *= 100;
        }
        return value;
    }
}
