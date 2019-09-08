using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isBlue = false;
    public bool isGreen = false; 
    public bool isRed = false;
    public bool isYellow = false;

    //these are needed for online data.....
    public string Name = "";
    public int index = 0;
    public int SaveX; //x and y are set when the User sends their move to the server.
    public int SaveY;

    //this will be used on every non-pawn as well to determine if they moved from their starting position. Caselting uses it too..
    public int pawnDirection = 0; // 0 is undecided, 1 is left, 2 is right.  This integer will have to change when a pawn commits to a direction.

    public bool isChecked = false; //for leaders
    public bool isMated = false;
    public int Rank = 0;

    public void SetPosition (int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public int getColor()
    {
        if (isBlue)
        {
            return 1;
        }
        if (isYellow)
        {
            return 2;
        }
        if (isRed)
        {
            return 3;
        }
        if (isGreen)
        {
            return 4;
        }
        return 0;
    }

    public virtual bool[,] PossibleMove(Chessman[,] Chessmans, int playerTurn, int[] b, int[] yel, int[] red, int[] g)
    {
        return new bool [12,12];
    }
}
