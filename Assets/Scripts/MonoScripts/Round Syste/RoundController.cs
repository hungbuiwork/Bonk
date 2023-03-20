using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoundController : MonoBehaviour
{
    /// <summary>
    /// Controls the game's rounds, score, and phases between rounds.
    /// This script's implementation works with both local and online multiplayer, depending
    /// on if useOnlineRounds is ticked to be true.
    /// </summary>
    /// 
    static RoundController instance;

    [SerializeField]
    private Timer timeManager;

    public float currentTime;

    public bool isStandbyPhase;
	public bool isWaitingPhase; // when only one player is done w standby
    public bool isPrepPhase; // prep phase
    public bool isPrepPhase2; // prep phase for player 2
	public bool isWaitingPhase2; // when only one player is done w prep
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
	
	[SerializeField]
	private GameObject nextButton;
	
	[SerializeField]
	private bool useOnlineRounds;
	
	private PhotonView photonView;
	private bool p1Ready;
	private bool p2Ready;

    private void Awake()
    {
        //Singleton
        if (instance != null)
        {
            Debug.Log("THERE IS ALREADY AN INSTANCE OF THE ROUND CONTROLLER");
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        unitManager.onFirstTroopPlacedTeam1 += timeManager.Resume;
        unitManager.onFirstTroopPlacedTeam2 += timeManager.Resume;
		
		if (useOnlineRounds)
		{
			photonView = PhotonView.Get(this);
		}
    }
    void Start()
    {
        
        timeManager.MethodFromTimer();
        if (unitManager == null)
        {
            Debug.LogError("MUST ATTACH UNIT MANAGER");
        }
        unitManager.onWin += WinRound;
        scoreTeam1 = scoreTeam2 = 0;
		p1Ready = p2Ready = false;
    }

    void WinRound(int i)
    {
        ///Determine if a battle has ended, and give points accordingly
        if (i == 1)
        {
            scoreTeam1 += 1;
        }
        else if (i == 2)
        {
            scoreTeam2 += 1;
        }

        // if tied, both team +0
        else if (i == 3)
        {
            scoreTeam1 += 0;
            scoreTeam2 += 0;
        }

        checkWin();
        NextPhase();
    }

    void checkWin()
    {
        // check if round limit is reached. If so, declare a player as the winner.
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
        ///Currently inactive(so players can play endless). 
        ///Stops the game, and declares a specific player as the winner.
        Debug.LogError("There is a winner");
        //Time.timeScale = 0f;

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
            //Call next phase if the timer for that phase hits 0
            NextPhase();
        }
		
		else if (isBattlePhase)
        {
            //Call the update function for all units
            unitManager.OnUpdate(); 
        }
		
		if(isWaitingPhase || isWaitingPhase2)
		{
            //If in either of the online waiting phases, check to see if both players are ready. If so, move to the next phase.
            timeManager.Pause();
            if (p1Ready && p2Ready)
			{
				NextPhase();
                timeManager.Resume();
			}
		}
		else
		{
			p1Ready = false;
			p2Ready = false;
		}
    }


	[PunRPC]
	void P1Ready()
	{
        //used w/ PUN to communicate player is ready
		p1Ready = true;
	}
	
	[PunRPC]
	void P2Ready()
	{
        //used w/ PUN to communicate player is ready
        p2Ready = true;
	}

    public void NextPhase()
    {
        ///Swaps the current phase to the next phase
       
        if(isStandbyPhase)
        {
            isStandbyPhase = false;
			if (useOnlineRounds)
			{
                //Online
				nextButton.SetActive(false);
                isWaitingPhase = true;
				if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
				{
                    //if player 1, communicate to the server you are ready
					photonView.RPC("P1Ready", RpcTarget.All);
				}
				else
				{
                    //if player 2, communicate to the server you are ready
					photonView.RPC("P2Ready", RpcTarget.All);
				}
			}
			else
			{
                //Local
                //move straight to prep phase
				isPrepPhase = true;
				timeManager.StartPrep();
                timeManager.Pause();
			}
        }
		
		else if(isWaitingPhase)
		{
            ///The online phase before prep phase
			nextButton.SetActive(true);
			isWaitingPhase = false;
			if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
			{
				isPrepPhase = true;
			}
			else
			{
				isPrepPhase2 = true;
			}
			timeManager.StartPrep();
			photonView.RPC("P1Ready", RpcTarget.All);
			photonView.RPC("P2Ready", RpcTarget.All);
		}

        else if(isPrepPhase)
        {
            //player 1's prep phase
            isPrepPhase = false;
			if (useOnlineRounds)
			{
				photonView.RPC("P1Ready", RpcTarget.All);
				isWaitingPhase2 = true;
				nextButton.SetActive(false);
			}
			else
			{
				isPrepPhase2 = true;
				timeManager.StartPrep();
				timeManager.Pause();
			}
        }

        else if(isPrepPhase2)
        {
            //player 2's prep phase
            isPrepPhase2 = false;
			if (useOnlineRounds)
			{
				photonView.RPC("P2Ready", RpcTarget.All);
				isWaitingPhase2 = true;
				nextButton.SetActive(false);
			}
			else
			{
				isBattlePhase = true;
				if (beginBattle != null) beginBattle();
				timeManager.StartBattle();
				nextButton.SetActive(false);
			}
        }
		
		else if(isWaitingPhase2)
		{
            //the online phase before the battle begins
			isWaitingPhase2 = false;
			isBattlePhase = true;
			if (beginBattle != null) beginBattle();
			timeManager.StartBattle();
			photonView.RPC("P1Ready", RpcTarget.All);
			photonView.RPC("P2Ready", RpcTarget.All);
		}

        else if(isBattlePhase)
        {
            //the phase that a battle is happening
			nextButton.SetActive(true);
            isBattlePhase = false;
            isStandbyPhase = true;
            unitManager.DestroyAllUnits();
            beginStandby();
            timeManager.StartStandby();
        }
    }
}
