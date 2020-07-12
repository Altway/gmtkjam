using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrafficLight : MonoBehaviour
{
    public LightColor _color; 
    public float _switchingLightTimer = 1.5f;
    public List<CityNode> responsibleNode;

    public GameObject lightRed;
    public GameObject lightOrange;
    public GameObject lightGreen;

    public Material neutralMat;
    public Material redMat;
    public Material orangeMat;
    public Material greenMat;

    public Light redSpot;

    public void MEH() {
        StartCoroutine(SwitchColor(_switchingLightTimer));
    }

    public void SetColor(LightColor color) {
        _color = color;
        if (color == LightColor.Red) {
            redSpot.color = Color.red;
            lightRed.GetComponent<Renderer>().material = redMat;
            lightOrange.GetComponent<Renderer>().material = neutralMat;
            lightGreen.GetComponent<Renderer>().material = neutralMat;
            ModifyCityNode(responsibleNode, NodeType.Pavement);
        }
        else if (color == LightColor.Green) {
            redSpot.color = Color.green;
            lightRed.GetComponent<Renderer>().material = neutralMat;
            lightOrange.GetComponent<Renderer>().material = neutralMat;
            lightGreen.GetComponent<Renderer>().material = greenMat;
            ModifyCityNode(responsibleNode, NodeType.Street);
        }
        else if (color == LightColor.Orange) {
            redSpot.color = new Color(1f, 0.45f, 0f, 1);
            lightRed.GetComponent<Renderer>().material = neutralMat;
            lightOrange.GetComponent<Renderer>().material = orangeMat;
            lightGreen.GetComponent<Renderer>().material = neutralMat;
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
            SetColor(LightColor.Green);
        } else if (_color == LightColor.Orange) {
            Debug.Log("I am Orange, nothing happen if you click on me");
        } else if (_color == LightColor.Green) {
            SetColor(LightColor.Orange);
            yield return new WaitForSeconds(seconds);
            SetColor(LightColor.Red);
        }
    }

}
