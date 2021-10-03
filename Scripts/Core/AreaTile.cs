using System.Collections;
using UnityEngine;

public class AreaTile : MonoBehaviour
{
    public int ID;
    public Sprite Icon;

    public AreaType areaType;
    
    public int costSleep;
    public int costSupply;

    public int gainSleep;
    public int gainSupply;

    public bool bRecoveredSleep;
    public bool bRecoveredSupply;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (bRecoveredSupply)
        {
            gainSupply = 0;
        }
    }

    public int DoRecoverSleep()
    {
        bRecoveredSleep = true;
        gainSleep -= 1;
        if (gainSleep < -3 && areaType == AreaType.City)
        {
            gainSleep = -3;
        }
        else if(gainSleep<-1 && areaType == AreaType.Town)
        {
            gainSleep = -1;
        }
        return gainSleep;
    }

}

public enum AreaType
{
    City,
    Town,
    Wild,
    Empty,
}