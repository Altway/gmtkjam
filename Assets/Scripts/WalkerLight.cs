using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerLight : MonoBehaviour
{
    public LightColor _color; 
    public float _switchingLightTimer = 1.5f;
    public GameObject lightPieton;
    public Material greenMat;
    public Material redMat;

    public bool invert = false;


    public void SetColor(LightColor color) {
        _color = color;
        if (color == LightColor.Red) {
            _color = LightColor.Red;
            lightPieton.GetComponent<Renderer>().material = redMat;
        } else {
            _color = LightColor.Green;
            lightPieton.GetComponent<Renderer>().material = greenMat;
        }
    }
    
    public void SwitchColor()
    {
        if (_color == LightColor.Red) {
            SetColor(LightColor.Green);
        } else if (_color == LightColor.Green) {
            SetColor(LightColor.Red);
        }
    }
    
}
