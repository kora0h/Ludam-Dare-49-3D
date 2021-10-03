using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileManager tileManager;
    public PlayerNeeds playerNeeds;
    public SoundEffectManager soundManager;
    public RandomEventHolder eventHolder;
    
    public int Round;
    public TMP_Text _Tround;
    public GameObject tutorialPanel;
    public GameObject gameOverPanel;
    public GameObject titleImage;
    public TMP_Text _TendingWord;
    public bool bEnableGame;

    int wildernesses = 0;
    int town = 0;
    int city = 0;
    [SerializeField] int gainSupplies = 0;
    [SerializeField] int costSupplies = 0;


    public Vector3 targetTitlePosition = new Vector3(0f, 120f, 0f);
    public Vector3 targetCameraPosition = new Vector3(2f, -2f, 0f);
    public List<GameObject> disableWhenNewGame;
    public List<GameObject> enableWhenNewGame;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        //Open Game
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.orthographicSize = 2.25f;

        foreach (GameObject item in enableWhenNewGame)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in disableWhenNewGame)
        {
            item.SetActive(true);
        }

        tileManager = FindObjectOfType<TileManager>();
        playerNeeds = GetComponent<PlayerNeeds>();
        soundManager = FindObjectOfType<SoundEffectManager>();
        eventHolder = GetComponent<RandomEventHolder>();

        titleImage.transform.localPosition = Vector3.zero;
        Invoke("TitleMove", 0.4f);
    }

    private void TitleMove()
    {
        LeanTween.moveLocal(titleImage, targetTitlePosition, 2f);
    }

    private void LateUpdate()
    {
        _Tround.text = Round.ToString();
    }

    public void UpdateRound()
    {
        Round += 1;
        
    }
    public void GameOver()
    {


        //string consumedString = costSupplies == gainSupplies ? "all of them" : string.Format("{0} of them", costSupplies);

        gameOverPanel.SetActive(true);
        bEnableGame = false;

        int index = Random.Range(0, 3);
        switch (index)
        {
            case (0):
                _TendingWord.text = string.Format("Tiredness drags you down, shadow fade into the crowd.");
                break;
            case (1):
                _TendingWord.text = string.Format("Withered bone from the history, warm home from the memory.");
                break;
            case (2):
                _TendingWord.text = string.Format("Wandering feet, determined heart.");
                break;
        }


        //_TendingWord.text = string.Format("You are tired of this kind of wandering life, you have chosen to starve yourself to death. " +
        //    "\n You walked through {0} wildernesses, {1} town , {2} city." 
        //    //+"\n You gain {3} supplies and consumed {4}."
        //    , wildernesses, town, city/*, gainSupplies, consumedString*/);
    }

    public void AreaRecord(AreaTile area)
    {
        if(area.areaType== AreaType.Wild)
        {
            wildernesses += 1;
        }
        else if(area.areaType== AreaType.Town)
        {
            town += 1;
        }
        else
        {
            wildernesses += 1;
        }
    }

    public void NeedsRecord(bool cost, int value)
    {
        if (cost)
        {
            costSupplies += value;
        }
        else
        {
            gainSupplies+=value;
        }
    }

    public void RestartGame()
    {
        tileManager.NewGame();
        playerNeeds.NewGame();
        Round = 0;
        bEnableGame = true;

        wildernesses = 0;
        town = 0;
        city = 0;
        gainSupplies = 0;
        costSupplies = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableTutorial()
    {
        bEnableGame = !bEnableGame;
        tutorialPanel.gameObject.SetActive(!bEnableGame);
    }

    public void NewGame()
    {
        //camera movement
        LeanTween.moveLocal(Camera.main.gameObject, targetCameraPosition, 0.7f);
        StartCoroutine(CameraTransition(2f));
        //disable and enable some stuff
        foreach (GameObject item in disableWhenNewGame)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in enableWhenNewGame)
        {
            item.SetActive(true);
        }

        //call restart game

        bEnableGame = true;

        RestartGame();
    }

    IEnumerator CameraTransition(float time)
    {
        bool end = false;
        float t = 0f;
        while (!end) 
        {
            if (Camera.main.orthographicSize == 3.6f)
            {
                end = true;
            }

            t+= Time.deltaTime / time * 0.1f;

            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3.6f, t);

            yield return null;
        }

    }

}
