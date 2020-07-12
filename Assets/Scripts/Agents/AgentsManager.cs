using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkerState{Calm, Fear, Rage, Death}
public enum CarState{Calm, Rage, Broken}
public class AgentsManager : MonoBehaviour
{
    public List<Walker> allWalker = new List<Walker>();
    public List<Car> allCars = new List<Car>();
    public bool started;

    public float timer;
    public float cooldown;
    void Start()
    {
        
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
        if (timer <= cooldown) 
            return;
        else
            timer = 0;
        for(int i = 0; i<allWalker.Count; i++){
            Walker currentWalker = allWalker[i];
            
            if(currentWalker.state != WalkerState.Death && currentWalker.path.Count-1 > currentWalker.index){//If I'm not dead, orient my body based on my last position
                Vector3 dir = currentWalker.path[currentWalker.index].transform.position - currentWalker.path[currentWalker.index+1].transform.position;
                currentWalker.transform.LookAt(dir + currentWalker.transform.position);
            }
            // I am calm
            if(currentWalker.state == WalkerState.Calm){
                currentWalker.myRend.material.color = Color.white;
                // I am waiting
                if(currentWalker.waiting){
                    currentWalker.waitingTimer+= Time.deltaTime;
                    // I am upset
                    if(currentWalker.waitingTimer>= currentWalker.waitingCD)
                        currentWalker.state = WalkerState.Rage;
                }else{
                    // I move so I cool-off
                    currentWalker.waitingTimer -= Mathf.Min(0, Time.deltaTime);
                }
                if(currentWalker.calmPathPicked){
                     // I Already have a path
                    if(currentWalker.currentNode == currentWalker.testingDestination) {
                        // Reached my destination
                        Destroy(currentWalker.gameObject);
                        allWalker.Remove(currentWalker);
                        continue;
                    } else
                        // Next move
                        currentWalker.MoveToNextNode();
                }else{
                    currentWalker.PickDestination();
                    // I look for a path
                    currentWalker.path = currentWalker.FindPathCalm(currentWalker.currentNode, currentWalker.testingDestination);
                    if (currentWalker.path.Count != 0) {
                        // I Found a path
                        currentWalker.index = 0;
                        currentWalker.calmPathPicked = true;
                        currentWalker.fearPathPicked = false;
                        currentWalker.ragePathPicked = false;
                    }
                }
            }
            else if(currentWalker.state == WalkerState.Fear){
                currentWalker.myRend.material.color = Color.blue;
                if(currentWalker.fearPathPicked){
                    if(currentWalker.currentNode == currentWalker.testingDestination){
                        currentWalker.state = WalkerState.Calm;
                    }
                    else{
                        currentWalker.MoveToNextNode();
                    }
                }else{
                    currentWalker.PickDestinationInFear();
                    currentWalker.path = currentWalker.FindPathToClosestSafeSpot(currentWalker.currentNode, currentWalker.testingDestination);
                    if(currentWalker.path.Count != 0){
                        currentWalker.index = 0;
                        currentWalker.calmPathPicked = false;
                        currentWalker.fearPathPicked = true;
                        currentWalker.ragePathPicked = false;
                    }
                }
                currentWalker.waiting = false;
                currentWalker.waitingTimer = 0;
            }
            else if(currentWalker.state == WalkerState.Rage){
                currentWalker.myRend.material.color = Color.red;
                currentWalker.waiting = false;
                currentWalker.waitingTimer = 0;
                currentWalker.rageTimer += Time.deltaTime;
                if(currentWalker.rageTimer >= currentWalker.rageCooldown){
                    if(currentWalker.currentNode.type == NodeType.Pavement || currentWalker.currentNode.type == NodeType.PedestrianEntrance || currentWalker.currentNode.type == NodeType.CarEntrance){
                        currentWalker.rageTimer = 0;
                        currentWalker.state = WalkerState.Calm;
                    }
                }
                if(!currentWalker.ragePathPicked){
                    currentWalker.path = currentWalker.FindPath(currentWalker.currentNode, currentWalker.testingDestination);
                    currentWalker.index = 0;
                    currentWalker.calmPathPicked = false;
                    currentWalker.fearPathPicked = false;
                    currentWalker.ragePathPicked = true;
                }else{
                    if(currentWalker.currentNode != currentWalker.testingDestination){
                        currentWalker.MoveToNextNode();
                    }
                    else{
                        Destroy(currentWalker.gameObject);
                        allWalker.Remove(currentWalker);
                        continue;
                    }
                }
            }
            else if(currentWalker.state == WalkerState.Death){
                currentWalker.myRend.material.color = Color.black;
                if(currentWalker.jumpanim != null)
                {
                    Destroy(currentWalker.jumpanim);
                }
                currentWalker.transform.eulerAngles = new Vector3(-90, 0,0);
            }
        }

        for(int i = 0; i<allCars.Count; i++){
            Car currentCar = allCars[i];
            if(currentCar.state == CarState.Calm){
                currentCar.myRend.material.color = Color.white;
                if(currentCar.waiting){
                    currentCar.waitingTimer+= Time.deltaTime;
                    if(currentCar.waitingTimer>= currentCar.waitingCD)
                        currentCar.state = CarState.Rage;
                }else{
                    if(currentCar.waitingTimer >= 0)
                        currentCar.waitingTimer -= Time.deltaTime;
                }
/*                 if(!currentCar.calmPathPicked)
                    currentCar.path = allCars[i].FindPathCalm(currentCar.currentNode, currentCar.testingDestination); */
                if (currentCar.state == CarState.Calm && currentCar.currentNode != currentCar.testingDestination) {
                    currentCar.MoveToNextNode();
                        /*currentCar.index = 0;
                        currentCar.calmPathPicked = true;
                        currentCar.ragePathPicked = false;*/
                }else{
                    Destroy(currentCar.gameObject);
                    allCars.Remove(currentCar);
                    continue;
                }
            }
            
            else if(currentCar.state == CarState.Rage){
                currentCar.myRend.material.color = Color.red;
                currentCar.waiting = false;
                currentCar.waitingTimer = 0;
                currentCar.rageTimer += Time.deltaTime;
                if(currentCar.rageTimer >= currentCar.rageCooldown){
                    if(currentCar.currentNode.type == NodeType.Pavement || currentCar.currentNode.type == NodeType.PedestrianEntrance || currentCar.currentNode.type == NodeType.CarEntrance){
                        currentCar.rageTimer = 0;
                        currentCar.state = CarState.Calm;
                    }
                }
                if(!currentCar.ragePathPicked){
                    currentCar.path = currentCar.FindPath(currentCar.currentNode, currentCar.testingDestination);
                    currentCar.index = 0;
                    currentCar.calmPathPicked = false;
                    currentCar.ragePathPicked = true;
                }else{
                    if(currentCar.currentNode != currentCar.testingDestination){
                        currentCar.MoveToNextNode();
                    }
                    else{
                        Destroy(currentCar.gameObject);
                        allCars.Remove(currentCar);
                        continue;
                    }
                }
            }
            else if(allCars[i].state == CarState.Broken){
                allCars[i].myRend.material.color = Color.black;
            }
            //Debug.Log("J'y arrive");
            allCars[i].oldPosition = allCars[i].transform.position;
        }
    }
}