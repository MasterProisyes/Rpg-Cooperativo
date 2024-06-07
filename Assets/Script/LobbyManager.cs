using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public TMP_Text statusText;
    public Button createRoomButton;
    public Button joinRoomButton;

    void Start()
    {
        statusText.text = "Connecting to Photon...";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true; // Asegurarse de que las escenas se sincronicen automáticamente

        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;

        createRoomButton.onClick.AddListener(CreateRoom);
        joinRoomButton.onClick.AddListener(JoinRoom);
    }

    public override void OnConnectedToMaster()
    {
        statusText.text = "Connected to Master";
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            Debug.Log("Creating room: " + roomNameInput.text);
            PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 4 });
            statusText.text = "Creating Room...";
        }
        else
        {
            statusText.text = "Room name cannot be empty";
        }
    }

    public void JoinRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            Debug.Log("Joining room: " + roomNameInput.text);
            PhotonNetwork.JoinRoom(roomNameInput.text);
            statusText.text = "Joining Room...";
        }
        else
        {
            statusText.text = "Room name cannot be empty";
        }
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Joined Room: " + PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.LoadLevel("GameScene"); // Asegúrate de que "GameScene" está en las Build Settings
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        statusText.text = "Create Room Failed: " + message;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        statusText.text = "Join Room Failed: " + message;
    }
}
