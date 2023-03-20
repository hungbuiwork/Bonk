using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoundController : MonoBehaviour
{
    /// <summary>
    /// Controls the rounds and the phases inside.
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
		
		if(isWaitingPhase || isWaitingPhase2)
		{
            timeManager.Pause();
            if (p1Ready && p2Ready)
			{
				NextPhase();
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
		p1Ready = true;
	}
	
	[PunRPC]
	void P2Ready()
	{
		p2Ready = true;
	}

    public void NextPhase()
    {
        if(isStandbyPhase)
        {
            isStandbyPhase = false;
			//timeManager.Pause();
			if (useOnlineRounds)
			{
				nextButton.SetActive(false);
				isWaitingPhase = true;
				if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
				{
					photonView.RPC("P1Ready", RpcTarget.All);
				}
				else
				{
					photonView.RPC("P2Ready", RpcTarget.All);
				}
			}
			else
			{
				isPrepPhase = true;
				timeManager.StartPrep();
                timeManager.Pause();
			}
        }
		
		else if(isWaitingPhase)
		{
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
			//timeManager.Pause();
			photonView.RPC("P1Ready", RpcTarget.All);
			photonView.RPC("P2Ready", RpcTarget.All);
		}

        else if(isPrepPhase)
        {
            isPrepPhase = false;
			if (useOnlineRounds)
			{
				photonView.RPC("P1Ready", RpcTarget.All);
				isWaitingPhase2 = true;
				//timeManager.Pause();
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
            isPrepPhase2 = false;
			if (useOnlineRounds)
			{
				photonView.RPC("P2Ready", RpcTarget.All);
				isWaitingPhase2 = true;
				//timeManager.Pause();
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
			isWaitingPhase2 = false;
			isBattlePhase = true;
			if (beginBattle != null) beginBattle();
			timeManager.StartBattle();
			photonView.RPC("P1Ready", RpcTarget.All);
			photonView.RPC("P2Ready", RpcTarget.All);
		}

        else if(isBattlePhase)
        {
			nextButton.SetActive(true);
            isBattlePhase = false;
            isStandbyPhase = true;
            unitManager.DestroyAllUnits();
            beginStandby();
            timeManager.StartStandby();
        }

        else{}

    }
}
