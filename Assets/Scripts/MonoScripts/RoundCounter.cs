using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{

    public TextMeshProUGUI counter;
    public RoundController round;
    
    public bool stopCount = false;
    public int numOfRounds = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
