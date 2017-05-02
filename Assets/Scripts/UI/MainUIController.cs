using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainUIController : UIController
{
    public static MainUIController Instance;

    private Text[] textCells;
    private GameObject Canvas;
    private Transform Grid;
    private GameObject EndPanel;
    private Text EndGameText;
    private Text ResultsText;
    private bool isBlockPress;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }

    // initializing fields
    void Start()
    {
        Grid = GameObject.FindGameObjectWithTag("Grid").transform;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");

        // find ui elements
        Transform[] gos = Canvas.GetComponentsInChildren<Transform>(true);
        foreach (var trans in gos)
        {
            if (trans.tag == "EndPanel")
            {
                EndPanel = trans.gameObject;
            }
            else if (trans.tag == "EndGameText")
            {
                EndGameText = trans.GetComponent<Text>();
            }
            else if (trans.tag == "ResultsText")
            {
                ResultsText = trans.GetComponent<Text>();
            }
        }

        // filling grid
        textCells = new Text[Grid.childCount];
        for (int i = 0; i < Grid.childCount; i++)
        {
            textCells[i] = Grid.GetChild(i).GetComponentInChildren<Text>();
            textCells[i].text = "";
        }

        FillWinningInfo();
    }

    // make player step
    private void MakeStep(int index, Player player)
    {
        textCells[index].text = player.Symbol.ToString();
        textCells[index].color = player.SymbolColor;

        int x = index / 3;
        int y = index % 3;

        GameCenter.Instance._xoGame.MakeStep(x, y);

        if (GameCenter.Instance._xoGame.IsEnd)
            StartCoroutine(ShowResult());
    }

    // press cell of grid and make pair moves
    public void MakePlayerStep(int index)
    {
        if (isBlockPress)
            return;

        if (!GameCenter.Instance._xoGame.IsFreeCell(index))
            return;

        isBlockPress = true;

        // make player step
        MakeStep(index, GameCenter.Instance._xoGame.Player1);

        // make oppponent step
        if (!GameCenter.Instance._xoGame.IsEnd)
            StartCoroutine(OpponentStep());
    }

    // make opponent step
    private IEnumerator OpponentStep()
    {
        yield return new WaitForSeconds(0.5f);
        int oppIndex = GameCenter.Instance._xoGame.GetRandomFreeCell();
        MakeStep(oppIndex, GameCenter.Instance._xoGame.Player2);

        if (!GameCenter.Instance._xoGame.IsEnd)
            isBlockPress = false;
    }

    // show result of game
    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(0.5f);

        Player winner = GameCenter.Instance._xoGame.GetWinner();

        if (winner != null)
        {
            EndGameText.text = string.Format("Won\n<color=#{0}>{1}\n{2}</color>",
                ColorUtility.ToHtmlStringRGB(winner.SymbolColor),
                winner.Symbol.ToString(),
                winner.Name == string.Empty ? "Player1" : winner.Name);

            if (winner == GameCenter.Instance._xoGame.Player1)
                GameCenter.WinCounterPlayer1++;
            else
                GameCenter.WinCounterPlayer2++;

            FillWinningInfo();
        }
        else
        {
            EndGameText.text = "No winner";
        }

        EndPanel.SetActive(true);
    }

    // fill winnings statistics on top panel
    private void FillWinningInfo()
    {
        ResultsText.text = string.Format("<color=#{0}>{1}</color> {2}:{3} <color=#{4}>{5}</color>",
            ColorUtility.ToHtmlStringRGB(GameCenter.Instance._xoGame.Player1.SymbolColor),
            GameCenter.Instance._xoGame.Player1.Symbol.ToString(),
            GameCenter.WinCounterPlayer1,
            GameCenter.WinCounterPlayer2,
            ColorUtility.ToHtmlStringRGB(GameCenter.Instance._xoGame.Player2.SymbolColor),
            GameCenter.Instance._xoGame.Player2.Symbol.ToString());
    }

    // restart game from end panel
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}