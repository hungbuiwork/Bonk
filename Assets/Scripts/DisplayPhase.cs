using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPhase : MonoBehaviour
{

    [Header("Component")]
    public TextMeshProUGUI phase;
    public TextMeshProUGUI nextPhase;
    public RoundController round;


    // Update is called once per frame
    void Update()
    {
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
