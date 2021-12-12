using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointUI : MonoBehaviour
{
    public FPSPlayerManager player;

    public Text points;

    void Update()
    {
        points.text = player.points.ToString();
    }
}
