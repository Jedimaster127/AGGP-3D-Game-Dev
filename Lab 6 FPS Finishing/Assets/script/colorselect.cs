using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorselect : MonoBehaviour
{
    public Slider red;

    public Slider green;

    public Slider blue;

    public Image imageColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color color = new Color(red.value, green.value, blue.value);

        imageColor.color = color;

        photonmanager.instance.red = red.value;
        photonmanager.instance.green = green.value;
        photonmanager.instance.blue = blue.value;
    }
}
