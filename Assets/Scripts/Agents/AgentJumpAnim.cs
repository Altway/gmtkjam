using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentJumpAnim : MonoBehaviour
{
    public List<AgentGraphic> agentsGraphics = new List<AgentGraphic>();

    void Update()
    {
        for(int i = 0; i<agentsGraphics.Count; i++){
            AgentGraphic currAgent = agentsGraphics[i];
            if(currAgent.sens == 1){
                currAgent.x += Time.deltaTime * currAgent.animSpeed;
                if(currAgent.x >= 1){
                    currAgent.sens = -1;
                }
            }
            else{
                currAgent.x -= Time.deltaTime * currAgent.animSpeed;
                if(currAgent.x <= 0.05){
                    currAgent.x=0.001f;
                    currAgent.sens = 1;
                }
            }
            currAgent.transform.position = new Vector3(currAgent.transform.position.x, Mathf.Sqrt(1 - Mathf.Pow(currAgent.x - 1, 2)) * 0.25f, currAgent.transform.position.z);
            currAgent.oldPos = new Vector2(currAgent.transform.position.x, currAgent.transform.position.z);
        }
    }        
}
