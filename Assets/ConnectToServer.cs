using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject failureDisplay;
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        Invoke("ShowConnectionFailure", 10);
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        ShowConnectionFailure();
    }
    private void ShowConnectionFailure()
    {
        failureDisplay.SetActive(true);
    }




}
