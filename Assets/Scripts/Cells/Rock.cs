public class Rock : Cell {
    
    public override float NextState() {
        if (X == 0 && Y == 0) return 0;
        if (X == 0 && Y == Map.Length - 1) return 0;
        if (Y == 0 && X == Map.Length - 1) return 0;
        if (Y == Map.Length - 1 && X == Map.Length - 1) return 0;
        return 1;
    }
}