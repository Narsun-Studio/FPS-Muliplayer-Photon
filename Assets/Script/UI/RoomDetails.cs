using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomDetails : MonoBehaviour
{
    public Text RoomName;
    public Button JoinButton;
    public RoomInfo roominfo;
    


    private void Start()
    {
        JoinButton.onClick.AddListener(OnJoinButtomClick);
    }

    private void OnJoinButtomClick()
    { 
        FindObjectOfType<Launcher>().JoinRoom(roominfo);   
    }

    public void OnLoad(RoomInfo roomInfo)
    {
        RoomName.text = roomInfo.Name;
        roominfo = roomInfo;
    }
}
