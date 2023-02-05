using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPhase : MonoBehaviour
{

    [Header("Component")]
    public TextMeshProUGUI phase;
    public RoundController round;


    // Start is called before the first frame update
    void Start()
    {
        /*
        phase_list.Add(round.isStandbyPhase);
        phase_list.Add(round.isPrepPhase);
        phase_list.Add(round.isPrepPhase2);
        phase_list.Add(round.isBattlePhase);*/
    } 

    // Update is called once per frame
    void Update()
    {
        if(round.isStandbyPhase)
        {
            phase.text = "Standby Phase";
        }

        else if(round.isPrepPhase)
        {
            phase.text = "Preparation Phase 1";
        }

        else if(round.isPrepPhase2)
        {
            phase.text = "Preparation Phase 2";
        }

        else if(round.isBattlePhase)
        {
            phase.text = "Battle Phase";
        }


        //SetPhase();
    }


    private void SetPhase()
    {
        //phase.text = .ToString();
    }


}
