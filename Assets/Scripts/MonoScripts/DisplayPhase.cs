using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPhase : MonoBehaviour
{
    /// <summary>
    /// Displays phase and scores of the game
    /// </summary>
    [Header("Component")]
    [SerializeField]
    private TextMeshProUGUI phase;
    [SerializeField]
    private TextMeshProUGUI nextPhase;
    [SerializeField]
    private TextMeshProUGUI scorePlayer1;
    [SerializeField]
    private TextMeshProUGUI scorePlayer2;
    [SerializeField]
    private RoundController round;


    void Update()
    {
        //possible improvement in future: dont update every frame/only update text when an event is fired
        scorePlayer1.text = round.scoreTeam1.ToString();
        scorePlayer2.text= round.scoreTeam2.ToString();
        if(round.isStandbyPhase)
        {
            phase.text = "Standby Phase";
            nextPhase.text = "Start Preparation";
        }

        else if(round.isPrepPhase)
        {
            phase.text = "Player 1 Preparation";
            nextPhase.text = "Ready!";
        }

        else if(round.isPrepPhase2)
        {
            phase.text = "Player 2 Preparation";
            nextPhase.text = "Ready!";
        }
		
		else if(round.isWaitingPhase || round.isWaitingPhase2)
        {
            phase.text = "Waiting For Opponent";
        }

        else if(round.isBattlePhase)
        {
            phase.text = "Battle Phase";
        }


    }

}
