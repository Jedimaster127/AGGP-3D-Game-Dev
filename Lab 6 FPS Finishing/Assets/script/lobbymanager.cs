using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;
public class lobbymanager : MonoBehaviour
{

    public int playercount = 0;

    public int playFPS = 0;

    public static lobbymanager instance;

    public List<TextMeshProUGUI> playerNames = new List<TextMeshProUGUI>();

    private void Awake()
    {
        instance = this;
        if(gameObject.GetPhotonView().IsMine)
        {
            gameObject.GetPhotonView().RPC("PlayerCount", RpcTarget.AllBuffered, playercount);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playercount == 4 && playFPS == 0)
        {
            Debug.Log("4 players are here");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("KingofHill");
            }
            gameObject.GetPhotonView().RPC("GamePlaying", RpcTarget.AllBuffered);
        }

    }

    [PunRPC]
    void PlayerJoined()
    {
        playercount += 1;
    }

    [PunRPC]
    void PlayerLeft()
    {
        playercount -= 1;
    }

    [PunRPC]
    void GamePlaying()
    {
        playFPS++;
    }

    [PunRPC]
    void PlayerUser(string name)
    {
        
        playerNames[playercount - 1].text = name;
        
    }

    [PunRPC]
    void MapLoad()
    {

    }
}
