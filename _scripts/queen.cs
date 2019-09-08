using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queen : Chessman
{
    public override bool[,] PossibleMove(Chessman[,] Chessmans, int playerTurn, int[] b, int[] yel, int[] red, int[] g)
    {
        int x = CurrentX;
        int y = CurrentY;

        //if(!eval)
        //{
        //    Chessmans = manager.Chessmans;
        //}
        //if(eval)
        //{
        //    Chessmans = eval.Chessmans;
        //}

        bool[,] r = new bool[12, 12];

        Chessman c;
        int i, j;

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
        } // END rookish movements

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
        } //END Queen movements

        return r;
    }
}
