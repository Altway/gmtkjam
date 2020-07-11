using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    private enum COLOR {Red, Orange, Green};
    private TrafficLight.COLOR _color; 
    public float _switchingLightTimer = 1.5f;
    public List<CityNode> responsibleNode;
    public List<TrafficLight> conjugatedTrafficLight;

    // Start is called before the first frame update
    void Awake()
    {
        
        /*for(int i = 0){
            _color = COLOR.Red;
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
        }*/
        foreach(CityNode node in responsibleNode) {
            node.type = NodeType.Street;
            node.ChangeColor();
        }
    }

    public void OnMouseUp() {
        StartCoroutine(sleepSeconds(_switchingLightTimer));
        //conjugatedTrafficLight.OnMouseUp();
    }

    IEnumerator sleepSeconds(float seconds)
    {
         if (_color == COLOR.Red) {
            _color = COLOR.Green;
            gameObject.GetComponent<Renderer>().material.color = new Color(0f,1f,0f, 1);
            foreach(CityNode node in responsibleNode) {
                node.type = NodeType.Street;
                node.ChangeColor();
            }
        } else if (_color == COLOR.Orange) {
            Debug.Log("I am Orange, nothing happen");
            foreach(CityNode node in responsibleNode) {
                node.type = NodeType.Street;
                node.ChangeColor();
            }
        } else if (_color == COLOR.Green) {
            _color = COLOR.Orange;
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0.45f,0f, 1);
            foreach(CityNode node in responsibleNode) {
                node.type = NodeType.Street;
                node.ChangeColor();
            }
            yield return new WaitForSeconds(seconds);
            _color = COLOR.Red;
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
            foreach(CityNode node in responsibleNode) {
                node.type = NodeType.Pavement;
                node.ChangeColor();
            }
        }

    }
}
