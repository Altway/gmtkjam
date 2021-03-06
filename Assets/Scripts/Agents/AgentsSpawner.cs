﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public GameObject carPrefab;
    public CityGraph cityGraph;
    public Transform walkersParent;
    public Transform carsParent;
    public float cooldown;
    public float timer;
    public float cooldownCar;
    public float timerCar;
    public int MAX=0;
    public bool started;
    void Start()
    {
        cityGraph = GameObject.Find("CityGraph").GetComponent<CityGraph>();
        carsParent = GameObject.Find("Cars").transform;
        walkersParent = GameObject.Find("Walkers").transform;
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
        timerCar += Time.deltaTime;
        if(timer >= cooldown){
            timer = 0;
            SpawnAgent();
        }
        if(timerCar >= cooldownCar){
            timerCar = 0;
            SpawnCar();
        }
    }
    void SpawnAgent(){
        int tempInt = Random.Range(0, cityGraph.pedestrianEntranceNodes.Count);
        Walker tempWalk = Instantiate(agentPrefab, cityGraph.pedestrianEntranceNodes[tempInt].transform.position,cityGraph.pedestrianEntranceNodes[tempInt].transform.rotation).GetComponent<Walker>();
        tempWalk.currentNode = cityGraph.pedestrianEntranceNodes[tempInt];
        tempWalk.PickDestination();
        tempWalk.transform.SetParent(walkersParent);
    }
    void SpawnCar(){
        int tempInt = Random.Range(0, cityGraph.carEntranceNodes.Count);
        if(cityGraph.carEntranceNodes[tempInt].currentCarOnNode == null){
            Car tempWalk = Instantiate(carPrefab, cityGraph.carEntranceNodes[tempInt].transform.position,cityGraph.carEntranceNodes[tempInt].transform.rotation).GetComponent<Car>();
            tempWalk.currentNode = cityGraph.carEntranceNodes[tempInt];
            cityGraph.carEntranceNodes[tempInt].currentCarOnNode = tempWalk;
            tempWalk.next_node = tempWalk.currentNode.possible_neighbors[Random.Range(0, tempWalk.currentNode.possible_neighbors.Count)];
            tempWalk.PickDestination();
            tempWalk.transform.SetParent(carsParent);
        }
        else{
            timerCar = cooldownCar;
        }
    }
}
