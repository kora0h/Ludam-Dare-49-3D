using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PureThink.Tools;

public class TileManager : MonoBehaviour
{
    //public Transform[] holder;
    public GameObject groundPrefab;
    public GameObject areaUIPrefab;
    public int groundSizeX = 2;
    public GameObject groundHolder;
    public GameObject areaHolder;
    public GameObject areaUIHolder;
    public GameObject areaInfoUI;
    
    public GameObject[] level0;
    public GameObject[] level1;
    public GameObject[] level2;
    public GameObject buildingParticle;
    public RandomEventBase currentRandomEvent;

    public int currentMouseGroundID = -1;
    public int currentStandGroundID = 4;


    public List<GameObject> groundList;
    public List<GameObject> areaList;

    public List<GameObject> areaUIList;


    public bool bDragUI;

    public GameObject playerObject;
    public Vector3 offsetValue = new Vector3(0f, 0.2f, 0f);

    public TMP_Text _TcostSleep;
    public TMP_Text _TcostSupply;
    public TMP_Text _TgainSleep;
    public TMP_Text _TgainSupply;

    public Transform btnSleepTrans;
    public Transform btnSupplyTrans;
    public Button btn_Sleep;
    public Button btn_Supply;

    public GameObject randomEventUIHolder;
    public TMP_Text _TrandomEventInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnGround();
            RefreshAreaUI();
        }



    }

    private void LateUpdate()
    {
        if (currentMouseGroundID != -1)
        {
            areaInfoUI.SetActive(true);

            if (currentMouseGroundID == currentStandGroundID)
            {
                _TcostSleep.text = "";
                _TcostSupply.text = "";
                if (areaList[currentMouseGroundID].GetComponent<AreaTile>().bRecoveredSupply)
                {
                    _TgainSupply.text = "";
                }
                else
                {
                    _TgainSupply.text = areaList[currentMouseGroundID].GetComponent<AreaTile>().gainSupply.ToString();
                }
                _TgainSleep.text = areaList[currentMouseGroundID].GetComponent<AreaTile>().gainSleep.ToString();

            }
            else
            {
                _TcostSleep.text = areaList[currentMouseGroundID].GetComponent<AreaTile>().costSleep.ToString();
                _TcostSupply.text = areaList[currentMouseGroundID].GetComponent<AreaTile>().costSupply.ToString();
                _TgainSleep.text = areaList[currentMouseGroundID].GetComponent<AreaTile>().gainSleep.ToString();
                _TgainSupply.text = areaList[currentMouseGroundID].GetComponent<AreaTile>().gainSupply.ToString();

            }

        }
        else
        {

            _TcostSleep.text = "";
            _TcostSupply.text = "";
            _TgainSleep.text = "";
            _TgainSupply.text = "";
            areaInfoUI.SetActive(false);
        }


        ShowInteractButton();

    }

    public void NewGame()
    {
        SpawnGround();
        RefreshAreaUI();
        AddListenerToButton();

        playerObject.transform.position = Vector3.zero + offsetValue;
        currentStandGroundID = 4;
    }

    public void SpawnGround()
    {
        groundList.Clear();
        groundList.TrimExcess();
        areaList.Clear();
        areaList.TrimExcess();



        for (int i = 0; i < groundHolder.transform.childCount; i++)
        {
            Destroy(groundHolder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < areaHolder.transform.childCount; i++)
        {
            Destroy(areaHolder.transform.GetChild(i).gameObject);
        }

        int ID = 0;
        for (int i = 0; i < 3; i++)
        {

            for (int j = 0; j < 3; j++)
            {

                GameObject newGroud = Instantiate(groundPrefab, new Vector3(i, 0, j) * groundSizeX - new Vector3(groundSizeX, 0, groundSizeX), Quaternion.identity);
                newGroud.name = string.Format("{0} : {1}", i, j);
                newGroud.transform.SetParent(groundHolder.transform);
                groundList.Add(newGroud);


                int randNum = Random.Range(0, level0.Length);
                GameObject newOth1 = Instantiate(level0[randNum], new Vector3(i, 0, j) * groundSizeX - new Vector3(groundSizeX, 0, groundSizeX), Quaternion.identity);
                newOth1.transform.SetParent(areaHolder.transform);
                areaList.Add(newOth1);
                AreaTile newArea = newOth1.GetComponent<AreaTile>();

                //Cost and Recover Function
                AreaTileCostAndRecoverFactory(newArea);



                newGroud.GetComponent<GroundTile>().ID = ID;
                newOth1.GetComponent<AreaTile>().ID = ID;
                
                ID++;

            }
        }
    }

    public void RefreshAreaUI()
    {
        areaUIList.Clear();
        areaUIList.TrimExcess();


        for (int i = 0; i < areaUIHolder.transform.childCount; i++)
        {
            Destroy(areaUIHolder.transform.GetChild(i).gameObject);
        }

        //current staus : sleep and supply
        //more sleep more new area
        int sleepValue = -1;
        if(GameManager.Instance.playerNeeds.Needs_Sleep <= 3)
        {
            sleepValue = 1;
        }
        else if (GameManager.Instance.playerNeeds.Needs_Supply <= 5)
        {
            sleepValue = 2;
        }
        else if (GameManager.Instance.playerNeeds.Needs_Supply <= 7)
        {
            sleepValue = 3;
        }
        else
        {
            sleepValue = 4;
        }

        //more supply more high value area
        int supplyValue = -1;
        if (GameManager.Instance.playerNeeds.Needs_Supply <= 4)
        {
            supplyValue = 0;
        }
        else if (GameManager.Instance.playerNeeds.Needs_Supply <= 7)
        {
            supplyValue =1;
        }
        else
        {
            supplyValue = 1;
        }


        int maxNum = Random.Range(0, sleepValue);
        for (int i = 0; i < maxNum; i++)
        {
            GameObject newUIObj = Instantiate(areaUIPrefab, areaUIHolder.transform);
            DragableAreaUI newUI = newUIObj.GetComponent<DragableAreaUI>();


            int[] levs = new int[3] { 0, 1, 2 };
            float[] weighted;

            switch (supplyValue)
            {
                case 1:
                    weighted = GameManager.Instance.playerNeeds.weight_0_3;
                    break;
                case 2:
                    weighted = GameManager.Instance.playerNeeds.weight_4_7;
                    break;
                case 3:
                    weighted = GameManager.Instance.playerNeeds.weight_8_10;
                    break;
                default:
                    weighted = GameManager.Instance.playerNeeds.weight_0_3;
                    break;
            }

            int option = WeightedChoice.GetRandom(levs, weighted);

            switch (option)
            {
                case 0:
                    newUI.referAreaPrefab = level0[Random.Range(0, level0.Length)];
                    //newUI.GetComponent<Image>().sprite = newUI.referAreaPrefab.GetComponent<AreaTile>().Icon;
                    break;
                case 1:
                    newUI.referAreaPrefab = level1[Random.Range(0, level1.Length)];
                    break;
                case 2:
                    newUI.referAreaPrefab = level2[Random.Range(0, level2.Length)];
                    break;
                default:
                    newUI.referAreaPrefab = level0[Random.Range(0, level0.Length)];
                    break;
            }
            newUI.GetComponent<Image>().sprite = newUI.referAreaPrefab.GetComponent<AreaTile>().Icon;

            //newUI.referAreaPrefab = level0[Random.Range(0, level0.Length)];

            areaUIList.Add(newUIObj);
        }

    }

    public bool ReplaceArea(GameObject newArea, DragableAreaUI ui)
    {
        if (currentStandGroundID == currentMouseGroundID)
            return false;

        //if (newArea.GetComponent<MeshFilter>().sharedMesh == areaList[currentMouseGroundID].GetComponent<MeshFilter>().sharedMesh)
        //    return false;

        if (newArea.GetComponent<AreaTile>().areaType == areaList[currentMouseGroundID].GetComponent<AreaTile>().areaType)
            return false;

        areaUIList.Remove(ui.gameObject);

        Destroy(areaList[currentMouseGroundID].gameObject);

        GameObject newOth1;

        newOth1 = Instantiate(newArea, areaList[currentMouseGroundID].transform.position, Quaternion.identity);
        newOth1.transform.SetParent(areaHolder.transform);

        AreaTile area = newOth1.GetComponent<AreaTile>();
        area.ID = currentMouseGroundID;
        AreaTileCostAndRecoverFactory(area);

        areaList[currentMouseGroundID] = newOth1;


        //play sound
        GameManager.Instance.soundManager.PlayeReplace();

        //Play particle effect
        Instantiate(buildingParticle, areaList[currentMouseGroundID].transform.position, Quaternion.identity);

        return true;
    }

    public void PlayerMoveToNear()
    {
        if (bDragUI)
            return;


        if(currentMouseGroundID == currentStandGroundID +3 || currentMouseGroundID == currentStandGroundID + 1 
            || currentMouseGroundID == currentStandGroundID - 1 || currentMouseGroundID == currentStandGroundID - 3)
        {
            //print("move");
            //Cost Calculator

            GameManager.Instance.playerNeeds.CostSleep(areaList[currentMouseGroundID].GetComponent<AreaTile>().costSleep);
            GameManager.Instance.playerNeeds.CostSupply(areaList[currentMouseGroundID].GetComponent<AreaTile>().costSupply);

            //area record
            GameManager.Instance.AreaRecord(areaList[currentMouseGroundID].GetComponent<AreaTile>());

            //apply listener to button

            AddListenerToButton();


            //reset sleep fuction
            ResetCurrentAreaSleepFuction();

            playerObject.transform.position = groundList[currentMouseGroundID].transform.position + offsetValue;
            currentStandGroundID = currentMouseGroundID;

            //Death Check 
            GameManager.Instance.playerNeeds.DeathCheck();

            //player move sound
            GameManager.Instance.soundManager.PlayCombine();

            return;

        }




    }

    public void AreaTileCostAndRecoverFactory(AreaTile area)
    {
        int costSleepValue;
        int costSupplyValue;
        int gainSleepValue;
        int gainSupplyValue;

        if (area.areaType == AreaType.City)
        {
            costSleepValue = Random.Range(3, 5);
            costSupplyValue = Random.Range(3, 5);
            gainSleepValue = Random.Range(3, 6);
            gainSupplyValue = Random.Range(-2, 6);
        }
        else if(area.areaType == AreaType.Town)
        {
            costSleepValue = Random.Range(2, 5);
            costSupplyValue = Random.Range(2, 5);
            gainSleepValue = Random.Range(2, 6);
            gainSupplyValue = Random.Range(-2, 5);
        }
        else if(area.areaType == AreaType.Wild)
        {
            costSleepValue = Random.Range(2, 6);
            costSupplyValue = Random.Range(2, 6);
            gainSleepValue = Random.Range(1, 5);
            gainSupplyValue = Random.Range(-3, 4);
        }
        else
        {
            costSleepValue = -1;
            costSupplyValue = -1;
            gainSleepValue = -1;
            gainSupplyValue = -1;
        }

        area.costSleep = costSleepValue;
        area.costSupply = costSupplyValue;
        area.gainSleep = gainSleepValue;
        area.gainSupply = gainSupplyValue;


    }

    public void AddListenerToButton()
    {
        btn_Sleep.onClick.RemoveAllListeners();
        btn_Sleep.onClick.AddListener(delegate { GameManager.Instance.playerNeeds.GainSleep(areaList[currentStandGroundID].GetComponent<AreaTile>().DoRecoverSleep()); });
        btn_Sleep.onClick.AddListener(delegate { RandomEventForSleep(areaList[currentStandGroundID].GetComponent<AreaTile>());  });

        btn_Sleep.onClick.AddListener(delegate { DisableCurrentAreaSleepFuction(); });
        btn_Sleep.onClick.AddListener(delegate { GameManager.Instance.UpdateRound(); });
        btn_Sleep.onClick.AddListener(delegate { RefreshAreaUI(); });

        //Random event


        btn_Supply.onClick.RemoveAllListeners();
        btn_Supply.onClick.AddListener(delegate { GameManager.Instance.playerNeeds.GainSupply(areaList[currentStandGroundID].GetComponent<AreaTile>().gainSupply); });
        btn_Supply.onClick.AddListener(delegate { RandomEventForSupply(areaList[currentStandGroundID].GetComponent<AreaTile>());  });


        btn_Supply.onClick.AddListener(delegate { DisableCurrentAreaSupplyFuction(); });

        //Random event


    }

    public void RandomEventForSleep(AreaTile area)
    {
        randomEventUIHolder.SetActive(false);

        //chance to add , 15%
        bool apply = Random.Range(0, 100) < 15 ? true : false;
        if (!apply)
            return;

        int value = 0;
        //good or bad
        bool goodEvent = Random.Range(0, 2) == 0 ? true : false;
        bool common = Random.Range(0, 100) < 20 ? true : false;

        if (common)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventCommon(true);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventCommon(true);

            }
        }
        else if (area.areaType == AreaType.City)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventCity(true);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventCity(true);

            }
        }
        else if (area.areaType == AreaType.Town)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventTown(true);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventTown(true);

            }
        }
        else if (area.areaType == AreaType.Wild)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventWild(true);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventWild(true);

            }
        }

        //get output and store for ui

        //refresh ui

        RefreshRandomEventUI();

        if (goodEvent)
        {
            GameManager.Instance.playerNeeds.GainSleep(currentRandomEvent.Value_Sleep);
        }
        else
        {
            GameManager.Instance.playerNeeds.CostSleep(currentRandomEvent.Value_Sleep);
        }


    }
    public void RandomEventForSupply(AreaTile area)
    {
        randomEventUIHolder.SetActive(false);

        //chance to add , 15%
        bool apply = Random.Range(0, 100) < 15 ? true : false;
        if (!apply)
            return;

        int value = 0;
        //good or bad
        bool goodEvent = Random.Range(0, 2) == 0 ? true : false;
        bool common = Random.Range(0, 100) < 20 ? true : false;

        if (common)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventCommon(false);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventCommon(false);

            }
        }
        else if (area.areaType == AreaType.City)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventCity(false);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventCity(false);

            }
        }
        else if (area.areaType == AreaType.Town)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventTown(false);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventTown(false);

            }
        }
        else if (area.areaType == AreaType.Wild)
        {
            if (goodEvent)
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetGoodEventWild(false);
            }
            else
            {
                currentRandomEvent = GameManager.Instance.eventHolder.GetBadEventWild(false);

            }
        }

        //get output and store for ui

        //refresh ui

        RefreshRandomEventUI();

        if (goodEvent)
        {
            GameManager.Instance.playerNeeds.GainSupply(currentRandomEvent.Value_Supply);
        }
        else
        {
            GameManager.Instance.playerNeeds.CostSupply(currentRandomEvent.Value_Supply);
        }

    }

    public void RefreshRandomEventUI()
    {
        //update ui with current random info
        randomEventUIHolder.SetActive(true);
        _TrandomEventInfo.text = currentRandomEvent.EventInfo;


        //sound effect 
        GameManager.Instance.soundManager.PlayAlert();

    }

    public void ShowInteractButton()
    {
        Vector3 btnSleepPos = Camera.main.WorldToScreenPoint(btnSleepTrans.position);
        btn_Sleep.transform.position = btnSleepPos;
        Vector3 btnSupplyPos = Camera.main.WorldToScreenPoint(btnSupplyTrans.position);
        btn_Supply.transform.position = btnSupplyPos;

        if(currentMouseGroundID == currentStandGroundID)
        {
            if (areaList[currentStandGroundID].GetComponent<AreaTile>().bRecoveredSleep)
            {
                btn_Sleep.gameObject.SetActive(false);
            }
            else
            {
                btn_Sleep.gameObject.SetActive(true);
            }


            if (areaList[currentStandGroundID].GetComponent<AreaTile>().bRecoveredSupply)
            {
                btn_Supply.gameObject.SetActive(false);
            }
            else
            {
                btn_Supply.gameObject.SetActive(true);
            }
        }
        else
        {
            btn_Sleep.gameObject.SetActive(false);
            btn_Supply.gameObject.SetActive(false);
        }
        

    }

    public void ResetCurrentAreaSleepFuction()
    {
        areaList[currentStandGroundID].GetComponent<AreaTile>().bRecoveredSleep = false;

    }

    public void DisableCurrentAreaSleepFuction()
    {
        areaList[currentStandGroundID].GetComponent<AreaTile>().bRecoveredSleep = true;

    }

    public void DisableCurrentAreaSupplyFuction()
    {
        areaList[currentStandGroundID].GetComponent<AreaTile>().bRecoveredSupply = true;
        //print("Disable");
    }

}
