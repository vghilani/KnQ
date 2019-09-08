using System.Collections.Generic;
using UnityEngine;

public class AIControl
{
    //Stores current board state
    public AIChessman[,] boardState { set; get; }
    //Stores the color that this AI controls
    public int color;
    //Stores root node of tree
    public node root;
    //Depth to which recursion will search through future moves
    //Assert (recursionDepth % 4) == 0
    public int recursionDepth = 0;
    //Store the previous move chosen
    public int[] previousMove = new int[2];

    //Constructor - Generates controller from a state of Chessman type
    public AIControl(Chessman[,] Chessmans, int c)
    {
        boardState = duplicateBoard(Chessmans);
        color = c;
        int[] emptyPos = new int[2];
        root = new node(null, duplicateAIBoard(boardState), emptyPos, emptyPos);
    }

    //Alternate Constructor 1 - Generates controller from a state of AIChessman type
    public AIControl(AIChessman[,] Chessmans, int c)
    {
        boardState = duplicateAIBoard(Chessmans);
        color = c;
        int[] emptyPos = new int[2];
        root = new node(null, duplicateAIBoard(boardState), emptyPos, emptyPos);
    }

    //Alternate Constructor 2 - Generates conroller from an existing node
    public AIControl(node newRoot, int c)
    {
        boardState = duplicateAIBoard(newRoot.state);
        color = c;
        root = newRoot;
    }

    public static AIChessman[,] duplicateBoard(Chessman[,] boardState)
    {
        AIChessman[,] dupe = new AIChessman[12, 12];
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (boardState[i, j] != null)
                {
                    Chessman x = boardState[i, j];
                    dupe[i, j] = new AIChessman(x.CurrentX, x.CurrentY, x.isBlue, x.isGreen, x.isRed, x.isYellow, x.pawnDirection, x.isChecked, x.isMated, x.Rank);
                }
            }
        }
        return dupe;
    }

    public static AIChessman[,] duplicateAIBoard(AIChessman[,] boardState)
    {
        AIChessman[,] dupe = new AIChessman[12, 12];
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (boardState[i, j] != null)
                {
                    AIChessman x = boardState[i, j];
                    dupe[i, j] = new AIChessman(x.CurrentX, x.CurrentY, x.isBlue, x.isGreen, x.isRed, x.isYellow, x.pawnDirection, x.isChecked, x.isMated, x.Rank);
                }
            }
        }
        return dupe;
    }

    public void addNewState(node n, AIChessman[,] state, AIChessman x, int[] tPos, int[] curPos)
    {
        //Adds a new node in the children of node n by moving Chessman x from curPos to tPos in the state

        AIChessman[,] newState = duplicateAIBoard(state);

        int pdir = x.pawnDirection;
        //Check if the moved piece is a pawn to set direction
        if ((x.Rank == 0) && (x.pawnDirection == 0) && ((tPos[0] != curPos[0]) ^ (tPos[1] != curPos[1])))
        {
            switch (x.getColor())
            {
                case 1:
                    if (curPos[1] > tPos[1])
                    {
                        pdir = 1;
                    }
                    else
                    {
                        pdir = 2;
                    }
                    break;
                case 2:
                    if (curPos[0] > tPos[0])
                    {
                        pdir = 1;
                    }
                    else
                    {
                        pdir = 2;
                    }
                    break;
                case 3:
                    if (curPos[1] < tPos[1])
                    {
                        pdir = 1;
                    }
                    else
                    {
                        pdir = 2;
                    }
                    break;
                case 4:
                    if (curPos[0] < tPos[0])
                    {
                        pdir = 1;
                    }
                    else
                    {
                        pdir = 2;
                    }
                    break;
            }
        }
        newState[tPos[0], tPos[1]] = new AIChessman(tPos[0], tPos[1], x.isBlue, x.isGreen, x.isRed, x.isYellow, pdir, x.isChecked, x.isMated, x.Rank);
        newState[curPos[0], curPos[1]] = null;
        n.addChild(new node(n, newState, curPos, tPos));
        return;
    }

    public void generateChildren(node n)
    {
        int[] testPosition;
        int[] currentPosition;

        boardState = n.state;
        foreach (AIChessman x in boardState)
        {
            if (x != null)
            {
                if (x.getColor() == color)
                {
                    switch (x.Rank)
                    {

                        case 0:
                            //pawn
                            int moveX = 0;
                            int moveY = 0;
                            int globalDirection = -1; // 0 == both, 1 == x axis, 2 == y axis
                            switch (x.getColor())
                            {//Determine movement direction
                                case 1:
                                    //Blue
                                    moveX = 1;
                                    moveY = 1;
                                    if (x.pawnDirection == 1)
                                    {
                                        globalDirection = 2;
                                    }
                                    else if (x.pawnDirection == 2)
                                    {
                                        globalDirection = 1;
                                    }
                                    else
                                    {
                                        globalDirection = 0;
                                    }
                                    break;
                                case 2:
                                    //Yellow
                                    moveX = 1;
                                    moveY = -1;
                                    globalDirection = x.pawnDirection;
                                    break;
                                case 3:
                                    //Red
                                    moveX = -1;
                                    moveY = -1;
                                    if (x.pawnDirection == 1)
                                    {
                                        globalDirection = 2;
                                    }
                                    else if (x.pawnDirection == 2)
                                    {
                                        globalDirection = 1;
                                    }
                                    else
                                    {
                                        globalDirection = 0;
                                    }
                                    break;
                                case 4:
                                    //Green
                                    moveX = -1;
                                    moveY = 1;
                                    globalDirection = x.pawnDirection;
                                    break;
                            }
                            switch (globalDirection)
                            {
                                case 0:
                                    //Move in x direction
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY] == null)
                                        {
                                            testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY };
                                            currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                            addNewState(n, boardState, x, testPosition, currentPosition);
                                            //Double move
                                            if (boardState[x.CurrentX + moveX + moveX, x.CurrentY] == null)
                                            {
                                                testPosition = new int[2] { x.CurrentX + moveX + moveX, x.CurrentY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    //Attack in x direction
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12) && ((x.CurrentY - moveY) >= 0) && ((x.CurrentY - moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY - moveY] != null)
                                        {
                                            if (boardState[x.CurrentX + moveX, x.CurrentY - moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY - moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12) && ((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY + moveY] != null)
                                        {
                                            if (boardState[x.CurrentX + moveX, x.CurrentY + moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY + moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }

                                    //Move in y direction
                                    if (((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX, x.CurrentY + moveY] == null)
                                        {
                                            testPosition = new int[2] { x.CurrentX, x.CurrentY + moveY };
                                            currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                            addNewState(n, boardState, x, testPosition, currentPosition);
                                            //Double move
                                            if (boardState[x.CurrentX, x.CurrentY + moveY + moveY] == null)
                                            {
                                                testPosition = new int[2] { x.CurrentX, x.CurrentY + moveY + moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }

                                    //Attack in y direction
                                    if (((x.CurrentX - moveX) >= 0) && ((x.CurrentX - moveX) < 12) && ((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX - moveX, x.CurrentY + moveY] != null)
                                        {
                                            if (boardState[x.CurrentX - moveX, x.CurrentY + moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX - moveX, x.CurrentY + moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12) && ((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY + moveY] != null)
                                        {
                                            if (boardState[x.CurrentX + moveX, x.CurrentY + moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY + moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    break;
                                case 1:
                                    //Move in x direction
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY] == null)
                                        {
                                            testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY };
                                            currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                            addNewState(n, boardState, x, testPosition, currentPosition);
                                        }
                                    }
                                    //Attack in x direction
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12) && ((x.CurrentY - moveY) >= 0) && ((x.CurrentY - moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY - moveY] != null)
                                        {
                                            if (boardState[x.CurrentX + moveX, x.CurrentY - moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY - moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12) && ((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY + moveY] != null)
                                        {
                                            if (boardState[x.CurrentX + moveX, x.CurrentY + moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY + moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    break;
                                case 2:
                                    //Move in y direction
                                    if (((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX, x.CurrentY + moveY] == null)
                                        {
                                            testPosition = new int[2] { x.CurrentX, x.CurrentY + moveY };
                                            currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                            addNewState(n, boardState, x, testPosition, currentPosition);
                                        }
                                    }
                                    //Attack in y direction
                                    if (((x.CurrentX - moveX) >= 0) && ((x.CurrentX - moveX) < 12) && ((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX - moveX, x.CurrentY + moveY] != null)
                                        {
                                            if (boardState[x.CurrentX - moveX, x.CurrentY + moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX - moveX, x.CurrentY + moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    if (((x.CurrentX + moveX) >= 0) && ((x.CurrentX + moveX) < 12) && ((x.CurrentY + moveY) >= 0) && ((x.CurrentY + moveY) < 12))
                                    {
                                        if (boardState[x.CurrentX + moveX, x.CurrentY + moveY] != null)
                                        {
                                            if (boardState[x.CurrentX + moveX, x.CurrentY + moveY].getColor() != color)
                                            {
                                                testPosition = new int[2] { x.CurrentX + moveX, x.CurrentY + moveY };
                                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;


                        case 1:
                            //rook
                            //Positive x dir
                            for (int i = x.CurrentX + 1; i < 12; i++)
                            {
                                //Select position to test
                                testPosition = new int[2] { i, x.CurrentY };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            //Negative x dir
                            for (int i = x.CurrentX - 1; i >= 0; i--)
                            {
                                //Select position to test
                                testPosition = new int[2] { i, x.CurrentY };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            //Positive y dir
                            for (int i = x.CurrentY + 1; i < 12; i++)
                            {
                                //Select position to test
                                testPosition = new int[2] { x.CurrentX, i };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            //Negative y dir
                            for (int i = x.CurrentY - 1; i >= 0; i--)
                            {
                                //Select position to test
                                testPosition = new int[2] { x.CurrentX, i };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            break;


                        case 2:
                            //knight
                            int[] knightMove1 = new int[2] { -1, 1 };
                            int[] knightMove2 = new int[2] { -2, 2 };
                            foreach (int i in knightMove1)
                            {
                                foreach (int j in knightMove2)
                                {
                                    if (((x.CurrentX + i) < 12) && ((x.CurrentX + i) >= 0) && ((x.CurrentY + j) < 12) && ((x.CurrentY + j) >= 0))
                                    {
                                        //Select position to test
                                        testPosition = new int[2] { x.CurrentX + i, x.CurrentY + j };
                                        currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                        //If selected position is empty or contains enemy piece move there
                                        if (boardState[testPosition[0], testPosition[1]] == null ||
                                            boardState[testPosition[0], testPosition[1]].getColor() != color)
                                        {
                                            addNewState(n, boardState, x, testPosition, currentPosition);
                                        }
                                    }
                                    if (((x.CurrentX + j) < 12) && ((x.CurrentX + j) >= 0) && ((x.CurrentY + i) < 12) && ((x.CurrentY + i) >= 0))
                                    {
                                        //Select position to test
                                        testPosition = new int[2] { x.CurrentX + j, x.CurrentY + i };
                                        currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                        //If selected position is empty or contains enemy piece move there
                                        if (boardState[testPosition[0], testPosition[1]] == null ||
                                            boardState[testPosition[0], testPosition[1]].getColor() != color)
                                        {
                                            addNewState(n, boardState, x, testPosition, currentPosition);
                                        }
                                    }
                                }
                            }
                            break;


                        case 3:
                            //bishop
                            for (int i = 1; i < 12; i++)
                            { //Up Right
                                if (((x.CurrentX + i) < 12) && ((x.CurrentY + i) < 12))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX + i, x.CurrentY + i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i < 12; i++)
                            { //Up Left
                                if (((x.CurrentX - i) >= 0) && ((x.CurrentY + i) < 12))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX - i, x.CurrentY + i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }

                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i < 12; i++)
                            { //Down Right
                                if (((x.CurrentX + i) < 12) && ((x.CurrentY - i) >= 0))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX + i, x.CurrentY - i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i < 12; i++)
                            { //Down Left
                                if (((x.CurrentX - i) >= 0) && ((x.CurrentY - i) >= 0))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX - i, x.CurrentY - i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;


                        case 4:
                            //queen
                            //logic from rook
                            //Positive x dir
                            for (int i = x.CurrentX + 1; i < 12; i++)
                            {
                                //Select position to test
                                testPosition = new int[2] { i, x.CurrentY };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            //Negative x dir
                            for (int i = x.CurrentX - 1; i >= 0; i--)
                            {
                                //Select position to test
                                testPosition = new int[2] { i, x.CurrentY };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            //Positive y dir
                            for (int i = x.CurrentY + 1; i < 12; i++)
                            {
                                //Select position to test
                                testPosition = new int[2] { x.CurrentX, i };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            //Negative y dir
                            for (int i = x.CurrentY - 1; i >= 0; i--)
                            {
                                //Select position to test
                                testPosition = new int[2] { x.CurrentX, i };
                                currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                //If selected position is empty or contains enemy piece move there
                                if (boardState[testPosition[0], testPosition[1]] == null)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                }
                                else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                {
                                    addNewState(n, boardState, x, testPosition, currentPosition);
                                    //Selected position contains enemy piece, stop searching
                                    break;
                                }
                                else
                                {
                                    //Selected position contains allied piece, stop searching
                                    break;
                                }
                            }
                            //logic from bishop
                            for (int i = 1; i < 12; i++)
                            { //Up Right
                                if (((x.CurrentX + i) < 12) && ((x.CurrentY + i) < 12))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX + i, x.CurrentY + i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i < 12; i++)
                            { //Up Left
                                if (((x.CurrentX - i) >= 0) && ((x.CurrentY + i) < 12))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX - i, x.CurrentY + i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i < 12; i++)
                            { //Down Right
                                if (((x.CurrentX + i) < 12) && ((x.CurrentY - i) >= 0))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX + i, x.CurrentY - i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            for (int i = 1; i < 12; i++)
                            { //Down Left
                                if (((x.CurrentX - i) >= 0) && ((x.CurrentY - i) >= 0))
                                {
                                    //Select position to test
                                    testPosition = new int[2] { x.CurrentX - i, x.CurrentY - i };
                                    currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                    //If selected position is empty or contains enemy piece move there
                                    if (boardState[testPosition[0], testPosition[1]] == null)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                    }
                                    else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                    {
                                        addNewState(n, boardState, x, testPosition, currentPosition);
                                        //Selected position contains enemy piece, stop searching
                                        break;
                                    }
                                    else
                                    {
                                        //Selected position contains allied piece, stop searching
                                        break;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;


                        case 5:
                            //king
                            for (int i = -1; i <= 1; i++)
                            { //Change in x
                                for (int j = -1; j <= 1; j++)
                                { //Change in y
                                    if (((x.CurrentX + i) >= 0) && ((x.CurrentX + i) < 12) && ((x.CurrentY + j) >= 0) && ((x.CurrentY + j) < 12))
                                    { //Check that move is still on board
                                        if (!((i == 0) && (j == 0)))
                                        { //Cannot have no movement
                                          //Select position to test
                                            testPosition = new int[2] { x.CurrentX + i, x.CurrentY + j };
                                            currentPosition = new int[2] { x.CurrentX, x.CurrentY };
                                            //If selected position is empty or contains enemy piece move there
                                            //Currently king will only move to take a piece
                                            if (boardState[testPosition[0], testPosition[1]] == null)
                                            {
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                            else if (boardState[testPosition[0], testPosition[1]].getColor() != color)
                                            {
                                                addNewState(n, boardState, x, testPosition, currentPosition);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

    }

    //Evaluates score of a state
    public void evaluateScores(node n)
    {
        foreach (node c in n.children)
        {
            int score = 0;
            //Check that the move isn't undoing the previous move
            if ((previousMove[0] == c.positionTo[0]) && (previousMove[1] == c.positionTo[1]))
            {
                c.illegal = true;
                score -= 10000;
            }

            //Score based on player piceces and enemy pieces
            foreach (AIChessman x in c.state)
            {
                if (x != null)
                {
                    if (x.getColor() == color)
                    {
                        int checkScore = 0;
                        switch (x.Rank)
                        {
                            case 0: //pawn
                                score += 1;
                                break;
                            case 1: //rook
                                score += 15;
                                break;
                            case 2: //knight
                                score += 10;
                                break;
                            case 3: //bishop
                                score += 15;
                                break;
                            case 4: //queen
                                checkScore = checkCheck(x, c.state);
                                if (checkScore < 0) {
//                                    c.illegal = true;
                                }

                                score += checkScore;
                                score += 75;
                                break;
                            case 5: //king
                                checkScore = checkCheck(x, c.state);
                                if (checkScore < 0) {
                                    c.illegal = true;
                                }

                                score += checkScore;
                                score += 1000;
                                break;
                        }
                    }
                    else
                    {
                        switch (x.Rank)
                        {
                            case 0: //pawn
                                score -= 1;
                                break;
                            case 1: //rook
                                score -= 15;
                                break;
                            case 2: //knight
                                score -= 10;
                                break;
                            case 3: //bishop
                                score -= 15;
                                break;
                            case 4: //queen
                                score -= 75;
                                break;
                            case 5: //king
                                score -= 1000;
                                break;
                        }
                    }
                }
            }

            c.setScore(score);
        }
        return;
    }

    //See if piece x is in check (king or queen)
    public int checkCheck(AIChessman x, AIChessman[,] checkState)
    {
        int checkScore = 0;
        if (x.Rank == 4)
        {
            checkScore = -5000;
        }
        else if (x.Rank == 5)
        {
            checkScore = -10000;
        }
        int[] testPosition = new int[2];

        //rook
        //Positive x dir
        for (int i = x.CurrentX + 1; i < 12; i++)
        {
            //Select position to test
            testPosition = new int[2] { i, x.CurrentY };
            if (checkState[testPosition[0], testPosition[1]] == null)
            {
                //Do nothing, not in check
            }
            else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                    (checkState[testPosition[0], testPosition[1]].Rank == 1 ||
                     checkState[testPosition[0], testPosition[1]].Rank == 4))
            {
                //Selected position contains enemy piece, in check
                return checkScore;
            }
            else
            {
                //Selected position contains other piece, stop searching
                break;
            }
        }
        //Negative x dir
        for (int i = x.CurrentX - 1; i >= 0; i--)
        {
            //Select position to test
            testPosition = new int[2] { i, x.CurrentY };
            if (checkState[testPosition[0], testPosition[1]] == null)
            {
                //Do nothing, not in check
            }
            else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                    (checkState[testPosition[0], testPosition[1]].Rank == 1 ||
                     checkState[testPosition[0], testPosition[1]].Rank == 4))
            {
                //Selected position contains enemy piece, in check
                return checkScore;
            }
            else
            {
                //Selected position contains other piece, stop searching
                break;
            }
        }
        //Positive y dir
        for (int i = x.CurrentY + 1; i < 12; i++)
        {
            //Select position to test
            testPosition = new int[2] { x.CurrentX, i };
            if (checkState[testPosition[0], testPosition[1]] == null)
            {
                //Do nothing, not in check
            }
            else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                    (checkState[testPosition[0], testPosition[1]].Rank == 1 ||
                     checkState[testPosition[0], testPosition[1]].Rank == 4))
            {
                //Selected position contains enemy piece, in check
                return checkScore;
            }
            else
            {
                //Selected position contains other piece, stop searching
                break;
            }
        }
        //Negative y dir
        for (int i = x.CurrentY - 1; i >= 0; i--)
        {
            //Select position to test
            testPosition = new int[2] { x.CurrentX, i };
            if (checkState[testPosition[0], testPosition[1]] == null)
            {
                //Do nothing, not in check
            }
            else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                    (checkState[testPosition[0], testPosition[1]].Rank == 1 ||
                     checkState[testPosition[0], testPosition[1]].Rank == 4))
            {
                //Selected position contains enemy piece, in check
                return checkScore;
            }
            else
            {
                //Selected position contains other piece, stop searching
                break;
            }
        }

        //knight
        int[] knightMove1 = new int[2] { -1, 1 };
        int[] knightMove2 = new int[2] { -2, 2 };
        foreach (int i in knightMove1)
        {
            foreach (int j in knightMove2)
            {
                if (((x.CurrentX + i) < 12) && ((x.CurrentX + i) >= 0) && ((x.CurrentY + j) < 12) && ((x.CurrentY + j) >= 0))
                {
                    //Select position to test
                    testPosition = new int[2] { x.CurrentX + i, x.CurrentY + j };
                    if (checkState[testPosition[0], testPosition[1]] != null &&
                        checkState[testPosition[0], testPosition[1]].getColor() != color &&
                        checkState[testPosition[0], testPosition[1]].Rank == 2)
                    {
                        return checkScore;
                    }
                }
                if (((x.CurrentX + j) < 12) && ((x.CurrentX + j) >= 0) && ((x.CurrentY + i) < 12) && ((x.CurrentY + i) >= 0))
                {
                    //Select position to test
                    testPosition = new int[2] { x.CurrentX + j, x.CurrentY + i };
                    if (checkState[testPosition[0], testPosition[1]] != null &&
                        checkState[testPosition[0], testPosition[1]].getColor() != color &&
                        checkState[testPosition[0], testPosition[1]].Rank == 2)
                    {
                        return checkScore;
                    }
                }
            }
        }

        //bishop
        for (int i = 1; i < 12; i++)
        { //Up Right
            if (((x.CurrentX + i) < 12) && ((x.CurrentY + i) < 12))
            {
                //Select position to test
                testPosition = new int[2] { x.CurrentX + i, x.CurrentY + i };
                //If selected position is empty or contains enemy piece move there
                if (checkState[testPosition[0], testPosition[1]] == null)
                {

                }
                else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                        (checkState[testPosition[0], testPosition[1]].Rank == 3 ||
                         checkState[testPosition[0], testPosition[1]].Rank == 4))
                {
                    return checkScore;
                }
                else
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 12; i++)
        { //Up Left
            if (((x.CurrentX - i) >= 0) && ((x.CurrentY + i) < 12))
            {
                //Select position to test
                testPosition = new int[2] { x.CurrentX - i, x.CurrentY + i };
                //If selected position is empty or contains enemy piece move there
                if (checkState[testPosition[0], testPosition[1]] == null)
                {

                }
                else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                        (checkState[testPosition[0], testPosition[1]].Rank == 3 ||
                         checkState[testPosition[0], testPosition[1]].Rank == 4))
                {
                    return checkScore;
                }
                else
                {
                    //Selected position contains allied piece, stop searching
                    break;
                }

            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 12; i++)
        { //Down Right
            if (((x.CurrentX + i) < 12) && ((x.CurrentY - i) >= 0))
            {
                //Select position to test
                testPosition = new int[2] { x.CurrentX + i, x.CurrentY - i };
                //If selected position is empty or contains enemy piece move there
                if (checkState[testPosition[0], testPosition[1]] == null)
                {

                }
                else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                        (checkState[testPosition[0], testPosition[1]].Rank == 3 ||
                         checkState[testPosition[0], testPosition[1]].Rank == 4))
                {
                    return checkScore;
                }
                else
                {
                    //Selected position contains allied piece, stop searching
                    break;
                }
            }
            else
            {
                break;
            }
        }
        for (int i = 1; i < 12; i++)
        { //Down Left
            if (((x.CurrentX - i) >= 0) && ((x.CurrentY - i) >= 0))
            {
                //Select position to test
                testPosition = new int[2] { x.CurrentX - i, x.CurrentY - i };
                //If selected position is empty or contains enemy piece move there
                if (checkState[testPosition[0], testPosition[1]] == null)
                {

                }
                else if (checkState[testPosition[0], testPosition[1]].getColor() != color &&
                        (checkState[testPosition[0], testPosition[1]].Rank == 3 ||
                         checkState[testPosition[0], testPosition[1]].Rank == 4))
                {
                    return checkScore;
                }
                else
                {
                    //Selected position contains allied piece, stop searching
                    break;
                }
            }
            else
            {
                break;
            }
        }

        //king
        for (int i = -1; i <= 1; i++)
        { //Change in x
            for (int j = -1; j <= 1; j++)
            { //Change in y
                if (((x.CurrentX + i) >= 0) && ((x.CurrentX + i) < 12) && ((x.CurrentY + j) >= 0) && ((x.CurrentY + j) < 12))
                { //Check that move is still on board
                    if (!((i == 0) && (j == 0)))
                    { //Cannot have no movement
                      //Select position to test
                        testPosition = new int[2] { x.CurrentX + i, x.CurrentY + j };
                        //If selected position is empty or contains enemy piece move there
                        //Currently king will only move to take a piece
                        if (boardState[testPosition[0], testPosition[1]] == null)
                        {

                        }
                        else if (boardState[testPosition[0], testPosition[1]].getColor() != color &&
                                 boardState[testPosition[0], testPosition[1]].Rank == 5)
                        {
                            return checkScore;
                        }
                    }
                }
            }
        }

        //pawns (if there is a pawn immediately diagonal it is assumed to be in check)
        for (int i = -1; i <= 1; i++)
        { //Change in x
            for (int j = -1; j <= 1; j++)
            { //Change in y
                if (((x.CurrentX + i) >= 0) && ((x.CurrentX + i) < 12) && ((x.CurrentY + j) >= 0) && ((x.CurrentY + j) < 12))
                { //Check that move is still on board
                    if ((i != 0) && (j != 0))
                    {
                        //Select position to test
                        testPosition = new int[2] { x.CurrentX + i, x.CurrentY + j };
                        if (checkState[testPosition[0], testPosition[1]] != null &&
                            checkState[testPosition[0], testPosition[1]].getColor() != color &&
                            checkState[testPosition[0], testPosition[1]].Rank == 0)
                        {
                            return checkScore;
                        }
                    }
                }
            }
        }
        return 0;
    }


    //Move Choosing Logic

    //Chose move
    public node chooseMove(node n, bool isRandom)
    {
        if (isRandom)
        {
            //Randomly choose from best 3 (favors best)
            int max1 = -1000000;
            int max2 = -999999;
            int max3 = -999998;
            node best1 = null;
            node best2 = null;
            node best3 = null;

            foreach (node x in n.children)
            {
                if ( !x.illegal ) {
                    if (x.score > max1)
                    {
                        max1 = x.score;
                        best1 = x;
                    }
                    else if (x.score > max2)
                    {
                        max2 = x.score;
                        best2 = x;
                    }
                    else if (x.score > max3)
                    {
                        max3 = x.score;
                        best3 = x;
                    }
                }
            }
            System.Random r = new System.Random();
            int num = r.Next(100);
            if (best1 == null)
            {
                // all moves illegal, return first move so something can be returned
                return n.children[0];
            }
            if (num < 68 || best2 == null)
            {
                return best1;
            }
            if (num < 98 || best3 == null)
            {
                return best2;
            }
            if (num < 100)
            {
                return best3;
            }
        }
        else
        {
            //Choose best
            int max = -100000000;
            node best = null;
            foreach (node x in n.children)
            {
                if (x.score > max)
                {
                    max = x.score;
                    best = x;
                }
            }
            return best;
        }
        return null;
    }


    //Get move. Called by boardmanager
    public int[,] getMove(Chessman[,] currentBoard)
    {

        //Update board state
        root.state = duplicateBoard(currentBoard);
        //Generate Moves
        generateChildren(root);
        //Evaluate Moves
        evaluateScores(root);

        //Evaluate next level down tree
        //evaluateRecursive(root, color, 4);

        //Choose best Move
        node newState = chooseMove(root, true);

        int[,] move = new int[2, 2] {{0, 0},{0, 0}};
        if (newState != null)
        {
            move[0, 0] = newState.positionFrom[0];
            move[0, 1] = newState.positionFrom[1];
            move[1, 0] = newState.positionTo[0];
            move[1, 1] = newState.positionTo[1];

            Debug.Log(newState.score + ", " + newState.positionFrom[0] + ", " + newState.positionFrom[1] + ", " + newState.positionTo[0] + ", " + newState.positionTo[1]);
            //Reassign Root
            root = newState;
            previousMove = newState.positionFrom;
        }
        root.children.Clear();
        System.GC.Collect();

        return move;
    }

}

//Node class for search tree
public class node
{

    //Parent node
    public node parent;
    //List of children nodes
    public List<node> children;
    //State in this node
    public AIChessman[,] state { set; get; }
    //Move used to create node
    public int[] positionFrom;
    public int[] positionTo;
    //Score of this node
    public int score;
    //Recursive Score
    public int recursiveScore;
    //Generation of this node
    public int generation;
    //Illegal flag
    public bool illegal;

    //Constructor
    public node(node p, AIChessman[,] AIChessmans, int[] pFrom, int[] pTo)
    {
        parent = p;
        children = new List<node>();
        state = AIChessmans;
        positionFrom = pFrom;
        positionTo = pTo;
        score = -10000000;
        illegal = false;
        if (parent != null)
        {
            generation = parent.generation + 1;
        }
        else
        {
            generation = 0;
        }
    }

    public void setScore(int s)
    {
        score = s;
        return;
    }

    //Add Child
    public void addChild(node c)
    {
        children.Add(c);
        return;
    }

    //Remove Child
    public void removeChild(node c)
    {
        children.Remove(c);
        return;
    }
}

//Modified duplicate of the Chessman class for simulating purposes
public class AIChessman
{
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isBlue = false;
    public bool isGreen = false;
    public bool isRed = false;
    public bool isYellow = false;

    //this will be used on every non-pawn as well to determine if they moved from their starting position. Caselting uses it too..
    public int pawnDirection = 0; // 0 is undecided, 1 is left, 2 is right.  This integer will have to change when a pawn commits to a direction.

    public bool isChecked = false; //for leaders
    public bool isMated = false;
    public int Rank = 0;

    public AIChessman(int cx, int cy, bool ib, bool ig, bool ir, bool iy, int pd, bool ic, bool im, int r)
    {
        CurrentX = cx;
        CurrentY = cy;
        isBlue = ib;
        isGreen = ig;
        isRed = ir;
        isYellow = iy;
        pawnDirection = pd;
        isChecked = ic;
        isMated = im;
        Rank = r;
    }

    public void SetPosition(int x, int y)
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
}







