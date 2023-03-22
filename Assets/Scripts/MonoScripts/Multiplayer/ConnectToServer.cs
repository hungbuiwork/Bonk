using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Connects to the PHOTON server, and redirects to the lobby scene when connected.
    /// </summary>
    [SerializeField] private GameObject failureDisplay;
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        //if in 10 seconds does not connect, show a connection failure
        Invoke("ShowConnectionFailure", 10);
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        //if disconnected, show connection failure
        ShowConnectionFailure();
    }
    private void ShowConnectionFailure()
    {
        failureDisplay.SetActive(true);
    }




}
