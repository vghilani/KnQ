using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
    public static BoardHighlights Instance { set; get; }

    public GameObject highlightPrefab;
    public GameObject attackHighlight;
    private List<GameObject> highlights;
    private List<GameObject> attackhighlights;
    boardmanager bm;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
        attackhighlights = new List<GameObject>();
        bm = GameObject.Find("Chessboard").GetComponent<boardmanager>();
    }

    private GameObject GetHighlightObject() //find the active moves.
    {
        GameObject go = Instantiate(highlightPrefab);
        highlights.Add(go);

        return go;
    }

    private GameObject GetAttackHighlightObject() //find the active moves.
    {
        GameObject o = Instantiate(attackHighlight);
        attackhighlights.Add(o);

        return o;
    }

    public void HighlightAllowedMoves(bool[,] moves) //apply the highlight to the active moves
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (moves[i, j] ==  true)
                {
                    bool isAttack = false;
              
                    if(bm.playerTurn == 1)
                    {
                        Chessman cm = bm.Chessmans[i, j];

                        if (cm != null)
                        {
                            if (!cm.isBlue)
                            {
                                GameObject o = GetAttackHighlightObject();
                                o.SetActive(true);
                                o.transform.position = new Vector3(i + 0.5f, 0.002f, j + 0.5f);
                                isAttack = true;
                            }
                        }
                    }
                    if (bm.playerTurn == 2)
                    {
                        Chessman cm = bm.Chessmans[i, j];

                        if (cm != null)
                        {
                            if (!cm.isYellow)
                            {
                                GameObject o = GetAttackHighlightObject();
                                o.SetActive(true);
                                o.transform.position = new Vector3(i + 0.5f, 0.002f, j + 0.5f);
                                isAttack = true;
                            }
                        }
                    }
                    if (bm.playerTurn == 3)
                    {
                        Chessman cm = bm.Chessmans[i, j];

                        if (cm != null)
                        {
                            if (!cm.isRed)
                            {
                                GameObject o = GetAttackHighlightObject();
                                o.SetActive(true);
                                o.transform.position = new Vector3(i + 0.5f, 0.002f, j + 0.5f);
                                isAttack = true;
                            }
                        }
                    }
                    if (bm.playerTurn == 4)
                    {
                        Chessman cm = bm.Chessmans[i, j];

                        if (cm != null)
                        {
                            if (!cm.isGreen)
                            {
                                GameObject o = GetAttackHighlightObject();
                                o.SetActive(true);
                                o.transform.position = new Vector3(i + 0.5f, 0.002f, j + 0.5f);

                                isAttack = true;
                            }
                        }
                    }

                    if(!isAttack)
                    {
                        GameObject go = GetHighlightObject();
                        go.SetActive(true);
                        go.transform.position = new Vector3(i + 0.5f, 0.001f, j + 0.5f);
                    }
                }
            }
        }
    }


    public void Hidehighlights() //remove the highlights
    {
        if(highlights.Count > 0)
        {
            foreach (GameObject go in highlights)
            {
                Destroy(go.gameObject);
            }
        }

        if(attackhighlights.Count > 0)
        {
            foreach (GameObject go in attackhighlights)
            {
                Destroy(go.gameObject);
            }
        }

        if(bm.NoMoveTiles.Count > 0)
        {
            foreach(GameObject go in bm.NoMoveTiles)
            {
                Destroy(go.gameObject);
            }
        }
    }
}
