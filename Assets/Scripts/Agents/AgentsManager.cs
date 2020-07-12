using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkerState{Calm, Fear, Rage}
public enum CarState{Calm, Rage}
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
        }

        for(int i = 0; i<allCars.Count; i++){
            if(allCars[i].state == CarState.Calm){
                allCars[i].myRend.material.color = Color.white;
                if(allCars[i].waiting){
                    allCars[i].waitingTimer+= Time.deltaTime;
                    if(allCars[i].waitingTimer>= allCars[i].waitingCD){
                        allCars[i].state = CarState.Rage;
                    }
                }else{
                    if(allCars[i].waitingTimer >= 0){
                        allCars[i].waitingTimer -= Time.deltaTime;
                    }
                }
                if(!allCars[i].calmPathPicked){
                    allCars[i].path = allCars[i].FindPathCalm(allCars[i].currentNode, allCars[i].testingDestination);
                    if (allCars[i].path.Count != 0) {
                        allCars[i].index = 0;
                        allCars[i].calmPathPicked = true;
                        allCars[i].ragePathPicked = false;
                    }
                }else{
                    if(allCars[i].currentNode != allCars[i].testingDestination){
                        if (allCars[i].path.Count != 0) {
                            allCars[i].MoveToNextNode();
                        }
                    }
                    else{
                        Destroy(allCars[i].gameObject);
                        allCars.Remove(allCars[i]);
                        continue;
                    }
                }
            }
            else if(allCars[i].state == CarState.Rage){
                allCars[i].myRend.material.color = Color.red;
                allCars[i].waiting = false;
                allCars[i].waitingTimer = 0;
                allCars[i].rageTimer += Time.deltaTime;
                if(allCars[i].rageTimer >= allCars[i].rageCooldown){
                    if(allCars[i].currentNode.type == NodeType.Pavement || allCars[i].currentNode.type == NodeType.PedestrianEntrance || allCars[i].currentNode.type == NodeType.CarEntrance){
                        allCars[i].rageTimer = 0;
                        allCars[i].state = CarState.Calm;
                    }
                }
                if(!allCars[i].ragePathPicked){
                    allCars[i].path = allCars[i].FindPath(allCars[i].currentNode, allCars[i].testingDestination);
                    allCars[i].index = 0;
                    allCars[i].calmPathPicked = false;
                    allCars[i].ragePathPicked = true;
                }else{
                    if(allCars[i].currentNode != allCars[i].testingDestination){
                        allCars[i].MoveToNextNode();
                    }
                    else{
                        Destroy(allCars[i].gameObject);
                        allCars.Remove(allCars[i]);
                        continue;
                    }
                }
            }
        }
    }
}