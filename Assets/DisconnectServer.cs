using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking.Types;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class DisconnectServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private string disconnectionScene;
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(disconnectionScene);
    }

    public void Disconnect()
    {
        Time.timeScale = 1f;
        PhotonNetwork.Disconnect();
    }
}
