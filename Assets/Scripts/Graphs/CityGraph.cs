using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGraph : MonoBehaviour
{
    public List<CityNode> allNodes;
    public List<CityNode> pedestrianEntranceNodes = new List<CityNode>();
    public List<CityNode> carEntranceNodes = new List<CityNode>();
    void Start()
    {
        allNodes.AddRange(gameObject.GetComponentsInChildren<CityNode>());
        foreach(CityNode node in allNodes){
            if(node.type == NodeType.PedestrianEntrance){
                pedestrianEntranceNodes.Add(node);
            }
        }
        foreach(CityNode node in allNodes){
            if(node.type == NodeType.CarEntrance){
                carEntranceNodes.Add(node);
            }
        }
    }
}
