using UnityEngine;

public class Player {
    
    public string Name { get; private set; }
    public Color SymbolColor { get; private set; }
    public char Symbol { get; private set; }

    public Player(string name, Color playerType, char symbol)
    {
        this.Name = name;
        this.SymbolColor = playerType;
        this.Symbol = symbol;
    }
}
