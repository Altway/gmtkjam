using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{

    public CityGraph cityGraph;    
    public CityNode testingDestination;    
    public CityNode currentNode;

    void Start()
    {
        cityGraph = GameObject.Find("CityGraph").GetComponent<CityGraph>();
    }

    public Dictionary<CityNode, CityNode> FindPath(CityNode start, CityNode destination) {
        List<CityNode> path = new List<CityNode>();
        Queue<CityNode> frontier = new Queue<CityNode>();
        frontier.Enqueue(start);
        Dictionary<CityNode, CityNode> visited = new Dictionary<CityNode, CityNode>();
        visited[start] = null;

        int MAX = 1000;
        int control = 0;
        while(frontier.Count > 0 || control < MAX) {

            CityNode tmp = frontier.Dequeue();
            if (tmp == destination) {
                Debug.Log("Found destination");
                break;
            }

            foreach(CityNode node in tmp.neighbors) {
                if (!visited.ContainsKey(node)) {
                    frontier.Enqueue(node);
                    visited[node] = tmp;
                }
            }
            control++;

        }

        return visited;

    }

    public void WalkTo(CityNode destination) {
        StartCoroutine(move(destination));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M)) {
            foreach(CityNode node in cityGraph.allNodes)
                Debug.Log(node.transform.position);
        }
        if (Input.GetKeyUp(KeyCode.P)) 
            WalkTo(testingDestination);
    }

    IEnumerator move(CityNode destination)
    {
        Dictionary<CityNode, CityNode> path_dict = FindPath(currentNode, destination);
        List<CityNode> path =new List<CityNode>();

        CityNode path_temp = destination;
        path.Add(destination);
        while(true) {
            path_temp = path_dict[path_temp];
            path.Add(path_temp);
            if (path_dict[path_temp] == currentNode)
                break;
        }
        path.Add(currentNode);
        path.Reverse();

        /*foreach (KeyValuePair<CityNode, CityNode> kvp in path_dict)
        {
            if (kvp.Key != null && kvp.Value != null)
            print("Key =" + kvp.Key.transform.position +"Value =" + kvp.Value.transform.position);
        }

        foreach (CityNode node in path) {
            if (node != null)
                print("LIST_NODE: " + node.transform.position);
        }
        */

        foreach(CityNode node in path) {
            gameObject.transform.position = new Vector3(node.transform.position.x, 1, node.transform.position.z);
            currentNode = node;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
