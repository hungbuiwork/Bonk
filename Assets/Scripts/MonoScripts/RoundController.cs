using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    /// <summary>
    /// Controls the rounds and the phases inside.
    /// </summary>
    [SerializeField]
    private Timer timeManager;

    public float currentTime;

    public bool isStandbyPhase;
    public bool isPrepPhase; // prep phase
    public bool isPrepPhase2; // prep phase for player 2
    public bool isBattlePhase;

    [SerializeField]
    private UnitManager unitManager;

    [SerializeField]
    private bool phase_debug;
    public int scoreTeam1;
    public int scoreTeam2;
    public int maxScore;

    public Action beginBattle, beginStandby;

    [SerializeField]
    private RoundCounter roundCounter; 
    public int maxRound;

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

        // if tied, both team +1
        else if (i == 3)
        {
            scoreTeam1 += 1;
            scoreTeam2 += 1;
        }

        checkWin();

        NextPhase();
    }

    void checkWin()
    {
        // check if round limit is reached
        if (roundCounter.numOfRounds >= 20)
        {
            Debug.LogError("max round reached");

            if (scoreTeam1 == scoreTeam2) {WinGame(3); }
            else if (scoreTeam1 > scoreTeam2) {WinGame(1); }
            else if (scoreTeam2 > scoreTeam1) {WinGame(2); }
        }

        // check if tie
        else if (scoreTeam1 == scoreTeam2 && scoreTeam1 >= maxScore) {WinGame(3); }

        else if (scoreTeam1 >= maxScore) {WinGame(1); }
        else if (scoreTeam2 >= maxScore) {WinGame(2); }
    }

    void WinGame(int i)
    {
        //TODO: add text and other UI stuff

        Debug.LogError("There is a winner");
        Time.timeScale = 0f;

        if (i == 1)
        {
            //player 1 wins
            Debug.LogError("player 1 wins");
        }

        else if (i == 2)
        {
            //player 2 wins
            Debug.LogError("player 2 wins");
        }

        else if (i == 3)
        {
            // tied
            Debug.LogError("both players tied");
        }
    }

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
            //if(phase_debug){Debug.Log("PHASE: BATTLE @@@");}
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
            if (beginBattle != null) beginBattle();
            timeManager.StartBattle();
        }

        else if(isBattlePhase)
        {
            isBattlePhase = false;
            isStandbyPhase = true;
            beginStandby();
            timeManager.StartStandby();
        }

        else{}

    }
}
