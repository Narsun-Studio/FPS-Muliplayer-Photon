using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class DisplayUsername : MonoBehaviour
{
    public PhotonView playerPV;
    public Text userName;


    private void Start()
    {
        if (playerPV.IsMine)
        {
            userName.transform.gameObject.SetActive(false);
        }

        userName.text = playerPV.Owner.NickName;
    }
}
