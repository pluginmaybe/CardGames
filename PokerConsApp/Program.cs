using CardGames;
using PokerConsApp;

Player player1 = new(1, "Abe");
Player player2 = new(2, "Betty");
Player player3 = new(3, "Clair");
Player player4 = new(4, "Donna");

PlayingCardDeck deck = new();

TexasHoldem texasHoldem = new();

List<Player> players = [];
players.Add(player1);
players.Add(player2);
players.Add(player3);
players.Add(player4);

texasHoldem.Deal(players);
foreach (Player player  in players)
{
    Console.WriteLine($"{player}");
    foreach (PlayingCard card in player.Hand)
    {
        Console.WriteLine(card);
    }
}
Console.WriteLine("Table");
foreach (PlayingCard card in texasHoldem.GetTableHand())
{ Console.WriteLine(card);}


foreach (Player player in players)
{
    texasHoldem.SetPlayersBestHand(player);
}

List<Player> winners = texasHoldem.GetWinningHands(players);
Console.WriteLine();
Console.WriteLine("><");

texasHoldem.DeclareWinner(winners);

Console.WriteLine("><");
Console.WriteLine();

foreach (Player player in players)
{
    Console.WriteLine($"{player}");
    Console.WriteLine($"{PokerHandHelpers.RateHand(player.BestHoldemHand)}");
    foreach (PlayingCard card in player.BestHoldemHand)
    {
        Console.WriteLine(card);
    }
}
Console.WriteLine();
FiveCard fiveCard = new();

foreach (Player player in players)
{
    player.Hand.Clear();

}
Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++");

fiveCard.Deal(players);

foreach (Player player in players)
{
    Console.WriteLine($"{player}");
    Console.WriteLine($"{PokerHandHelpers.RateHand(player.Hand)}");

    foreach (PlayingCard card in player.Hand)
    {
        Console.WriteLine(card);
    }
}

List<Player> w = fiveCard.GetWinningHands(players);
fiveCard.DeclareWinner(w);
