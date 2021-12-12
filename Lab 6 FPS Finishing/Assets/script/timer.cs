using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class timer : MonoBehaviour
{
    public float timeValue = 120;

    public Text theTimer;

    void Update()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                gameObject.GetPhotonView().RPC("OnTimeUp", RpcTarget.AllBuffered);
            }

            gameObject.GetPhotonView().RPC("DisplayTime", RpcTarget.AllBuffered, timeValue);
        }
        
    }

    [PunRPC]
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        theTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    [PunRPC]
    void OnTimeUp()
    {
        photonmanager.instance.LeaveRoom();

        Cursor.lockState = CursorLockMode.None;
    }
}
