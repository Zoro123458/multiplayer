using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;
    public GameObject LoadingScreen;
    public TMP_Text LoadingText;

    public GameObject CreateRoomScreen;

    public TMP_InputField roomNameInput;
    public GameObject CreatedRoomScreen;
    public TMP_Text roomNameText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        LoadingScreen.SetActive(true);
        LoadingText.text = "Connecting...";

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        LoadingText.text = "Joining lobby...";
    }

    public override void OnJoinedLobby()
    {
        LoadingScreen.SetActive(false);
    }

    public void OpenCreateRoomScreen()
    {
        CreateRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if(!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;



            PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);

            LoadingScreen.SetActive(true);
            LoadingText.text = "Creating Room...";
        }


    }

    public override void OnCreatedRoom()
    {
        LoadingScreen.SetActive(false);

        CreatedRoomScreen.SetActive(true);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;


    }

    public void leavRoom()
    {
        CreatedRoomScreen.SetActive(false);
        CreateRoomScreen.SetActive(false);
        LoadingScreen.SetActive(true);

        LoadingText.text = "Leaving Room";

        PhotonNetwork.LeaveRoom();

    }



}
