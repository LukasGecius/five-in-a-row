public static class Stats
{
    public static int boardSize;

    public static float[] CheckerV2posX
    {
        get
        {
            return CheckerV2posX;
        }
        set
        {
            for (int i = 0; i < boardSize; i++)
            {
                CheckerV2posX[i]
                
            }
            
        }
    }
    public static float[] CheckerV2posY
    {
        get
        {
            return CheckerV2posY;
        }
        set
        {
            for (int i = 0; i < boardSize; i++)
            {
                CheckerV2posY[i] = value;

            }
        }
    }
}