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
            nextPhase.text = "Start Preparation Phase";
        }

        else if(round.isPrepPhase)
        {
            phase.text = "Preparation Phase 1";
            nextPhase.text = "Ready!";
        }

        else if(round.isPrepPhase2)
        {
            phase.text = "Preparation Phase 2";
            nextPhase.text = "Ready!";
        }

        else if(round.isBattlePhase)
        {
            phase.text = "Battle Phase";
            nextPhase.text = "End Battle";
        }


    }

}
