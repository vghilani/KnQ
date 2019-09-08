using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rook : Chessman
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
        int i;

        //RIGHT
        i = x;
        while (true)
        {
            i++;
            if (i >= 12)
                break;

            c = Chessmans[i, y];
            if (c == null)
                r[i, y] = true;
            else
            {
                if (Chessmans[x, y].isBlue)
                {
                    if (!c.isBlue)
                        r[i, y] = true;

                    break;
                }
                if (Chessmans[x, y].isYellow)
                {
                    if (!c.isYellow)
                        r[i, y] = true;

                    break;
                }
                if (Chessmans[x, y].isRed)
                {
                    if (!c.isRed)
                        r[i, y] = true;

                    break;
                }
                if (Chessmans[x, y].isGreen)
                {
                    if (!c.isGreen)
                        r[i, y] = true;

                    break;
                }
            }
        } //END 

        //LEFT
        i = x;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = Chessmans[i, y];
            if (c == null)
                r[i, y] = true;
            else
            {
                if (Chessmans[x, y].isBlue)
                {
                    if (!c.isBlue)
                        r[i, y] = true;

                    break;
                }
                if (Chessmans[x, y].isYellow)
                {
                    if (!c.isYellow)
                        r[i, y] = true;

                    break;
                }
                if (Chessmans[x, y].isRed)
                {
                    if (!c.isRed)
                        r[i, y] = true;

                    break;
                }
                if (Chessmans[x, y].isGreen)
                {
                    if (!c.isGreen)
                        r[i, y] = true;

                    break;
                }
            }
        } // END

        // UP
        i = y;
        while (true)
        {
            i++;
            if (i >= 12)
                break;

            c = Chessmans[x, i];
            if (c == null)
                r[x, i] = true;
            else
            {
                if (Chessmans[x, y].isBlue)
                {
                    if (!c.isBlue)
                        r[x, i] = true;

                    break;
                }
                if (Chessmans[x, y].isYellow)
                {
                    if (!c.isYellow)
                        r[x, i] = true;

                    break;
                }
                if (Chessmans[x, y].isRed)
                {
                    if (!c.isRed)
                        r[x, i] = true;

                    break;
                }
                if (Chessmans[x, y].isGreen)
                {
                    if (!c.isGreen)
                        r[x, i] = true;

                    break;
                }
            }
        } // END

        // DOWN
        i = y;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = Chessmans[x, i];
            if (c == null)
                r[x, i] = true;
            else
            {
                if (Chessmans[x, y].isBlue)
                {
                    if (!c.isBlue)
                        r[x, i] = true;

                    break;
                }
                if (Chessmans[x, y].isYellow)
                {
                    if (!c.isYellow)
                        r[x, i] = true;

                    break;
                }
                if (Chessmans[x, y].isRed)
                {
                    if (!c.isRed)
                        r[x, i] = true;

                    break;
                }
                if (Chessmans[x, y].isGreen)
                {
                    if (!c.isGreen)
                        r[x, i] = true;

                    break;
                }
            }
        } // END

        return r;
    }
}
