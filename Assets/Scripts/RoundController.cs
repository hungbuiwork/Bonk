using System;
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

    public UnitManager unitManager;

    public bool phase_debug;
    public int scoreTeam1;
    public int scoreTeam2;

    public Action beginBattle, beginPrep, beginStandby;

    // Start is called before the first frame update
    void Start()
    {
        
        timeManager.MethodFromTimer();
        if (unitManager == null)
        {
            Debug.LogError("MUST ATTACH UNIT MANAGER");
        }
        unitManager.onWin += WinRound;
        scoreTeam1 = scoreTeam2 = 0;
    }

    void WinRound(int i)
    {
        if (i == 1)
        {
            scoreTeam1 += 1;
        }
        else if (i == 2)
        {
            scoreTeam2 += 1;
        }

        //TODO: add conditions to win the game

        NextPhase();
    }

    void WinGame(int i)
    {
        //TODO: Do stuff that wins the game
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
            //if(phase_debug){Debug.Log("PHASE: STANDBY @@@");}
            
        }

        else if(isPrepPhase)
        {
            //if(phase_debug){Debug.Log("PHASE: PREP 1 @@@");}
            //Debug.Log("prep phasing");

            //player 1 turn start
            
        }
        
        else if(isPrepPhase2)
        {
            //if(phase_debug){Debug.Log("PHASE: PREP 2 @@@");}
            //CheckTimeOver();

            //player 2 turn start
            
            
        }
        else if (isBattlePhase)
        {
            if(phase_debug){Debug.Log("PHASE: BATTLE @@@");}
            unitManager.OnUpdate(); //Call the update function
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
            beginPrep();
            timeManager.StartPrep();
        }

        else if(isPrepPhase)
        {
            isPrepPhase = false;
            isPrepPhase2 = true;
            beginPrep();
            timeManager.StartPrep();
        }

        else if(isPrepPhase2)
        {
            isPrepPhase2 = false;
            isBattlePhase = true;
            if (beginBattle != null) beginBattle();
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
