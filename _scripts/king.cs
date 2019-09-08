using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class king : Chessman
{
    public bool[,] tempAllowedMoves { set; get; }

    public override bool[,] PossibleMove(Chessman[,] Chessmans, int playerTurn, int[] b, int[] yel, int[] red, int[] g)
    {
        int x = CurrentX;
        int y = CurrentY;

        //int playerTurn;

        //if (eval == null)
        //{
        //    Chessmans = manager.Chessmans;
        //    playerTurn = manager.playerTurn;
        //}
        //else
        //{
        //    Chessmans = eval.Chessmans;
        //    playerTurn = eval.playerTurn;
        //}

        bool[,] r = new bool[12, 12];

        Chessman c;

        //Chessman c1;
        //Chessman c2;
        //Chessman c3;
        //Chessman c4;
        //Chessman c5;
        //Chessman c6;

 
        //UP
        if(y != 11)
        {
            c = Chessmans[x, y + 1];
            if (c == null)
                r[x, y + 1] = true;

            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x, y + 1] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x, y + 1] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x, y + 1] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x, y + 1] = true;
            }
        } // END UP

        //Down
        if (y != 0)
        {
            c = Chessmans[x, y - 1];
            if (c == null)
                r[x, y - 1] = true;

            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x, y - 1] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x, y - 1] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x, y - 1] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x, y - 1] = true;
            }
        } //end of down

        //Middle Left
        if(x != 0)
        {
            c = Chessmans[x - 1, y];
            if (c == null)
                r[x - 1, y] = true;
            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x - 1, y] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x - 1, y] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x - 1, y] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x - 1, y] = true;
            }
        } //end midddle left

        //Middle Right
        if (x != 11)
        {
            c = Chessmans[x + 1, y];
            if (c == null)
                r[x + 1, y] = true;
            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x + 1, y] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x + 1, y] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x + 1, y] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x + 1, y] = true;
            }
        } // End middle right

        //TOP LEFT
        if(x != 0 & y != 11)
        {
            c = Chessmans[x - 1, y + 1];
            if (c == null)
                r[x - 1, y + 1] = true;

            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x - 1, y + 1] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x - 1, y + 1] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x - 1, y + 1] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x - 1, y + 1] = true;
            }
        } //end top left

        //TOP RIGHT
        if (x != 11 & y != 11)
        {
            c = Chessmans[x + 1, y + 1];
            if (c == null)
                r[x + 1, y + 1] = true;

            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x + 1, y + 1] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x + 1, y + 1] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x + 1, y + 1] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x + 1, y + 1] = true;
            }
        } //end top right

        //BOTTOM LEFT
        if (x != 0 & y != 0)
        {
            c = Chessmans[x - 1, y - 1];
            if (c == null)
                r[x - 1, y - 1] = true;

            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x - 1, y - 1] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x - 1, y - 1] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x - 1, y - 1] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x - 1, y - 1] = true;
            }
        } //end bottom left

        //BOTTOM RIGHT
        if (x != 11 & y != 0)
        {
            c = Chessmans[x + 1, y - 1];
            if (c == null)
                r[x + 1, y - 1] = true;

            else if (Chessmans[x, y].isBlue & !c.isBlue)
            {
                r[x + 1, y - 1] = true;
            }
            else if (Chessmans[x, y].isYellow & !c.isYellow)
            {
                r[x + 1, y - 1] = true;
            }
            else if (Chessmans[x, y].isRed & !c.isRed)
            {
                r[x + 1, y - 1] = true;
            }
            else if (Chessmans[x, y].isGreen & !c.isGreen)
            {
                r[x + 1, y - 1] = true;
            }
        } //end bottom right


        //Castleing
        if (Chessmans[x, y].pawnDirection == 0)
        {
            if (playerTurn == 1)
            {
                //if isblue
                if (Chessmans[x, y].isBlue)
                {
                    bool canRight = true;
                    bool canLeft = true;

                    Chessman c1 = Chessmans[x + 1, y];
                    Chessman c2 = Chessmans[x + 2, y];
                    if (c1 == null & c2 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c3 = Chessmans[x + 3, y]; //then get the piece that is where the rook should be.
                        if (c3 != null)
                        {
                            if (c3.isBlue) //Identify the piece as the kings rook that hasnt moved.
                            {
                                if (c3.gameObject.name == "Rook")
                                {
                                    if (c3.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isBlue)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {
                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 2 && highlightY == 0)
                                                        {
                                                            canRight = false;
                                                        }
                                                        if (highlightX == 1 && highlightY == 0)
                                                        {
                                                            canRight = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canRight = false;
                                    }
                                }
                                else
                                {
                                    canRight = false;
                                }
                            }
                            else
                            {
                                canRight = false;
                            }
                        }
                        else
                        {
                            canRight = false;
                        }
                        if (canRight)
                        {
                            r[x + 2, y] = true;
                        }
                    }

                    Chessman c4 = Chessmans[x, y + 1];
                    Chessman c5 = Chessmans[x, y + 2];

                    if (c4 == null && c5 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c6 = Chessmans[x + 3, y]; //then get the piece that is where the rook should be.

                        if (c6 != null)
                        {


                            if (c6.isBlue) //Identify the piece as the kings rook that hasnt moved.
                            {
                                if (c6.gameObject.name == "Rook")
                                {
                                    if (c6.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isBlue)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {
                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 0 && highlightY == 2)
                                                        {
                                                            canLeft = false;
                                                        }
                                                        if (highlightX == 0 && highlightY == 1)
                                                        {
                                                            canLeft = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canLeft = false;
                                    }
                                }
                                else
                                {
                                    canLeft = false;
                                }
                            }
                            else
                            {
                                canLeft = false;
                            }
                        }
                        else
                        {
                            canLeft = false;
                        }
                        if (canLeft)
                        {
                            r[x, y + 2] = true;
                        }
                    }
                }
            }

            if (playerTurn == 2)
            {
                //is yellow
                if (Chessmans[x, y].isYellow)
                {
                    bool canRight = true;
                    bool canLeft = true;

                    Chessman c1 = Chessmans[x, y - 1];
                    Chessman c2 = Chessmans[x, y - 2];
                    if (c1 == null && c2 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c3 = Chessmans[x, y - 3]; //then get the piece that is where the rook should be.
                        if (c3 != null)
                        {
                            if (c3.isYellow)
                            {
                                if(c3.gameObject.name == "Rook")
                                {
                                    if (c3.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isYellow)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {

                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 0 && highlightY == 9)
                                                        {
                                                            canRight = false;
                                                        }
                                                        if (highlightX == 0 && highlightY == 10)
                                                        {
                                                            canRight = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canRight = false;
                                    }
                                }
                                else
                                {
                                    canRight = false;
                                }
                            }
                            else
                            {
                                canRight = false;
                            }
                        }
                        else
                        {
                            canRight = false;
                        }


                        if (canRight)
                        {
                            r[x, y - 2] = true;
                        }
                    }

                    Chessman c4 = Chessmans[x + 1, y];
                    Chessman c5 = Chessmans[x + 2, y];

                    if (c4 == null && c5 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c6 = Chessmans[x + 3, y]; //then get the piece that is where the rook should be.
                        if (c6 != null)
                        {
                            if (c6.isYellow)
                            {
                                if(c6.gameObject.name == "Rook")
                                {
                                    if (c6.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isYellow)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {
                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 2 && highlightY == 11)
                                                        {
                                                            canLeft = false;
                                                        }
                                                        if (highlightX == 1 && highlightY == 11)
                                                        {
                                                            canLeft = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canLeft = false;
                                    }
                                }
                                else
                                {
                                    canLeft = false;
                                }
                            }
                            else
                            {
                                canLeft = false;
                            }
                        }
                        else
                        {
                            canLeft = false;
                        }

                        if (canLeft)
                        {
                            r[x + 2, y] = true;
                        }
                    }
                }
            }

            if (playerTurn == 3)
            {
                //is red
                if (Chessmans[x, y].isRed)
                {
                    bool canRight = true;
                    bool canLeft = true;

                    Chessman c1 = Chessmans[x - 1, y];
                    Chessman c2 = Chessmans[x - 2, y];
                    if (c1 == null && c2 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c3 = Chessmans[x - 3, y]; //then get the piece that is where the rook should be.
                        if (c3 != null)
                        {
                            if (c3.isRed)
                            {
                                if (c3.gameObject.name == "Rook")
                                {
                                    if (c3.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isRed)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {
                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 9 && highlightY == 11)
                                                        {
                                                            canRight = false;
                                                        }
                                                        if (highlightX == 10 && highlightY == 11)
                                                        {
                                                            canRight = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canRight = false;
                                    }
                                }
                                else
                                {
                                    canRight = false;
                                }
                            }
                            else
                            {
                                canRight = false;
                            }
                        }
                        else
                        {
                            canRight = false;
                        }

                        if (canRight)
                        {
                            r[x - 2, y] = true;
                        }
                    }

                    Chessman c4 = Chessmans[x, y - 1];
                    Chessman c5 = Chessmans[x, y - 2];

                    if (c4 == null && c5 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c6 = Chessmans[x, y - 3]; //then get the piece that is where the rook should be.
                        if (c6 != null)
                        {
                            if (c6.isRed)
                            {
                                if (c6.gameObject.name == "Rook")
                                {
                                    if (c6.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isRed)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {
                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 11 && highlightY == 9)
                                                        {
                                                            canLeft = false;
                                                        }
                                                        if (highlightX == 11 && highlightY == 10)
                                                        {
                                                            canLeft = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canLeft = false;
                                    }
                                }
                                else
                                {
                                    canLeft = false;
                                }
                            }
                            else
                            {
                                canLeft = false;
                            }
                        }
                        else
                        {
                            canLeft = false;
                        }

                        if (canLeft)
                        {
                            r[x, y - 2] = true;
                        }
                    }
                }
            }

            if (playerTurn == 4)
            {
                //is green
                if (Chessmans[x, y].isGreen)
                {
                    bool canRight = true;
                    bool canLeft = true;

                    Chessman c1 = Chessmans[x, y + 1];
                    Chessman c2 = Chessmans[x, y + 2];
                    if (c1 == null && c2 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c3 = Chessmans[x, y + 3]; //then get the piece that is where the rook should be.

                        if(c3 != null)
                        {
                            if(c3.isGreen)
                            {
                                if(c3.gameObject.name == "Rook")
                                {
                                    Debug.Log("hai");
                                    if(c3.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isGreen)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {
                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 11 && highlightY == 2)
                                                        {
                                                            canRight = false;
                                                        }
                                                        if (highlightX == 11 && highlightY == 1)
                                                        {
                                                            canRight = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canRight = false;
                                    }
                                }
                                else
                                {
                                    canRight = false;
                                }
                            }
                            else
                            {
                                canRight = false;
                            }
                        }
                        else
                        {
                            canRight = false;
                        }


                        if (canRight)
                        {
                            r[x, y + 2] = true;
                        }
                    }

                    Chessman c4 = Chessmans[x - 1, y];
                    Chessman c5 = Chessmans[x - 2, y];

                    if (c4 == null && c5 == null) //if there are no pieces between the king and the rook to the right.
                    {
                        Chessman c6 = Chessmans[x - 3, y]; //then get the piece that is where the rook should be.
                        if (c6 != null)
                        {
                            if(c6.isGreen)
                            {
                                if (c6.gameObject.name == "Rook")
                                {
                                    if(c6.pawnDirection == 0)
                                    {
                                        //see if the two tiles are in check
                                        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                                        foreach (GameObject p in allPieces) // for each peice on the board that is active...
                                        {
                                            if (!p.GetComponent<Chessman>().isGreen)  //any pieces have these tiles in check.
                                            {
                                                bool hasAtleastOneMove = false;
                                                tempAllowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, b, yel, red, g); //then assign their possible moves to allowedMoves

                                                //see if the piece has any movements
                                                for (int i = 0; i < 12; i++)
                                                {
                                                    for (int j = 0; j < 12; j++)
                                                    {
                                                        if (tempAllowedMoves[i, j])
                                                        {
                                                            hasAtleastOneMove = true;
                                                        }
                                                    }
                                                }
                                                //if the piece has movements, see if one of those movements contains a king of another color
                                                if (hasAtleastOneMove)
                                                {
                                                    // if it is a king of another color, put them in check.
                                                    BoardHighlights.Instance.HighlightAllowedMoves(tempAllowedMoves); //spawn the highlight game objects on the allowed moves
                                                    GameObject[] allHighlights = GameObject.FindGameObjectsWithTag("highlights"); //make a list of the highlight GOs;                 

                                                    //check to see if those highlights are equal to the an enemy king's position.
                                                    foreach (GameObject h in allHighlights)
                                                    {
                                                        int highlightX = (int)(h.transform.position.x);
                                                        int highlightY = (int)(h.transform.position.z);

                                                        //if they're not, make the one next to the rook availible.
                                                        if (highlightX == 0 && highlightY == 9)
                                                        {
                                                            canLeft = false;
                                                        }
                                                        if (highlightX == 0 && highlightY == 10)
                                                        {
                                                            canLeft = false;
                                                        }
                                                    }
                                                    BoardHighlights.Instance.Hidehighlights();          //remove the highlight
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        canLeft = false;
                                    }
                                }
                                else
                                {
                                    canLeft = false;
                                }
                            }
                            else
                            {
                                canLeft = false;
                            }
                        }
                        else
                        {
                            canLeft = false;
                        }


                        if (canLeft)
                        {
                            r[x - 2, y] = true;
                        }
                    }
                }
            }
        }
        return r;
    }


}
