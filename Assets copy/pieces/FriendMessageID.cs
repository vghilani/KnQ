using GameSparks.Api.Requests;
using GameSparks.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendMessageID : MonoBehaviour {


    [SerializeField] public string MessageID;
    [SerializeField] public string friendDisplayName;
    public string friendID; 
    public Text Message;

    private bool isAssigned = true;

    public void DeclineFriendRequest()
    {
        new LogEventRequest().SetEventKey("friendRequestDeclined").SetEventAttribute("request_id", MessageID).Send((response) => {
            if (!response.HasErrors)
            {
                Destroy(this.gameObject);
                // Commented out on 3/26/19 - is this needed?
                // GameObject.Find("Menu Manager").GetComponent<mainMenu>().GetMessages(); 
                Debug.Log("declined request");
            }
            else
            {
                Debug.Log("fail");
            }
        });

    }

    public void AcceptFriendRequest()
    {
        new LogEventRequest().SetEventKey("acceptFriendRequest")
            .SetEventAttribute("request_id", MessageID)
            .SetEventAttribute("accepterid", mainMenu.myID)
            .SetEventAttribute("accepterName", mainMenu.myName)
            .Send((response) => {
            if (!response.HasErrors)
            {
                GameObject.Find("Menu Manager").GetComponent<mainMenu>().MessageCount--;
                Debug.Log("accepted request");


                    GameObject.Find("Menu Manager").GetComponent<mainMenu>().FriendCount++;
                    GameObject go = Instantiate(GameObject.Find("Menu Manager").GetComponent<mainMenu>().DogTag, GameObject.Find("Menu Manager").GetComponent<mainMenu>().spawnpoint2.transform);
                    go.GetComponent<Dogtag>().displayName = friendDisplayName;
                    go.GetComponent<Dogtag>().buttonText.text = friendDisplayName;
                    go.GetComponent<Dogtag>().playerId = friendID;
                    GameObject.Find("Menu Manager").GetComponent<mainMenu>().ListOfFriends.Add(go);
                    Destroy(this.gameObject);
                }
            else
            {
                Debug.Log("failed to accept friend request");
            }
        });
    }

}
