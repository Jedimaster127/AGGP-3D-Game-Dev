using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class gameplaymanager : MonoBehaviour
{
    public GameObject PlayerManager;

    private void Start()
    {
        if(!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);

            return;
        }

        if(PlayerManager)
        {
            PhotonNetwork.Instantiate(PlayerManager.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.Log("you forgot a playermanager prefab");
        }
    }
}
