using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerLight : MonoBehaviour
{
    public LightColor _color; 
    public float _switchingLightTimer = 1.5f;
    public List<CityNode> responsibleNode;
    public WalkerLight conjugatedWalkerLight;
    public TrafficLight conjugatedTrafficLight;

    public GameObject lightPieton;
    public Material greenMat;
    public Material redMat;


    public void SetColor(LightColor color) {
        _color = color;
        if (color == LightColor.Red) {
            _color = LightColor.Red;
            lightPieton.GetComponent<Renderer>().material = redMat;
           // gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
        } else {
            _color = LightColor.Green;
            lightPieton.GetComponent<Renderer>().material = greenMat;
            // gameObject.GetComponent<Renderer>().material.color = new Color(0f,1f,0f, 1);
        }
    }
    
    public void SwitchColor()
    {
        if (_color == LightColor.Red) {
            if (conjugatedWalkerLight != null)
               conjugatedWalkerLight.SetColor(LightColor.Red);
            SetColor(LightColor.Green);
        } else if (_color == LightColor.Green) {
            if (conjugatedWalkerLight != null)
                conjugatedWalkerLight.SetColor(LightColor.Green);
            SetColor(LightColor.Red);
        }
    }
    
}
