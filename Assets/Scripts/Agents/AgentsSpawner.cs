using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public GameObject carPrefab;
    public CityGraph cityGraph;
    public float cooldown;
    public float timer;
    public int MAX=0;
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
            //SpawnCar();
        }
    }
    void SpawnAgent(){
        int tempInt = Random.Range(0, cityGraph.pedestrianEntranceNodes.Count);
        Walker tempWalk = Instantiate(agentPrefab, cityGraph.pedestrianEntranceNodes[tempInt].transform.position,cityGraph.pedestrianEntranceNodes[tempInt].transform.rotation).GetComponent<Walker>();
        tempWalk.currentNode = cityGraph.pedestrianEntranceNodes[tempInt];
        tempWalk.PickDestination();
    }
    void SpawnCar(){
        int tempInt = Random.Range(0, cityGraph.carEntranceNodes.Count);
        Car tempWalk = Instantiate(carPrefab, cityGraph.carEntranceNodes[tempInt].transform.position,cityGraph.carEntranceNodes[tempInt].transform.rotation).GetComponent<Car>();
        tempWalk.currentNode = cityGraph.carEntranceNodes[tempInt];
        tempWalk.PickDestination();
    }
}
