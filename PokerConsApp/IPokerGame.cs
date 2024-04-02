
namespace PokerConsApp;

public interface IPokerGame
{
    List<Player> GetWinningHands(List<Player> players);
    void Deal(List<Player> players);
    void DeclareWinner(List<Player> winners);
}