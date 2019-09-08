using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {

    public string id;
    public string name; //for if we're deserializaing from accepted PlayerDetail Objects
    public string playerName; //for if we're deserializing from the CHallenge's ScriptData
    public string color; 

    public static Player CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<Player>(jsonString);
    }
}

[System.Serializable]
public class Data
{
    public string title;
    public string body;

    public static Data CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<Data>(jsonString);
    }
}

[System.Serializable]
public class GameOverID
{
    public string gid;

    public static GameOverID CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<GameOverID>(jsonString);
    }
}

