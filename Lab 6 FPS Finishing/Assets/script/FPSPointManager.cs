using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class FPSPointManager : MonoBehaviour
{
    public int playercount = 0;

    public static FPSPointManager instance;

    public List<TextMeshProUGUI> playerNames = new List<TextMeshProUGUI>();

    public List<TextMeshProUGUI> playerScores = new List<TextMeshProUGUI>();

    private void Awake()
    {
        instance = this;
        if (gameObject.GetPhotonView().IsMine)
        {
            gameObject.GetPhotonView().RPC("PlayerCounter", RpcTarget.AllBuffered, playercount);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void PlayerCounter()
    {
        playercount++;
    }

    [PunRPC]
    void NameAdder(string name)
    {
        playerNames[playercount - 1].text = name;
    }

    [PunRPC]
    void PointDetermin(int score, int playerNum)
    {
        playerScores[playerNum].text = score.ToString();
    }
}
