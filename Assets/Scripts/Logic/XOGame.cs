using System;
using System.Collections.Generic;

public class XOGame
{
    public char[,] Table { get; private set; }
    public Player Player1 { get; private set; }
    public Player Player2 { get; private set; }
    public bool IsEnd { get; private set; }

    private int _amountOfSteps = 0;
    private bool _isDraw;

    // constructor
    public XOGame(Player player1, Player player2)
    {
        this.Table = new char[3, 3];
        this.IsEnd = false;
        this.Player1 = player1;
        this.Player2 = player2;
    }

    // make one move
    public void MakeStep(int x, int y)
    {
        if (x < 0 || x > 2)
            return;

        if (y < 0 || y > 2)
            return;

        if (Table[x, y] != '\0')
            return;

        // fill cell symbol
        char checkingSymbol = _amountOfSteps % 2 == 0 ? Player1.Symbol : Player2.Symbol;
        Table[x, y] = checkingSymbol;

        _amountOfSteps++;

        if (_amountOfSteps < 5)
            return;

        // checking horizontal x
        if (Table[x, 0] == checkingSymbol && Table[x, 1] == checkingSymbol && Table[x, 2] == checkingSymbol)
        {
            IsEnd = true;
            return;
        }

        // checking vertical y
        if (Table[0, y] == checkingSymbol && Table[1, y] == checkingSymbol && Table[2, y] == checkingSymbol)
        {
            IsEnd = true;
            return;
        }

        // checking diagonals
        if (x == 1 && y == 1)
        {
            if (Table[0, 0] == checkingSymbol && Table[1, 1] == checkingSymbol && Table[2, 2] == checkingSymbol)
            {
                IsEnd = true;
                return;
            }

            if (Table[0, 2] == checkingSymbol && Table[1, 1] == checkingSymbol && Table[2, 0] == checkingSymbol)
            {
                IsEnd = true;
                return;
            }
        }
        else if ((x == 0 && y == 0) || (x == 2 && y == 2))
        {
            if (Table[0, 0] == checkingSymbol && Table[1, 1] == checkingSymbol && Table[2, 2] == checkingSymbol)
            {
                IsEnd = true;
                return;
            }
        }
        else if ((x == 0 && y == 2) || (x == 2 && y == 0))
        {
            if (Table[0, 2] == checkingSymbol && Table[1, 1] == checkingSymbol && Table[2, 0] == checkingSymbol)
            {
                IsEnd = true;
                return;
            }
        }

        // ckeck filling all grid
        if (_amountOfSteps >= 3 * 3)
        {
            IsEnd = true;
            _isDraw = true;
        }
    }

    // get cell - random from all free cell
    public int GetRandomFreeCell()
    {
        Random random = new Random();
        int index = 0;
        List<int> list = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Table[i, j] == '\0')
                    list.Add(index);

                index++;
            }
        }

        return list[random.Next(0, list.Count)];
    }

    // get the player won
    public Player GetWinner()
    {
        if (IsEnd)
        {
            if (!_isDraw)
            {
                return (_amountOfSteps % 2) == 0 ? Player2 : Player1;
            }
        }

        return null;
    }

    // check whether the cell is empty
    public bool IsFreeCell(int index)
    {
        int x = index / 3;
        int y = index % 3;

        return (Table[x, y] == '\0');
    }
}
