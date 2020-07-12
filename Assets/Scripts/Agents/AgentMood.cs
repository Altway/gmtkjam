using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMood : MonoBehaviour
{
    public Sprite[] waitingStates;
    public Sprite rageState;
    public Sprite[] calmStates;
    public Sprite fearState;
    public Sprite deadState;
    public int calmStateIndex;
    public SpriteRenderer mySpriteRend;
    public AgentMoodManager myManager;
    public Walker myWalker;
    void Start()
    {
        myWalker = transform.parent.parent.GetComponent<Walker>();
        calmStateIndex = Random.Range(0, calmStates.Length);
        mySpriteRend = gameObject.GetComponent<SpriteRenderer>();
        mySpriteRend.sprite = calmStates[calmStateIndex];
        myManager = GameObject.Find("GameManager").GetComponent<AgentMoodManager>();
        myManager.agentMoods.Add(this);
    }
    void OnDestroy(){
        myManager.agentMoods.Remove(this);
    }
}
