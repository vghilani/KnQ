using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knight : Chessman
{
    private void Update()
    {
        if (isBlue)
        {
            Quaternion orientation = Quaternion.Euler(-90, 315, 0);
            transform.rotation = orientation;
        }
        if (isYellow)
        {
            Quaternion orientation = Quaternion.Euler(-90, 45, 0);
            transform.rotation = orientation;
        }
        if (isRed)
        {
            Quaternion orientation = Quaternion.Euler(-90, 135, 0);
            transform.rotation = orientation;
        }
        if (isGreen)
        {
            Quaternion orientation = Quaternion.Euler(-90, 225, 0);
            transform.rotation = orientation;
        }
    }


    public override bool[,] PossibleMove(Chessman[,] Chessmans, int playerTurn, int[] b, int[] yel, int[] red, int[] g)
    {
        int x = CurrentX;
        int y = CurrentY;

        //if (eval == null)
        //{
        //    Chessmans = manager.Chessmans;
        //}
        //else
        //{
        //    Chessmans = eval.Chessmans;
        //}

        bool[,] r = new bool[12, 12];


        KnightMoves(x - 1, y + 2, ref r, Chessmans);
        //up and to the right
        KnightMoves(x + 1, y + 2, ref r, Chessmans);
        //right and then up
        KnightMoves(x + 2, y + 1, ref r, Chessmans);
        //right and then down
        KnightMoves(x + 2, y - 1, ref r, Chessmans);
        //down and to the left
        KnightMoves(x - 1, y - 2, ref r, Chessmans);
        //down and to the right
        KnightMoves(x + 1, y - 2, ref r, Chessmans);
        //left and then up
        KnightMoves(x - 2, y + 1, ref r, Chessmans);
        //left and then down
        KnightMoves(x - 2, y - 1, ref r, Chessmans);


        return r;
    }

    public void KnightMoves(int x, int y, ref bool[,] r, Chessman[,] Chessmans)
    {
        Chessman c; 
        if(x >= 0 && x < 12 && y >= 0 && y < 12)
        {
            c = Chessmans[x, y];
            if (c == null)
                r[x, y] = true;
            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                    r[x, y] = true;              
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x, y] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x, y] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x, y] = true;
            }
        }
    }



} //END OF FILE
