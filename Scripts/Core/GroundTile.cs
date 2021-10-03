using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundTile : MonoBehaviour
{
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseOver()
    {
        if(GameManager.Instance.bEnableGame)
            GameManager.Instance.tileManager.currentMouseGroundID = ID;
        //print(ID);
    }
    private void OnMouseExit()
    {
        GameManager.Instance.tileManager.currentMouseGroundID = -1;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.bEnableGame)
            GameManager.Instance.tileManager.PlayerMoveToNear();
    }
}
