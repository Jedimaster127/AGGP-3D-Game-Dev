using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpui : MonoBehaviour
{
    public FPSPlayerManager player;

    public Slider uiSlider;
    // Start is called before the first frame update
    void Start()
    {
        uiSlider.maxValue = player.hpSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        uiSlider.value = player.hpSlider.value;
    }
}
