﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficLight : MonoBehaviour
{
    public LightColor _color; 
    public float _switchingLightTimer = 1.5f;
    public List<CityNode> responsibleNode;
    public TrafficLight conjugatedTrafficLight;
    public WalkerLight conjugatedWalkerLight;

    void Awake()
    {
        if (_color == LightColor.Red)
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
        else if (_color == LightColor.Green)
            gameObject.GetComponent<Renderer>().material.color = new Color(0f,1f,0f, 1);
        else if (_color == LightColor.Orange)
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0.45f,0f, 1);
    }

    public void OnMouseUp() {
        StartCoroutine(SwitchColor(_switchingLightTimer));
    }

    public void SetColor(LightColor color) {
        _color = color;
        if (color == LightColor.Red) {
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
            ModifyCityNode(responsibleNode, NodeType.Pavement);
        }
        else if (color == LightColor.Green) {
            gameObject.GetComponent<Renderer>().material.color = new Color(0f,1f,0f, 1);
            ModifyCityNode(responsibleNode, NodeType.Street);
        }
        else if (color == LightColor.Orange) {
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0.45f,0f, 1);
            //ModifyCityNode(responsibleNode, NodeType.Building);
        }
    }
 
    public void ModifyCityNode(List<CityNode> cityNode_list, NodeType nodetype) {
        foreach(CityNode node in responsibleNode) {
            node.type = nodetype;
            node.ChangeColor();
        }
    }

    IEnumerator SwitchColor(float seconds)
    {
        if (_color == LightColor.Red) {
            if (conjugatedTrafficLight != null) {
                conjugatedWalkerLight.SwitchColor();
                conjugatedTrafficLight.SetColor(LightColor.Orange);
                yield return new WaitForSeconds(seconds);
                conjugatedTrafficLight.SetColor(LightColor.Red);
            }
            SetColor(LightColor.Green);
        } else if (_color == LightColor.Orange) {
            Debug.Log("I am Orange, nothing happen if you click on me");
        } else if (_color == LightColor.Green) {
            if (conjugatedTrafficLight != null)
                conjugatedWalkerLight.SwitchColor();
                conjugatedTrafficLight.SetColor(LightColor.Green);
            SetColor(LightColor.Orange);
            yield return new WaitForSeconds(seconds);
            SetColor(LightColor.Red);
        }
    }

}
