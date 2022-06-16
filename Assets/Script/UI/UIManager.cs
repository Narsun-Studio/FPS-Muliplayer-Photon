using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class UIManager : MonoBehaviour
{

    public GameObject LoadingPanel;
    public GameObject LobbyPanel;
    public GameObject CreateRoomPanel;
    public GameObject RoomPanel;
    public GameObject FindRoom;


    public Text newRoomText;
    public Text roomNameText;

    public InputField userNameTextField;

    public GameObject roomlistContainer;
    public RoomDetails roomPrefab;

    public GameObject playerListContainer;
    public PlayerDetaits playerPrefab;

    public Button startButton;

    private void Start()
    {
        LoadLoadingPanel();

        userNameTextField.text = PlayerPrefs.GetString("Username", "");
    }

    public void LoadLoadingPanel()
    {
        LoadingPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        CreateRoomPanel.SetActive(false);
        RoomPanel.SetActive(false);
        FindRoom.SetActive(false);
    }

    public void LoadLobbyPanel()
    {
        LoadingPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        CreateRoomPanel.SetActive(false);
        RoomPanel.SetActive(false);
        FindRoom.SetActive(false);
    }

    public void LoadCreateRoomPanel()
    {
        LoadingPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        CreateRoomPanel.SetActive(true);
        RoomPanel.SetActive(false);
        FindRoom.SetActive(false);
    }
    public void LoadRoomPanel()
    {
        LoadingPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        CreateRoomPanel.SetActive(false);
        RoomPanel.SetActive(true);
        FindRoom.SetActive(false);
    }

    public void LoadFindRoomPanel ()
    {
        LoadingPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        CreateRoomPanel.SetActive(false);
        RoomPanel.SetActive(false);
        FindRoom.SetActive(true);
    }

    public void LoadRoomList(List<RoomInfo> rooms )
    {
        foreach(Transform t in roomlistContainer.transform)
        {
            Destroy(t.gameObject);
        }

        for(int i = 0; i < rooms.Count; i++)
        {
            //if (rooms[i].PlayerCount <= 0) return;
            if (rooms[i].RemovedFromList) continue;
            RoomDetails room = Instantiate(roomPrefab,roomlistContainer.transform);
            room.OnLoad(rooms[i]);
        }
    }

    public void LoadPlayerList(Player[] player)
    {
        foreach(Transform t in playerListContainer.transform)
        {
            Destroy(t.gameObject);
        }

        for(int i = 0; i < player.Length; i++)
        {
            PlayerDetaits playerDetails = Instantiate(playerPrefab, playerListContainer.transform);
            playerDetails.LoadPlayer(player[i]);
        }
    }

    public void UpdatePlayerList(Player player)
    {
        PlayerDetaits playerDetails = Instantiate(playerPrefab, playerListContainer.transform);
        playerDetails.LoadPlayer(player);
    }
}
