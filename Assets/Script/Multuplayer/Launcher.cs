using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;


public class Launcher : MonoBehaviourPunCallbacks
{
    public UIManager uiManager;
    List<RoomInfo> rooms;
 
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby");

       

        uiManager.LoadLobbyPanel();
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Join Room Failed");
        uiManager.LoadLobbyPanel();
    }




    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(uiManager.newRoomText.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(uiManager.newRoomText.text);
        uiManager.LoadLoadingPanel();
        
    }

    public override void OnCreatedRoom()
    {
      
        uiManager.LoadRoomPanel();
        uiManager.roomNameText.text = PhotonNetwork.CurrentRoom.Name.ToString();
        
        LoadPlayer();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("fail to create room");
        uiManager.LoadLobbyPanel();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        uiManager.LoadLoadingPanel();
    }

    public override void OnLeftRoom()
    {
        uiManager.LoadLobbyPanel();
    }

    public void FindRoom()
    {
        uiManager.LoadRoomList(rooms);
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        rooms = roomList;
        uiManager.LoadRoomList(rooms);
    }


    public void JoinRoom(RoomInfo roomInfo)
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
        uiManager.LoadLoadingPanel();
    }

    public override void OnJoinedRoom()
    {
       
        print("Room Joined");
        uiManager.LoadRoomPanel();
        LoadPlayer();

        uiManager.startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        uiManager.startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Join Room Failed");
        uiManager.LoadLobbyPanel();
    }

    public void LoadPlayer()
    {
        Player[] player = PhotonNetwork.PlayerList;
        uiManager.LoadPlayerList(player);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        uiManager.UpdatePlayerList(newPlayer);
    }


    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
