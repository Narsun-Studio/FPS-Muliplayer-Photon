using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;

    GameObject controller;

    public int deathCount = 0;
    public int killCount = 0;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        print(pv.Owner.ActorNumber);
    }

    private void Start()
    {
        if (pv.IsMine)
        {
            CreateController();
        }
    }

    public void CreateController()
    {
        //Instantiate Player Controlller
        Transform _spawnPoint = SpawnManager.Instance.GetRandomSpawnPoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonNetwork", "PlayerController"), _spawnPoint.position , _spawnPoint.rotation,0,new object[] {pv.ViewID});
    }
    public void Die(int actorNumber)
    {

        pv.RPC(nameof(RPC_UpdateDeathCount), RpcTarget.All, pv.Owner.ActorNumber);
        PhotonView otherPv = PhotonNetwork.GetPhotonView(actorNumber);
        PhotonView parentPv = otherPv.GetComponent<PlayerController>().playerManager.GetComponent<PhotonView>();
        parentPv.RPC(nameof(RPC_UpdateKillCount), RpcTarget.All ,parentPv.OwnerActorNr);


        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        PhotonNetwork.Destroy(controller);
        yield return new WaitForSeconds(2);
        CreateController();
    }
                        
    [PunRPC]
    void RPC_UpdateDeathCount(int owneractorNumber )
    {
       
        if (pv.OwnerActorNr == owneractorNumber) {
            deathCount++;
        }
        FindObjectOfType<ScoreboardManager>().UpdateDeathCount((int) owneractorNumber, (int)deathCount);
  
    }


    [PunRPC]
    void RPC_UpdateKillCount(int otherActorNumber)
    {
        if (pv.OwnerActorNr == otherActorNumber)
        {
            killCount++;
        }
        FindObjectOfType<ScoreboardManager>().UpdateKillCount(otherActorNumber,killCount);
    }
  
}
