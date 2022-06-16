using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }

    }

    public override void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonNetwork", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }


}
