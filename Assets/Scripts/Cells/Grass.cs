public class Grass : Cell {
  
    public override float NextState() {
        if(Map[X + 1, Y] != null && Map[X + 1, Y].State == 1) Neighbourgs.Add(Map[X + 1, Y]);
        if(Map[X - 1, Y] != null && Map[X - 1, Y].State == 1) Neighbourgs.Add(Map[X - 1, Y]);
        if(Map[X - 1, Y + 1] != null && Map[X - 1, Y + 1].State == 1) Neighbourgs.Add(Map[X - 1, Y + 1]);
        if(Map[X + 1, Y + 1] != null && Map[X + 1, Y + 1].State == 1) Neighbourgs.Add(Map[X + 1, Y + 1]);
        if(Map[X, Y + 1] != null && Map[X, Y + 1].State == 1) Neighbourgs.Add(Map[X, Y + 1]);
        if(Map[X, Y - 1] != null && Map[X, Y - 1].State == 1) Neighbourgs.Add(Map[X, Y - 1]);

        int nbrg = Neighbourgs.Count;
        if (nbrg <= 1) return 0;
        if (nbrg > 1) return 1;
        return default;
    }
}