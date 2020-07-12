using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMoodManager : MonoBehaviour
{
    public List<AgentMood> agentMoods = new List<AgentMood>();
    public List<CarMood> carMoods = new List<CarMood>();
    public Transform camPos;
    public Vector3 offset;

    void Start(){
        camPos = GameObject.Find("Main Camera").transform;
    }
    void Update()
    {
        foreach(AgentMood mood in agentMoods){
            mood.transform.rotation = Quaternion.Euler(-72, 180,0);
            if(mood.myWalker.state == WalkerState.Calm){
                if(mood.myWalker.waiting){
                    if(mood.myWalker.waitingTimer / mood.myWalker.waitingCD < 0.25f )
                        mood.mySpriteRend.sprite = mood.waitingStates[0];
                    if(mood.myWalker.waitingTimer / mood.myWalker.waitingCD < 0.5f && mood.myWalker.waitingTimer / mood.myWalker.waitingCD >= 0.25f)
                        mood.mySpriteRend.sprite = mood.waitingStates[1];
                    if(mood.myWalker.waitingTimer / mood.myWalker.waitingCD < 0.75f && mood.myWalker.waitingTimer / mood.myWalker.waitingCD >= 0.5f)
                        mood.mySpriteRend.sprite = mood.waitingStates[2];
                    if(mood.myWalker.waitingTimer / mood.myWalker.waitingCD < 1f && mood.myWalker.waitingTimer / mood.myWalker.waitingCD >= 0.75f)
                        mood.mySpriteRend.sprite = mood.waitingStates[3];
                }
                else{
                    mood.mySpriteRend.sprite = mood.calmStates[mood.calmStateIndex];
                }
            }
            if(mood.myWalker.state == WalkerState.Rage){
                mood.mySpriteRend.sprite = mood.rageState;
            }
            if(mood.myWalker.state == WalkerState.Fear){
                mood.mySpriteRend.sprite = mood.fearState;
            }
            if(mood.myWalker.state == WalkerState.Death){
                mood.mySpriteRend.sprite = mood.deadState;
            }
        }
        foreach(CarMood mood in carMoods){
            mood.transform.rotation = Quaternion.Euler(-72, 180,0);
            if(mood.myCar.state == CarState.Calm){
                if(mood.myCar.waiting){
                    if(mood.myCar.waitingTimer / mood.myCar.waitingCD < 0.25f )
                        mood.mySpriteRend.sprite = mood.waitingStates[0];
                    if(mood.myCar.waitingTimer / mood.myCar.waitingCD < 0.5f && mood.myCar.waitingTimer / mood.myCar.waitingCD >= 0.25f)
                        mood.mySpriteRend.sprite = mood.waitingStates[1];
                    if(mood.myCar.waitingTimer / mood.myCar.waitingCD < 0.75f && mood.myCar.waitingTimer / mood.myCar.waitingCD >= 0.5f)
                        mood.mySpriteRend.sprite = mood.waitingStates[2];
                    if(mood.myCar.waitingTimer / mood.myCar.waitingCD < 1f && mood.myCar.waitingTimer / mood.myCar.waitingCD >= 0.75f)
                        mood.mySpriteRend.sprite = mood.waitingStates[3];
                }
                else{
                    mood.mySpriteRend.sprite = null;
                }
            }
            if(mood.myCar.state == CarState.Rage){
                mood.mySpriteRend.sprite = mood.rageState;
            }
            if(mood.myCar.state == CarState.Broken){
                mood.mySpriteRend.sprite = mood.deadState;
            }
        }
    }
}
