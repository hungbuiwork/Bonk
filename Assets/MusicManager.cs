using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip standbyMusic;
    [SerializeField]
    private AudioClip battleMusic;
    private void Start()
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }
        RoundController roundController = GameObject.FindObjectOfType<RoundController>();
        roundController.beginStandby += SwitchStandbyMusic;
        roundController.beginBattle += SwitchBattleMusic;
    }

    // Update is called once per frame
    private void SwitchBattleMusic() 
    {
        audio.clip = battleMusic;
        audio.Play();
    }
    private void SwitchStandbyMusic()
    {
        audio.clip = standbyMusic;
        audio.Play();
    }
}
