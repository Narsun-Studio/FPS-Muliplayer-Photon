using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;


public class ScoreboardItem : MonoBehaviour
{
    public Text name;
    public Text kills;
    public Text deaths;

    public void Initialize(Player player)
    {
        name.text = player.NickName;
        deaths.text = "0";
    }
    public void UpdateDeath(int deathCount)
    {
        /*int death= int.Parse(deaths.text.ToString());
        death++;
        deaths.text = death.ToString();  */
        deaths.text = deathCount.ToString();
    }

    public void UpdateKills(int killCount)
    {
        /* int killCount = int.Parse( kills.text.ToString());
         killCount += 1;

         kills.text = killCount.ToString();  */
        kills.text = killCount.ToString();
    }


}
