using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAdder : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<FPSPlayerManager>().points != FPSGameManager.instance.maxScore)
        {
            other.gameObject.GetComponent<FPSPlayerManager>().points++;
        }
    }
}
