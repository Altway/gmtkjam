using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public CityGraph cityGraph;
    public float cooldown;
    public float timer;
    public bool started;
    void Start()
    {
        cityGraph = GameObject.Find("CityGraph").GetComponent<CityGraph>();
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.W)){
            started = true;
        }
        if(!started){
            return;
        }
        timer += Time.deltaTime;
        if(timer >= cooldown){
            timer = 0;
            SpawnAgent();
        }
    }
    void SpawnAgent(){
        int tempInt = Random.Range(0, cityGraph.pedestrianEntranceNodes.Count);
        Walker tempWalk = Instantiate(agentPrefab, cityGraph.pedestrianEntranceNodes[tempInt].transform.position + new Vector3(0,1,0),cityGraph.pedestrianEntranceNodes[tempInt].transform.rotation).GetComponent<Walker>();
        tempWalk.currentNode = cityGraph.pedestrianEntranceNodes[tempInt];
        tempWalk.PickDestination();
    }
}
