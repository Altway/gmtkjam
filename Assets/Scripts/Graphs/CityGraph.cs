using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGraph : MonoBehaviour
{
    public List<CityNode> allNodes;
    void Start()
    {
        allNodes.AddRange(gameObject.GetComponentsInChildren<CityNode>());
    }
}
