using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WalkerState{Calm, Fear, Rage}
public class AgentsManager : MonoBehaviour
{
    public List<Walker> allWalkers = new List<Walker>();
    public bool started;
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

        for(int i = 0; i<allWalkers.Count; i++){
            if(allWalkers[i].state == WalkerState.Calm){
                allWalkers[i].myRend.material.color = Color.white;
                if(allWalkers[i].waiting){
                    allWalkers[i].waitingTimer+= Time.deltaTime;
                    if(allWalkers[i].waitingTimer>= allWalkers[i].waitingCD){
                        allWalkers[i].state = WalkerState.Rage;
                    }
                }else{
                    if(allWalkers[i].waitingTimer >= 0){
                        allWalkers[i].waitingTimer -= Time.deltaTime;
                    }
                }
                if(!allWalkers[i].calmPathPicked){
                    allWalkers[i].path = allWalkers[i].FindPathCalm(allWalkers[i].currentNode, allWalkers[i].testingDestination);
                    allWalkers[i].index = 0;
                    allWalkers[i].calmPathPicked = true;
                    allWalkers[i].fearPathPicked = false;
                    allWalkers[i].ragePathPicked = false;
                }else{
                    if(allWalkers[i].currentNode != allWalkers[i].testingDestination){
                        allWalkers[i].MoveToNextNode();
                    }
                    else{
                        Destroy(allWalkers[i].gameObject);
                        allWalkers.Remove(allWalkers[i]);
                        continue;
                    }
                }
            }
            else if(allWalkers[i].state == WalkerState.Fear){
                allWalkers[i].myRend.material.color = Color.blue;
                allWalkers[i].waiting = false;
                allWalkers[i].waitingTimer = 0;
            }
            else if(allWalkers[i].state == WalkerState.Rage){
                allWalkers[i].myRend.material.color = Color.red;
                allWalkers[i].waiting = false;
                allWalkers[i].waitingTimer = 0;
                allWalkers[i].rageTimer += Time.deltaTime;
                if(allWalkers[i].rageTimer >= allWalkers[i].rageCooldown){
                    if(allWalkers[i].currentNode.type == NodeType.Pavement || allWalkers[i].currentNode.type == NodeType.PedestrianEntrance || allWalkers[i].currentNode.type == NodeType.CarEntrance){
                        allWalkers[i].rageTimer = 0;
                        allWalkers[i].state = WalkerState.Calm;
                    }
                }
                if(!allWalkers[i].ragePathPicked){
                    allWalkers[i].path = allWalkers[i].FindPath(allWalkers[i].currentNode, allWalkers[i].testingDestination);
                    allWalkers[i].index = 0;
                    allWalkers[i].calmPathPicked = false;
                    allWalkers[i].fearPathPicked = false;
                    allWalkers[i].ragePathPicked = true;
                }else{
                    if(allWalkers[i].currentNode != allWalkers[i].testingDestination){
                        allWalkers[i].MoveToNextNode();
                    }
                    else{
                        Destroy(allWalkers[i].gameObject);
                        allWalkers.Remove(allWalkers[i]);
                        continue;
                    }
                }
            }
        }
    }
}
