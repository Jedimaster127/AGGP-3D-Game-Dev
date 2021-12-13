using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class KingofHillManager : MonoBehaviour
{
    public static KingofHillManager instance;

    public GameObject winBoard;

    public Text winnername;

    private void Awake()
    {
        instance = this;
    }

    public void EndGame(string user)
    {
        gameObject.GetPhotonView().RPC("SetEndGame", RpcTarget.AllBuffered, user);
    }

    [PunRPC]
    IEnumerator SetEndGame(string playername)
    {
        winBoard.SetActive(true);
        winnername.text = playername;
        yield return new WaitForSecondsRealtime(5);
        photonmanager.instance.LeaveRoom();
    }
    
}
