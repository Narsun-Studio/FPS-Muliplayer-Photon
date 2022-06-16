using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerDetaits : MonoBehaviourPunCallbacks
{

    public Player player;
    public Text nameText;


    public void LoadPlayer(Player _player)
    {
        player = _player;
        nameText.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }




}
