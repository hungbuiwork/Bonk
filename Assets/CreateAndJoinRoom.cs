using Photon.Pun;
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
    [SerializeField] private Color feedbackColor = Color.red;

    private void SetFeedbackText(string text)
    {
        feedback.color = feedbackColor;
        feedback.text = text;
    }
    public void CreateRoom()
    {
        if (createInput.text == "")
        {
            SetFeedbackText("Name the room before creating!");
            return;
        }
        PhotonNetwork.CreateRoom(createInput.text);
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
}
