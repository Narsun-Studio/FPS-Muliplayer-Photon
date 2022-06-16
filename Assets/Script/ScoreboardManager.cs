using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ScoreboardManager : MonoBehaviourPunCallbacks
{
    public Transform container;
    public ScoreboardItem scoreboardItemPrefab;
    Dictionary<Player, ScoreboardItem> scoreboardItems = new Dictionary<Player, ScoreboardItem>();


    private void Start()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddPlayerInScoreboard(player);
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerInScoreboard(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemovePlayerFromScoreboard(otherPlayer);
        
    }


    public void AddPlayerInScoreboard(Player p)
    {
        ScoreboardItem scoreboardItem = Instantiate(scoreboardItemPrefab, container);
        scoreboardItem.Initialize(p);
        scoreboardItems.Add(p, scoreboardItem);

    }
    public void RemovePlayerFromScoreboard(Player p)
    {
        Destroy(scoreboardItems[p].gameObject);
        scoreboardItems.Remove(p);
    }


    public void UpdateDeathCount(int actorNumber, int deathCount) 
    {
        //print(actorNumber);
        scoreboardItems[PhotonNetwork.PlayerList[actorNumber - 1]].UpdateDeath(deathCount);
    }

    public void UpdateKillCount(int actorNumber, int killCount)
    {
        //print(actorNumber);
        scoreboardItems[PhotonNetwork.PlayerList[actorNumber - 1]].UpdateKills(killCount);
    }


}
