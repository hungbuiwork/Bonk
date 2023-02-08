using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
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


    // Update is called once per frame
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
