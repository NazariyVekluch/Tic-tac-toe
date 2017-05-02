using UnityEngine;

public class GameCenter : MonoBehaviour {

    public static GameCenter Instance;

    public static int WinCounterPlayer1;
    public static int WinCounterPlayer2;
    public static string Name;
    public static char PlayerSymbol = 'X';
    public static char OpponentSymbol = 'O';

    public Color ColorSymbolForPlayer1;
    public Color ColorSymbolForPlayer2;

    public XOGame _xoGame { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    void Start () {
        _xoGame = new XOGame(new Player(Name,      ColorSymbolForPlayer1, PlayerSymbol),
                             new Player("Opponent", ColorSymbolForPlayer2, OpponentSymbol));
    }
}
