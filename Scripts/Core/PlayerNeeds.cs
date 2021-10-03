using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNeeds : MonoBehaviour
{

    [SerializeField] private TMP_Text _Tsleep;
    [SerializeField] private TMP_Text _Tsupply;
    [SerializeField] public TMP_Text _TtimeBeforeDie;

    //supply effect new area level
    //city - town - wild
    public float[] weight_0_3 = new float[] { 0.05f, 0.35f, 0.6f };
    public float[] weight_4_7 = new float[] { 0.1f, 0.4f, 0.5f };
    public float[] weight_8_10 = new float[] { 0.5f, 0.3f, 0.2f };

    public int timeBeforeDie = 0;
    private bool bCountDown;

    public int Needs_Sleep { get; private set; }
    public int Needs_Supply { get; private set; }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        _Tsleep.text = string.Format("{0}", Needs_Sleep);
        _Tsupply.text = string.Format("{0}", Needs_Supply);
        _TtimeBeforeDie.text = bCountDown == false ? string.Format(">=4") : string.Format("{0}", timeBeforeDie);
    }

    public void NewGame()
    {
        Needs_Sleep = 5;
        Needs_Supply = 5;
        bCountDown = false;
        timeBeforeDie = 4;
    }

    public void CostSleep(int value)
    {
        Needs_Sleep -= value;
        ReCal();
    }
    public void CostSupply(int value)
    {
        Needs_Supply -= value;
        ReCal();

        GameManager.Instance.NeedsRecord(true, value);

    }
    public void GainSleep(int value)
    {
        Needs_Sleep += value;
        ReCal();
    }
    public void GainSupply(int value)
    {
        Needs_Supply += value;
        ReCal();

        GameManager.Instance.NeedsRecord(false, value);

    }
    private void ReCal()
    {
        Needs_Sleep = Mathf.Clamp(Needs_Sleep, 0, 10);
        Needs_Supply = Mathf.Clamp(Needs_Supply, 0, 10);


    }


    public void DeathCheck()
    {

        if (Needs_Sleep == 10)
        {
            GameManager.Instance.soundManager.PlayWhenFullSleep();
        }
        if (Needs_Sleep == 0)
        {
            if (bCountDown)
            {
                timeBeforeDie -= 1;
                if (timeBeforeDie < 1)
                {
                    GameManager.Instance.GameOver();
                }
            }
            else
            {
                bCountDown = true;
                timeBeforeDie = 4;
            }
        }
        else
        {
            bCountDown = false;
            timeBeforeDie = 0;
        }
    }
}
