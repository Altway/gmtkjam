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
                if(allWalkers[i].path.Count == 0){
                    allWalkers[i].path = allWalkers[i].FindPath(allWalkers[i].currentNode, allWalkers[i].testingDestination);
                    allWalkers[i].index = 0;
                }else{
                    if(allWalkers[i].currentNode != allWalkers[i].testingDestination)
                        allWalkers[i].MoveToNextNode();
                }
            }
            else if(allWalkers[i].state == WalkerState.Fear){
                
            }
            else if(allWalkers[i].state == WalkerState.Rage){
                
            }
        }
    }
}
