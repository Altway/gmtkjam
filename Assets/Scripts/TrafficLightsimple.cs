using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficLightsimple : MonoBehaviour
{
    public LightColor _color; 
    public float _switchingLightTimer = 1.5f;
    public List<CityNode> responsibleNode;
    public TrafficLight conjugatedTrafficLight;
    public WalkerLight conjugatedWalkerLight;

    public GameObject lightRed;
    public GameObject lightOrange;
    public GameObject lightGreen;

    public Material neutralMat;
    public Material redMat;
    public Material orangeMat;
    public Material greenMat;

    public Light redSpot;
    public Light orangeSpot;
    public Light greenSpot;

    void Awake()
    {
        /*
        if (_color == LightColor.Red)
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
        else if (_color == LightColor.Green)
            gameObject.GetComponent<Renderer>().material.color = new Color(0f,1f,0f, 1);
        else if (_color == LightColor.Orange)
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0.45f,0f, 1);
            */
    }

   

    public void SetColor(LightColor color) {
        _color = color;
        if (color == LightColor.Red) {
            redSpot.color = Color.red;
            lightRed.GetComponent<Renderer>().material = redMat;
            lightOrange.GetComponent<Renderer>().material = neutralMat;
            lightGreen.GetComponent<Renderer>().material = neutralMat;
            //gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
            ModifyCityNode(responsibleNode, NodeType.Pavement);
        }
        else if (color == LightColor.Green) {
            redSpot.color = Color.green;
            lightRed.GetComponent<Renderer>().material = neutralMat;
            lightOrange.GetComponent<Renderer>().material = neutralMat;
            lightGreen.GetComponent<Renderer>().material = greenMat;
            //gameObject.GetComponent<Renderer>().material.color = new Color(0f,1f,0f, 1);
            ModifyCityNode(responsibleNode, NodeType.Street);
        }
        else if (color == LightColor.Orange) {
            redSpot.color = new Color(1f, 0.45f, 0f, 1);
            lightRed.GetComponent<Renderer>().material = neutralMat;
            lightOrange.GetComponent<Renderer>().material = orangeMat;
            lightGreen.GetComponent<Renderer>().material = neutralMat;
            //gameObject.GetComponent<Renderer>().material.color = new Color(1f,0.45f,0f, 1);
            //ModifyCityNode(responsibleNode, NodeType.Building);
        }
    }
 
    public void ModifyCityNode(List<CityNode> cityNode_list, NodeType nodetype) {
        foreach(CityNode node in responsibleNode) {
            node.type = nodetype;
            node.ChangeColor();
        }
    }

    public IEnumerator SwitchColor(float seconds)
    {
        if (_color == LightColor.Red)
        {
           
            SetColor(LightColor.Green);

        }
        else if (_color == LightColor.Orange)
        {
           
        }
        else if (_color == LightColor.Green)
        {
           
            SetColor(LightColor.Orange);
            yield return new WaitForSeconds(seconds);
            SetColor(LightColor.Red);
        }
    }

}
