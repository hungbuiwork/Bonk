using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    /// <summary>
    /// Displays the counter text in the UI.
    /// </summary>

    [SerializeField]
    private TextMeshProUGUI counter; //Counter text
    [SerializeField]
    private RoundController round;

    [SerializeField]
    private bool stopCount = false;
    [SerializeField]
    public int numOfRounds = 0;


    void Update()
    {
        if(round.isStandbyPhase && !stopCount)
        {
            numOfRounds += 1;
            counter.text = "Rounds: " + numOfRounds.ToString();
            stopCount = true;
        }

        else if(round.isStandbyPhase == false)
        {
            stopCount = false;
        }
    }
}
