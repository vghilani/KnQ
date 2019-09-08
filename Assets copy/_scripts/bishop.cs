using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bishop : Chessman
{
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
        Chessman c;
        int i, j;

        //TOP LEFT
        i = x;
        j = y;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= 12)
                break;

            c = Chessmans[i, j];

            if (c == null)
                r[i, j] = true;
            else if(Chessmans[x, y].isBlue)
            {
                if(!c.isBlue)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isYellow)
            {
                if (!c.isYellow)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isRed)
            {
                if (!c.isRed)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isGreen)
            {
                if (!c.isGreen)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
        } // END TOP LEFT

        //TOP RIGHT
        i = x;
        j = y;
        while (true)
        {
            i++;
            j++;
            if (i >= 12 || j >= 12)
                break;

            c = Chessmans[i, j];

            if (c == null)
                r[i, j] = true;
            else if (Chessmans[x, y].isBlue)
            {
                if (!c.isBlue)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isYellow)
            {
                if (!c.isYellow)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isRed)
            {
                if (!c.isRed)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isGreen)
            {
                if (!c.isGreen)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
        } //END TOP RIGHT

        //DOWN LEFT
        i = x;
        j = y;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0)
                break;

            c = Chessmans[i, j];

            if (c == null)
                r[i, j] = true;
            else if (Chessmans[x, y].isBlue)
            {
                if (!c.isBlue)
                {
                    r[i, j] = true;
                }
               break; //RIGHT?
            }
            else if (Chessmans[x, y].isYellow)
            {
                if (!c.isYellow)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isRed)
            {
                if (!c.isRed)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isGreen)
            {
                if (!c.isGreen)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
        } //END DOWN LEFT

        //DOWN RIGHT
        i = x;
        j = y;
        while (true)
        {
            i++;
            j--;
            if (i >= 12 || j < 0)
                break;

            c = Chessmans[i, j];

            if (c == null)
                r[i, j] = true;
            else if (Chessmans[x, y].isBlue)
            {
                if (!c.isBlue)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isYellow)
            {
                if (!c.isYellow)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isRed)
            {
                if (!c.isRed)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
            else if (Chessmans[x, y].isGreen)
            {
                if (!c.isGreen)
                {
                    r[i, j] = true;
                }
                break; //RIGHT?
            }
        } //END DOWN RIGHT

        return r;
    }
}
