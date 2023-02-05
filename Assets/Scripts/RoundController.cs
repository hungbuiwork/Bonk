using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    Timer timeManager;
    //public GameObject timeManager;

    public float currentTime;

    public bool isStandbyPhase;
    public bool isPrepPhase; // prep phase
    public bool isPrepPhase2; // prep phase for player 2
    public bool isBattlePhase;


    // Start is called before the first frame update
    void Start()
    {
        
        isStandbyPhase = true;
        isPrepPhase = false;
        isPrepPhase2 = false;
        isBattlePhase = false;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime = timeManager.getCurrentTime();

        if(isPrepPhase)
        {
            //player 1 turn start
            if (currentTime == 0)
            {
                isPrepPhase = false;
                isPrepPhase2 = true;
            }
        }
        
        else if(isPrepPhase2)
        {
            //player 2 turn start
        }
    }

    public void nextPhase()
    {
        if(isStandbyPhase)
        {
            isStandbyPhase = false;
            isPrepPhase = true;

            timeManager.startPrep();
        }

        if(isPrepPhase)
        {
            isPrepPhase = false;
            isPrepPhase2 = true;
        }

        if(isPrepPhase2)
        {
            isPrepPhase2 = false;
            isBattlePhase = true;
        }

        if(isBattlePhase)
        {
            isBattlePhase = false;
            isStandbyPhase = true;
        }

    }
}
