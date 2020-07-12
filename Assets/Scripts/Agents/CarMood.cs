using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMood : MonoBehaviour
{
    public Sprite[] waitingStates;
    public Sprite rageState;
    public Sprite deadState;
    public SpriteRenderer mySpriteRend;
    public AgentMoodManager myManager;
    public Car myCar;
    void Start()
    {
        myCar = transform.parent.GetComponent<Car>();
        mySpriteRend = gameObject.GetComponent<SpriteRenderer>();
        myManager = GameObject.Find("GameManager").GetComponent<AgentMoodManager>();
        myManager.carMoods.Add(this);
    }
    void OnDestroy(){
        myManager.carMoods.Remove(this);
    }
}
