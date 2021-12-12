using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class KingofHillManager : MonoBehaviour
{
    public static KingofHillManager instance;

    int maxScore = 250;

    public List<FPSPlayerManager> players = new List<FPSPlayerManager>();

    public GameObject winBoard;

    public Text winnername;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
            if(players[0].points == maxScore)
            {
                gameObject.GetPhotonView().RPC("EndGame", RpcTarget.AllBuffered, players[0].username.text);
            }
            else if(players[1].points == maxScore)
            {
                gameObject.GetPhotonView().RPC("EndGame", RpcTarget.AllBuffered, players[1].username.text);
            }
            else if(players[2].points == maxScore)
            {
                gameObject.GetPhotonView().RPC("EndGame", RpcTarget.AllBuffered, players[2].username.text);
            }
            else if(players[3].points == maxScore)
            {
                gameObject.GetPhotonView().RPC("EndGame", RpcTarget.AllBuffered, players[3].username.text);
            }
    }

    public void AddPlayers(FPSPlayerManager p)
    {
        if (!players.Contains(p))
        {
            players.Add(p);
        }
    }

   /*[PunRPC]
    void AddPlayer(FPSPlayerManager p)
    {
        if(!players.Contains(p))
        {
            players.Add(p);
        }
    }*/

    [PunRPC]
    IEnumerator EndGame(string playername)
    {
        winBoard.SetActive(true);
        winnername.text = playername;
        yield return new WaitForSecondsRealtime(5);
        photonmanager.instance.LeaveRoom();
    }
    
}
