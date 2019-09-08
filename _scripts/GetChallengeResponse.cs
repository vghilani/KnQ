using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Api.Messages;
using GameSparks.Core;

[System.Serializable]
public class GetChallengeResponse : MonoBehaviour {
}

[System.Serializable]
public class Challenge
{
    public Accepted[] accepted;
    public string nextPlayer;
    public string lastPlayer;
    public MyScriptData ChallengeScriptData;
    public static Challenge CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<Challenge>(jsonString);
    }
}


[System.Serializable]
public class Accepted
{
    public string id;
    public string name;

    public static Challenge CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<Challenge>(jsonString);
    }
}

[System.Serializable]
public class PieceData
{
    public bool isBlue;
    public bool isGreen;
    public bool isRed;
    public bool isYellow = false;

    //these are needed for online data.....
    public string Name;
    public int index;
    public int SaveX; //x and y are set when the User sends their move to the server.
    public int SaveY;

    //this will be used on every non-pawn as well to determine if they moved from their starting position. Caselting uses it too..
    public int pawnDirection; // 0 is undecided, 1 is left, 2 is right.  This integer will have to change when a pawn commits to a direction.

    public bool isChecked; //for leaders
    public bool isMated;
    public int Rank;

    public static PieceData CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<PieceData>(jsonString);
    }
}


[System.Serializable]
public class Enpass
{
    public int number;

    public static Enpass CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<Enpass>(jsonString);
    }
}

[System.Serializable]
public class PrisonerItem
{
    public string prisName;

    public static PrisonerItem CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<PrisonerItem>(jsonString);
    }
}

[System.Serializable]
public class LastTiles
{
    public int LastX;
    public int LastY;

    public static LastTiles CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<LastTiles>(jsonString);
    }
}

public class WinConditions
{
    public int playerT;
    public int prevT;
    public bool Bout;
    public bool Yout;
    public bool Rout;
    public bool Gout;

    public static WinConditions CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<WinConditions>(jsonString);
    }
}