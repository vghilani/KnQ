using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawn : Chessman
{
    private void Update()
    {
        if (pawnDirection == 0 || pawnDirection == -1)
        {
            if (isBlue)
            {
                Quaternion orientation = Quaternion.Euler(0, 155, 0);
                transform.rotation = orientation;
            }
            if (isYellow)
            {
                Quaternion orientation = Quaternion.Euler(0, 245, 0);
                transform.rotation = orientation;
            }
            if (isRed)
            {
                Quaternion orientation = Quaternion.Euler(0, 335, 0);
                transform.rotation = orientation;
            }
            if (isGreen)
            {
                Quaternion orientation = Quaternion.Euler(0, 65, 0);
                transform.rotation = orientation;
            }
        }
            if (pawnDirection == 1)
            {
                if (isBlue)
                {
                    Quaternion orientation = Quaternion.Euler(0, 110, 0);
                    transform.rotation = orientation;
                }
                if (isYellow)
                {
                    Quaternion orientation = Quaternion.Euler(0, 200, 0);
                    transform.rotation = orientation;
                }
                if (isRed)
                {
                    Quaternion orientation = Quaternion.Euler(0, 290, 0);
                    transform.rotation = orientation;
                }
                if (isGreen)
                {
                    Quaternion orientation = Quaternion.Euler(0, 15, 0);
                    transform.rotation = orientation;
                }
        }
        if (pawnDirection == 2)
        {
            if (isBlue)
            {
                Quaternion orientation = Quaternion.Euler(0, 200, 0);
                transform.rotation = orientation;
            }
            if (isYellow)
            {
                Quaternion orientation = Quaternion.Euler(0, 290, 0);
                transform.rotation = orientation;
            }
            if (isRed)
            {
                Quaternion orientation = Quaternion.Euler(0, 15, 0);
                transform.rotation = orientation;
            }
            if (isGreen)
            {
                Quaternion orientation = Quaternion.Euler(0, 110, 0);
                transform.rotation = orientation;
            }
        }

    }

    //default pawn direction is 0.
    public override bool[,] PossibleMove(Chessman[,] Chessmans, int playerTurn, int[] b, int[] yel, int[] red, int[] g)
    {
        int x = CurrentX;
        int y = CurrentY;

        //if (eval == null)
        //{
        //    Chessmans = manager.Chessmans;
        //    b = manager.EnPassantMoveBlue;
        //    yel = manager.EnPassantMoveYellow;
        //    red = manager.EnPassantMoveRed;
        //    g = manager.EnPassantMoveGreen;
        //}
        //else
        //{
        //    Chessmans = eval.Chessmans;
        //    b = eval.EnPassantMoveBlue;
        //    yel = eval.EnPassantMoveYellow;
        //    red = eval.EnPassantMoveRed;
        //    g = eval.EnPassantMoveGreen;
        //}

        bool[,] r = new bool[12, 12]; //variable that sweeps the board and collects availible moves
        Chessman c, c2; //used for checking for other pieces..

        //BLUE PAWN MOVEMENT 
        if (Chessmans[x, y].isBlue) //IF THE PAWN IS BLUE....
        {
            if (Chessmans[x, y].pawnDirection == 0) //if its the first move; the player has not chosen their direction
            {
                //diag left
                if(x != 0 && y != 11) //if the blue pawn is not in a position where diag left can't be read.
                {
                    c = Chessmans[x - 1, y + 1]; //diag left is left on x and up on y for blue, and only from start
                    if (c != null && !c.isBlue) //if c was actually applied to something, and it is not blue.
                        r[x - 1, y + 1] = true; //then it is an active move, whether there is an enemy there or whether it is empty.
                }
                //diag middle
                if(x != 11 && y != 11)
                {
                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y + 1] = true;
                }
                //diag right
                if(x != 11 && y != 0)
                {
                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y - 1] = true;
                }
                //forward up
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y + 1] = true;
                }
                //forward right
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    if (c == null)
                        r[x + 1, y] = true;
                }
                //forward on up on first
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    c2 = Chessmans[x, y + 2];
                    if (c == null & c2 == null)
                        r[x, y + 2] = true;
                }
                //forward right on first
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    c2 = Chessmans[x + 2, y];
                    if (c == null & c2 == null)
                        r[x + 2, y] = true;
                }
            }

            //for blue, 2 goes from left to right
            if (Chessmans[x, y].pawnDirection == 2) // COMMITS TO THE RIGHT PATH
            {
                //diag left (right orient)
                if(x != 11 && y != 11)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y + 1] = true;
                }
                //diag right (right orient)
                if (y != 0 &&x!= 11)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y - 1] = true;
                }
                //middle (right orient)
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    if (c == null)
                        r[x + 1, y] = true;
                }

                //end of board code needed.
            }

            //for blue, 1 goes from down to up
            if (Chessmans[x, y].pawnDirection == 1) // COMMITS TO THE LEFT PATH
            {
                //diag left (left orient)
                if(x != 0 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    //if you can enpassant red
                    if (red[0] ==x- 1 && red[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x - 1, y + 1] = true;
                }
                //diag right (left orient)
                if(x != 11 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x+ 1 && yel[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y + 1] = true;
                }
                //middle
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null)
                        r[x, y + 1] = true;
                }
                //getting to the end of board code needed - prisoner exchange
            }

            if (Chessmans[x, y].pawnDirection == -1) //if they went diag their first turn, their uncommited, but unable to move two spaces...
            {
                //diag left
                if(x != 0 && y != 11) //if the blue pawn is not in a position where diag left can't be read.
                {
                    c = Chessmans[x - 1, y + 1]; //diag left is left on x and up on y for blue, and only from start
                    if (c != null && !c.isBlue) //if c was actually applied to something, and it is not blue.
                        r[x - 1, y + 1] = true; //then it is an active move, whether there is an enemy there or whether it is empty.
                }
                //diag middle
                if(x != 11 && y != 11)
                {
                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y + 1] = true;
                }
                //diag right
                if(x != 11 && y != 0)
                {
                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y - 1] = true;
                }
                //forward up
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y + 1] = true;
                }
                //forward right
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    if (c == null)
                        r[x + 1, y] = true;
                }

                //diag left (right orient)
                if(x != 11 && y != 11)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y + 1] = true;
                }
                //diag right (right orient)
                if (y != 0 &&x!= 11)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y - 1] = true;
                }
                //diag left (left orient)
                if(x != 0 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    //if you can enpassant red
                    if (red[0] ==x- 1 && red[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x - 1, y + 1] = true;
                }
                //diag right (left orient)
                if(x != 11 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x+ 1 && yel[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isBlue)
                        r[x + 1, y + 1] = true;
                }
                //middle
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null)
                        r[x, y + 1] = true;
                }
            }
        }

// YELLOW PAWN MOVEMENT
        if(Chessmans[x, y].isYellow)
        {
            if(Chessmans[x, y].pawnDirection == 0)
            {
                //diag left 
                if(x != 11 && y != 11) //if the blue pawn is not in a position where diag left can't be read.
                {
                    c = Chessmans[x + 1, y + 1]; //diag left is left on x and up on y for blue, and only from start
                    if (c != null && !c.isYellow) //if c was actually applied to something, and it is not blue.
                        r[x + 1, y + 1] = true; //then it is an active move, whether there is an enemy there or whether it is empty.
                }
                //diag middle
                if(x != 11 && y != 0)
                {
                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y - 1] = true;
                }
                //diag right
                if(x != 0 && y != 0)
                {
                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x - 1, y - 1] = true;
                }
                //left
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x + 1, y] = true;
                }
                //right
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y - 1] = true;
                }
                //left on first
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    c2 = Chessmans[x + 2, y];
                    if (c == null & c2 == null)
                        r[x + 2, y] = true;
                }
                //right on first
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    c2 = Chessmans[x, y -2];
                    if (c == null & c2 == null)
                        r[x, y - 2] = true;
                }
            }

            //for yellow, 2 goes from up to down
            if(Chessmans[x, y].pawnDirection == 2)
            {
                //diag left (right orient)
                if(x != 11 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x+ 1 && b[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y - 1] = true;
                }
                //diag right (right orient)
                if(x != 0 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x- 1 && g[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x - 1, y - 1] = true;
                }
                //middle
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null)
                        r[x, y - 1] = true;
                }
            }

            //for yellow, 1 goes from left to right
            if(Chessmans[x, y].pawnDirection == 1)
            {
                //diag left (right orient)
                if(x != 11 && y != 11)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y + 1] = true;
                }
                //diag right (right orient)
                if(x != 11 && y != 0)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y - 1] = true;
                }
                //middle
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    if (c == null)
                        r[x + 1, y] = true;
                }
            }

            if(Chessmans[x, y].pawnDirection == -1)
            {
                //diag left 
                if(x != 11 && y != 11) //if the blue pawn is not in a position where diag left can't be read.
                {
                    c = Chessmans[x + 1, y + 1]; //diag left is left on x and up on y for blue, and only from start
                    if (c != null && !c.isYellow) //if c was actually applied to something, and it is not blue.
                        r[x + 1, y + 1] = true; //then it is an active move, whether there is an enemy there or whether it is empty.
                }
                //diag middle
                if(x != 11 && y != 0)
                {
                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y - 1] = true;
                }
                //diag right
                if(x != 0 && y != 0)
                {
                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x - 1, y - 1] = true;
                }
                //left
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x + 1, y] = true;
                }
                //right
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y - 1] = true;
                }
                //diag left (right orient)
                if(x != 11 && y != 11)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y + 1] = true;
                }
                //diag right (right orient)
                if(x != 11 && y != 0)
                {
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y - 1] = true;
                }
                //middle
                if(x != 11)
                {
                    c = Chessmans[x + 1, y];
                    if (c == null)
                        r[x + 1, y] = true;
                }
                //diag left (right orient)
                if(x != 11 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x+ 1 && b[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x + 1, y - 1] = true;
                }
                //diag right (right orient)
                if(x != 0 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x- 1 && g[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isYellow)
                        r[x - 1, y - 1] = true;
                }
                //middle
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null)
                        r[x, y - 1] = true;
                }
            }

        }


// RED PAWN MOVEMENT
        if (Chessmans[x, y].isRed)
        {
            if (Chessmans[x, y].pawnDirection == 0)
            {
                //diag left 
                if(x != 11 && y != 0) 
                {
                    c = Chessmans[x + 1, y - 1]; 
                    if (c != null && !c.isRed) 
                        r[x + 1, y - 1] = true; 
                }
                //diag middle
                if(x != 0 && y != 0) 
                {
                    c = Chessmans[x - 1, y - 1]; 
                    if (c != null && !c.isRed) 
                        r[x - 1, y - 1] = true;
                }
                //diag right
                if(x != 0 && y != 11)
                {
                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y + 1] = true;
                }
                //left
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y - 1] = true;
                }
                //right
                if(x != 0)
                {
                    c = Chessmans[x - 1 , y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
                //left on first
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    c2 = Chessmans[x, y - 2];
                    if (c == null & c2 == null)
                        r[x, y - 2] = true;
                }
                //right on first
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    c2 = Chessmans[x - 2, y];
                    if (c == null & c2 == null)
                        r[x - 2, y] = true;
                }
            }

            //for red 2 goes right to left
            if (Chessmans[x, y].pawnDirection == 2)
            {
                //diag left (right orient)
                if(x != 0 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y - 1] = true;
                }
                //diag right (right orient)
                if(x != 0 && y != 11)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y + 1)
                        r[x - 1, y + 1] = true;
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y + 1] = true;
                }
                //forward
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
            }

            //for red, 1 goes from up to down
            if (Chessmans[x, y].pawnDirection == 1)
            {
                //diag left (right orient)
                if(x != 11 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x+ 1 && b[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x + 1, y - 1] = true;
                }
                //diag right (right orient)
                if(x != 0 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x- 1 && g[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y - 1] = true;
                }
                //forward
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y - 1] = true;
                }
            }

            if(Chessmans[x, y].pawnDirection == -1)
            {
                //diag left 
                if(x != 11 && y != 0)
                {
                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x + 1, y - 1] = true;
                }
                //diag middle
                if(x != 0 && y != 0)
                {
                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y - 1] = true;
                }
                //diag right
                if(x != 0 && y != 11)
                {
                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y + 1] = true;
                }
                //left
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y - 1] = true;
                }
                //right
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
                //diag left (right orient)
                if(x != 0 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y - 1] = true;
                }
                //diag right (right orient)
                if(x != 0 && y != 11)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y + 1)
                        r[x - 1, y + 1] = true;
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y + 1] = true;
                }
                //forward
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
                //diag left (right orient)
                if(x != 11 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x+ 1 && b[1] == y - 1)
                        r[x + 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x+ 1 && g[1] == y - 1)
                        r[x + 1, y - 1] = true;

                    c = Chessmans[x + 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x + 1, y - 1] = true;
                }
                //diag right (right orient)
                if(x != 0 && y != 0)
                {
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant green
                    if (g[0] ==x- 1 && g[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isRed)
                        r[x - 1, y - 1] = true;
                }
                //forward
                if (y != 0)
                {
                    c = Chessmans[x, y - 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y - 1] = true;
                }

            }
        }

// GREEN PAWN MOVEMENT
        if (Chessmans[x, y].isGreen)
        {
            if (Chessmans[x, y].pawnDirection == 0)
            {
                //diag left 
                if(x != 0 && y != 0)
                {
                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y - 1] = true;
                }
                //diag middle
                if(x != 0 && y != 11)
                {
                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y + 1] = true;
                }
                //diag right
                if(x != 11 && y != 11)
                {
                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x + 1, y + 1] = true;
                }
                //left
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
                //right
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y + 1] = true;
                }

                //left on first
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    c2 = Chessmans[x - 2, y];
                    if (c == null & c2 == null)
                        r[x - 2, y] = true;
                }
                //right on first
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    c2 = Chessmans[x, y + 2];
                    if (c == null & c2 == null)
                        r[x, y + 2] = true;
                }
            }

            //for green 2 goes from down to up
            if (Chessmans[x, y].pawnDirection == 2)
            {
                //diag left
                if(x != 0 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;
                    //if you can enpassant red
                    if (red[0] ==x- 1 && red[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y + 1] = true;
                }
                //diag right
                if(x != 11 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x+ 1 && yel[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x + 1, y + 1] = true;
                }
                //right
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y + 1] = true;
                }

            }

            //for green 1 goes from right to left
            if (Chessmans[x, y].pawnDirection == 1)
            {
                //diag left 
                if(x != 0 && y != 0)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y - 1] = true;
                }
                //diag right
                if(x != 0 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y + 1] = true;
                }
                //forward
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
            }


            if(Chessmans[x, y].pawnDirection == -1)
            {
                //diag left 
                if(x != 0 && y != 0)
                {
                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y - 1] = true;
                }
                //diag middle
                if(x != 0 && y != 11)
                {
                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y + 1] = true;
                }
                //diag right
                if(x != 11 && y != 11)
                {
                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x + 1, y + 1] = true;
                }
                //left
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
                //right
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y + 1] = true;
                }
                //diag left 
                if(x != 0 && y != 0)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y - 1)
                        r[x - 1, y - 1] = true;
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y - 1)
                        r[x - 1, y - 1] = true;

                    c = Chessmans[x - 1, y - 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y - 1] = true;
                }
                //diag right
                if(x != 0 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;
                    //if you can enpassant blue
                    if (b[0] ==x- 1 && b[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y + 1] = true;
                }
                //forward
                if(x != 0)
                {
                    c = Chessmans[x - 1, y];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x - 1, y] = true;
                }
                //diag left
                if(x != 0 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x- 1 && yel[1] == y + 1)
                        r[x - 1, y + 1] = true;
                    //if you can enpassant red
                    if (red[0] ==x- 1 && red[1] == y + 1)
                        r[x - 1, y + 1] = true;

                    c = Chessmans[x - 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x - 1, y + 1] = true;
                }
                //diag right
                if(x != 11 && y != 11)
                {
                    //if you can enpassant yellow
                    if (yel[0] ==x+ 1 && yel[1] == y + 1)
                        r[x + 1, y + 1] = true;
                    //if you can enpassant red
                    if (red[0] ==x+ 1 && red[1] == y + 1)
                        r[x + 1, y + 1] = true;

                    c = Chessmans[x + 1, y + 1];
                    if (c != null && !c.isGreen)
                        r[x + 1, y + 1] = true;
                }
                //right
                if (y != 11)
                {
                    c = Chessmans[x, y + 1];
                    if (c == null) //as long as nothing is there, be it friend or foe
                        r[x, y + 1] = true;
                }
            }
        }

        return r;
    }
}
