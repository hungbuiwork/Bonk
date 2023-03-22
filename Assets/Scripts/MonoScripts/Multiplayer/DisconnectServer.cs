using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking.Types;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class DisconnectServer : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    /// <summary>
    /// This script is used to moderate the server for connectivity, severing a connection if a player has left the room or if a player quits to main menu.
    /// This script also displays information about disconnection status and room name
    /// </summary>
    [SerializeField] private string disconnectionScene;
    [SerializeField] private TextMeshProUGUI disconnectionText;
    [SerializeField] private TextMeshProUGUI roomName;
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene(disconnectionScene);
    }

    public void Disconnect()
    {
        Time.timeScale = 1f;
        PhotonNetwork.Disconnect();
    }
    public override void OnPlayerLeftRoom(Player otherplayer)
    {
        if (disconnectionText != null)
        {
            disconnectionText.text = "A player has disconnected. Returning to main menu in 5 seconds";
            Invoke("Disconnect", 5);
        }
    }
    public void Awake()
    {
        roomName.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
    }
}
