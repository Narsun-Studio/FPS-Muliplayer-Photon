using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UsernameManager : MonoBehaviour
{
    public UIManager uiManager;

    private void Start()
    {
        if (PlayerPrefs.GetString("Username", "").Equals("") || PlayerPrefs.GetString("Username") == null)
        {

            PhotonNetwork.NickName = "Player" + UnityEngine.Random.Range(0, 1000).ToString("0000");
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("Username");
        }
        uiManager.userNameTextField.text = PhotonNetwork.NickName;

        uiManager.userNameTextField.text = PhotonNetwork.NickName;
      
    }

    public void UsernameInputField_OnValueChanged()
    {
        PhotonNetwork.NickName = uiManager.userNameTextField.text;
        PlayerPrefs.SetString("Username", PhotonNetwork.NickName);
    }

}
