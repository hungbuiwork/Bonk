using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// Manages different audiosources, and plays/pauses them accordingly. 
    /// Uses the Observer pattern to listen to when the battle begins /ends
    /// </summary>
    [SerializeField]
    private AudioSource standbyMusic;
    [SerializeField]
    private AudioSource battleMusic;
    private void Start()
    {
        RoundController roundController = GameObject.FindObjectOfType<RoundController>();
        roundController.beginStandby += SwitchStandbyMusic;
        roundController.beginBattle += SwitchBattleMusic;
        battleMusic.Play();
        battleMusic.Pause();
    }

    private void SwitchBattleMusic() 
    {
        standbyMusic.Pause();
        battleMusic.UnPause();
    }
    private void SwitchStandbyMusic()
    {
        battleMusic.Pause();
        standbyMusic.UnPause();
    }
}
