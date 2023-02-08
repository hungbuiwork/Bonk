using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPhase : MonoBehaviour
{

    [Header("Component")]
    public TextMeshProUGUI phase;
    public TextMeshProUGUI nextPhase;
    public TextMeshProUGUI scorePlayer1;
    public TextMeshProUGUI scorePlayer2;
    public RoundController round;


    // Update is called once per frame
    void Update()
    {
        //possible improvement: dont update every frame/only update text when an event is fired
        scorePlayer1.text = "P1 Score: " + round.scoreTeam1.ToString();
        scorePlayer2.text= "P2 Score: " + round.scoreTeam2.ToString();
        if(round.isStandbyPhase)
        {
            phase.text = "Standby Phase";
            nextPhase.text = "Start Preparation Phase";
        }

        else if(round.isPrepPhase)
        {
            phase.text = "Preparation Phase 1";
            nextPhase.text = "Next Preparation Phase";
        }

        else if(round.isPrepPhase2)
        {
            phase.text = "Preparation Phase 2";
            nextPhase.text = "Start Battle Phase";
        }

        else if(round.isBattlePhase)
        {
            phase.text = "Battle Phase";
            nextPhase.text = "End Battle (debugging purpose)";
        }


    }

}
