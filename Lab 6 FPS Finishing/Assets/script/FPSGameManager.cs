using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;
public class FPSGameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public List<GameObject> spawnPoints = new List<GameObject>();
    int point;
    public int maxScore = 250;
    public static FPSGameManager instance;
    public List<int> players = new List<int>();
    public List<FPSPlayerManager> playerManagers = new List<FPSPlayerManager>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        point = Random.Range(0, spawnPoints.Count);

        bool contains = false;
        if (players.Contains(PhotonNetwork.LocalPlayer.ActorNumber))
        {
            contains = true;
        }

        if (!contains)
        {
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[point].transform.position, spawnPoints[point].transform.rotation);
            gameObject.GetPhotonView().RPC("UpdateDictionary", RpcTarget.AllBuffered, player.GetPhotonView().ControllerActorNr, false);
            AddPlayers(player.GetComponent<FPSPlayerManager>());
        }

    }

    private void Update()
    {
        if (playerManagers[0].points == maxScore)
        {
            KingofHillManager.instance.EndGame(playerManagers[0].username.text);
        }
    }

    public void AddPlayers(FPSPlayerManager p)
    {
        if (!playerManagers.Contains(p))
        {
            playerManagers.Add(p);
        }
    }

    [PunRPC]
    public void UpdateDictionary(int number, bool remove = false)
    {
        if (!players.Contains(number) && !remove)
        {
            players.Add(number);
        }

        if (players.Contains(number) && remove)
        {
            players.Remove(number);
        }
    }
}
