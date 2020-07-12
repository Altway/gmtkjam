using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTrafficSimple : MonoBehaviour
{
    public TrafficLightsimple traffic1;
    public TrafficLightsimple traffic2;

    public Renderer lightpieton1;
    public Renderer lightpieton2;

    public Material red;
    public Material green;
    public float _switchingLightTimer = 1.5f;
    public bool lightUp;
    // Start is called before the first frame update
    void Start()
    {
        lightUp = false;
    }

    public void OnMouseUp()
    {
       
        traffic1.StartCoroutine(traffic1.SwitchColor(_switchingLightTimer));      
        traffic2.StartCoroutine(traffic2.SwitchColor(_switchingLightTimer));


        if (lightUp == true)
        {
            lightpieton1.material = red;
            lightpieton2.material = red;
            lightUp = false;
        }
        else if (lightUp==false)
        {
            lightpieton1.material = green;
            lightpieton2.material = green;
            lightUp = true;
        }
        
        
    }
}
