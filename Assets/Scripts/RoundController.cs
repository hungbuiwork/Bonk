using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{

    public float roundTime;
    public float timeVariable;

    
    public bool isBattlePhase;
    public bool isPrepPhase; // prep phase
    public bool isPrepPhase2; // prep phase for player 2

    // Start is called before the first frame update
    void Start()
    {
        isBattlePhase = false;
        isPrepPhase = true;

        timeVariable = Time.time + roundTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // prep phase starts
        if(isPrepPhase)
        {
            if(Time.time >= timeVariable)
            {
                isPrepPhase = false;
                isBattlePhase = true;
                return;
            }

            // organize troops...

        // battle phase    
        }
        else if(isBattlePhase)
        {
            // battling...
        }

    }
}
