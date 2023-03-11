using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;
    [SerializeField] private TMP_Text feedback;
    [SerializeField] private TMP_Text rooms;
    [SerializeField] private Color feedbackColor = Color.red;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }
    private void SetFeedbackText(string text)
    {
        feedback.color = feedbackColor;
        feedback.text = text;
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // Limit to 2 maximum players

        if (createInput.text == "")
        {
            SetFeedbackText("Name the room before creating!");
            return;
        }
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, null);
    }

    public void JoinRoom()
    {
        if (joinInput.text == "")
        {
            SetFeedbackText("Give a room name before joining!");
            return;
        }

        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Multiplayer");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        SetFeedbackText(message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        SetFeedbackText(message);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        string s = "Online Rooms\n\n";
        foreach (RoomInfo room in roomList)
        {
            if (room.PlayerCount == 0)
            {
                continue;
            }
            s += room.Name + ": " + room.PlayerCount.ToString() + "/2\n";
        }
        rooms.text = s;
    }
}
