using System.Collections.Generic;
using UnityEngine;

public abstract class Cell {
    // 0 = Dead, 1 = Alive, 
    public float State;
    public Cell[,] Map;
    public List<Cell> Neighbourgs = new List<Cell>();

    protected Vector2Int Coordinate {
        get {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (Map != null) {
                        if (Map[i, j] == this)
                            return new Vector2Int(i, j);
                    }
                    else {
                        if (DataManager.Map[i, j] == this)
                            return new Vector2Int(i, j);
                    }
                }
            }

            return -Vector2Int.one;
        }
    }
    
    public int X => Coordinate.x;
    public int Y => Coordinate.y;

    public abstract float NextState();
}