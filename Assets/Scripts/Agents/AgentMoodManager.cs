using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMoodManager : MonoBehaviour
{
    public List<AgentMood> agentMoods = new List<AgentMood>();
    public Transform camPos;

    void Start(){
        camPos = GameObject.Find("Main Camera").transform;
    }
    void Update()
    {
        foreach(AgentMood mood in agentMoods){
            mood.transform.LookAt(camPos);
        }
    }
}
