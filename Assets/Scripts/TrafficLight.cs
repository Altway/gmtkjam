using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    private enum COLOR {Red, Orange, Green};
    private TrafficLight.COLOR _color; 
    private int _switching_light_timer = 3;

    // Start is called before the first frame update
    void Start()
    {
        _color = COLOR.Red;
        gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
    }

    public void OnMouseUp() {
        StartCoroutine(sleepSeconds(_switching_light_timer));
    }

    IEnumerator sleepSeconds(int seconds)
    {
         if (_color == COLOR.Red) {
            _color = COLOR.Green;
            gameObject.GetComponent<Renderer>().material.color = new Color(0f,1f,0f, 1);
        } else if (_color == COLOR.Orange) {
            Debug.Log("I am Orange, nothing happen");
        } else if (_color == COLOR.Green) {
            _color = COLOR.Orange;
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0.45f,0f, 1);
            yield return new WaitForSeconds(seconds);
            _color = COLOR.Red;
            gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
        }

    }
}
