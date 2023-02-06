using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    public Timer timeManager;

    public float currentTime;

    public bool isStandbyPhase;
    public bool isPrepPhase; // prep phase
    public bool isPrepPhase2; // prep phase for player 2
    public bool isBattlePhase;


    // Start is called before the first frame update
    void Start()
    {
        
        timeManager.MethodFromTimer();

    }

    // Update is called once per frame
    void Update()
    {
        if(timeManager.CheckTimeOver())
        {
            NextPhase();
        }

        if(isStandbyPhase)
        {
            //
        }

        else if(isPrepPhase)
        {
            //Debug.Log("prep phasing");
            //player 1 turn start
            
        }
        
        else if(isPrepPhase2)
        {
            //CheckTimeOver();
            //player 2 turn start
            
            
        }
    }

    public void StartGame()
    {
        isStandbyPhase = true;
    }



    public void NextPhase()
    {
        if(isStandbyPhase)
        {
            isStandbyPhase = false;
            isPrepPhase = true;

            timeManager.StartPrep();
        }

        else if(isPrepPhase)
        {
            isPrepPhase = false;
            isPrepPhase2 = true;

            timeManager.StartPrep();
        }

        else if(isPrepPhase2)
        {
            isPrepPhase2 = false;
            isBattlePhase = true;

            timeManager.StartBattle();
        }

        else if(isBattlePhase)
        {
            isBattlePhase = false;
            isStandbyPhase = true;

            timeManager.StartStandby();
        }

        else{}

    }
}
