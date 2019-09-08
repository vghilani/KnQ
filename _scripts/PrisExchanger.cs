using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrisExchanger : MonoBehaviour
{
    private Text text;
    public Button button;
    boardmanager bm;

    private void Start()
    {
        bm = GameObject.Find("Chessboard").GetComponent<boardmanager>();
    }

    public void RefactoredPrisonExchangeSpawn()
    {
        text = button.GetComponentInChildren<Text>();
        if (text.text == "Rook") //1,19,13,7
        {
            if (bm.id == 1)
            {
                bm.SpawnChessman(1, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 2)
            {
                bm.SpawnChessman(19, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 3)
            {
                bm.SpawnChessman(13, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 4)
            {
                bm.SpawnChessman(7, bm.man.CurrentX, bm.man.CurrentY);
            }
        }
        //
        if (text.text == "Knight") //2,20,14,8
        {

            if (bm.id == 1)
            {
                bm.SpawnChessman(2, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 2)
            {
                bm.SpawnChessman(20, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 3)
            {
                bm.SpawnChessman(14, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 4)
            {
                bm.SpawnChessman(8, bm.man.CurrentX, bm.man.CurrentY);
            }

        }
        //
        if (text.text == "Bishop") //3,21,15,9
        {

            if (bm.id == 1)
            {
                bm.SpawnChessman(3, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 2)
            {
                bm.SpawnChessman(21, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 3)
            {
                bm.SpawnChessman(15, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 4)
            {
                bm.SpawnChessman(9, bm.man.CurrentX, bm.man.CurrentY);
            }

        }
        //
        if (text.text == "Queen") //4,22,16,10
        {

            if (bm.id == 1)
            {
                bm.SpawnChessman(4, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 2)
            {
                bm.SpawnChessman(22, bm.man.CurrentX, bm.man.CurrentY);
            }
            if (bm.id == 3)
            {
                bm.SpawnChessman(16, bm.man.CurrentX, bm.man.CurrentY);

            }
            if (bm.id == 4)
            {
                bm.SpawnChessman(10, bm.man.CurrentX, bm.man.CurrentY);
            }

        }
        //

        if (bm.listid == 1)
        {
            bm.bluePrisonersYellow.Remove(text.text);
        }
        if (bm.listid == 2)
        {
            bm.bluePrisonersRed.Remove(text.text);
        }
        if (bm.listid == 3)
        {
            bm.bluePrisonersGreen.Remove(text.text);
        }
        if (bm.listid == 4)
        {
            bm.yelPrisonersBlue.Remove(text.text);
        }
        if (bm.listid == 5)
        {
            bm.yelPrisonersRed.Remove(text.text);
        }
        if (bm.listid == 6)
        {
            bm.yelPrisonersGreen.Remove(text.text);
        }
        if (bm.listid == 7)
        {
            bm.redPrisonersBlue.Remove(text.text);
        }
        if (bm.listid == 8)
        {
            bm.redPrisonersYellow.Remove(text.text);
        }
        if (bm.listid == 9)
        {
            bm.redPrisonersGreen.Remove(text.text);
        }
        if (bm.listid == 10)
        {
            bm.greenPrisonersBlue.Remove(text.text);
        }
        if (bm.listid == 11)
        {
            bm.greenPrisonersYellow.Remove(text.text);
        }
        if (bm.listid == 12)
        {
            bm.greenPrisonersRed.Remove(text.text);
        }

        //delete and/or reset everything
        Destroy(bm.man.gameObject);
        bm.isDisplayed = false;
        bm.prisonerList.Clear();
        GameObject[] PrisonerObjects = GameObject.FindGameObjectsWithTag("prisListing");
        foreach (GameObject go in PrisonerObjects)
        {
            Destroy(go.gameObject);
        }
        Destroy(GameObject.Find("Dropdown List"));
        Destroy(GameObject.Find("Blocker"));
        bm.myCanvas.GetComponent<Canvas>().enabled = false;
  //      boardmanager.moved = true;

        if(bm.isOnline)
        {
            bm.SendGameDataAndAdvanceTurn();
        }
        bm.id = 0;
        bm.listid = 0;
    }

}
