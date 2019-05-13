using UnityEditor;
public static class Stats
{
    public static int boardSize;

    //Checker movement statements
    public static bool goCheckerRow = false;
    public static bool goCheckerColm = false;
    public static bool goCheckerDR = false;
    public static bool goCheckerDL = false;
    public static bool goMoveRow = false;
    public static bool goMoveCol = false;
    public static bool goMoveDR = false;
    public static bool goMoveDL = false;
    public static bool stopped = false;
    public static int moveCount = 0;
    public static bool moveMade = false;
    public static bool dangerNotFound;
    public static bool shootPopSound = false;

}