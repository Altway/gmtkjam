using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public CityGraph cityGraph; 
    public AgentsManager agentsManager;
    public CityNode testingDestination;    
    public CityNode currentNode;
    public WalkerState state;
    public Renderer myRend;
    public bool waiting;
    public float waitingCD;
    public float waitingTimer;
    public float rageCooldown;
    public float rageTimer;
    public float calmSpeed;
    public float fearSpeed;
    public float rageSpeed;
    public int fearRange;
    public CityNode fearedNode;
    public List<CityNode> path = new List<CityNode>();
    public bool calmPathPicked;
    public bool ragePathPicked;
    public bool fearPathPicked;
    public int index;
    public Sprite mood;
    public AgentMood myMood;
    public AgentGraphic jumpanim;
    public Vector3 oldPosition;
    public ParticleSystem blood;
    void Awake()
    {
        cityGraph = GameObject.Find("CityGraph").GetComponent<CityGraph>();
        agentsManager = GameObject.Find("GameManager").GetComponent<AgentsManager>();
        agentsManager.allWalker.Add(this);
        myRend = gameObject.GetComponentInChildren<Renderer>();
        blood = transform.GetComponentInChildren<ParticleSystem>();
    }

    public List<CityNode> FindPath(CityNode start, CityNode destination) {
        Queue<CityNode> frontier = new Queue<CityNode>();
        frontier.Enqueue(start);
        Dictionary<CityNode, CityNode> visited = new Dictionary<CityNode, CityNode>();
        int MAX = 1000;
        int control = 0;
        while(frontier.Count > 0 || control < MAX) {

            CityNode tmp = frontier.Dequeue();
            if (tmp == destination)
                break;

            foreach(CityNode node in tmp.neighbors) {
                if (!visited.ContainsKey(node)) {
                    frontier.Enqueue(node);
                    visited[node] = tmp;
                }
            }
            if (frontier.Count == 0)
                return path;
            control++;

        }
        CityNode path_temp = destination;
        path.Add(destination);
        while(true) {
            path_temp = visited[path_temp];
            path.Add(path_temp);
            if (visited[path_temp] == currentNode)
                break;
        }
        path.Add(currentNode);
        path.Reverse();
        return path;

    }
    public List<CityNode> FindPathCalm(CityNode start, CityNode destination) {
        Queue<CityNode> frontier = new Queue<CityNode>();
        Dictionary<CityNode, CityNode> visited = new Dictionary<CityNode, CityNode>();
        frontier.Enqueue(start);

        int MAX = 1000;
        int control = 0;
        while(frontier.Count > 0 || control < MAX) {

            CityNode tmp = frontier.Dequeue();
            if (tmp == destination)
                break;

            foreach(CityNode node in tmp.neighbors) {
                if (!visited.ContainsKey(node)) {
                    if(node.type == NodeType.PedestrianEntrance || node.type == NodeType.Pavement || node.type == NodeType.CarEntrance){
                        frontier.Enqueue(node);
                        visited[node] = tmp;
                    }
                }
            }
            if (frontier.Count == 0)
                return path;
            control++;
        }

/*         foreach (KeyValuePair<CityNode, CityNode> kvp in visited)
        {
            if (kvp.Key != null && kvp.Value != null)
                print("Key =" + kvp.Key.transform.position +"Value =" + kvp.Value.transform.position);
            else
                print(kvp.Key.transform.position);
        }
  */    CityNode path_temp = destination;
        path.Add(destination);
        while(true) {
            path_temp = visited[path_temp];
            path.Add(path_temp);
            if (visited[path_temp] == currentNode)
                break;
        }
        path.Add(currentNode);
        path.Reverse();

        return path;
    }

    public void MoveToNextNode(){
        if((new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z)).magnitude < 0.05f){
            if(state == WalkerState.Rage || state == WalkerState.Fear){
                index++;
                currentNode = path[index];
                gameObject.transform.position = new Vector3(path[index].transform.position.x, 0, path[index].transform.position.z);
            }
            else{
                if(path.Count - index > 2){
                    if(path[index+2].type == NodeType.Street)
                    {
                        waiting = true;
                    }
                    else{
                        waiting = false;
                        index++;
                        currentNode = path[index];
                        gameObject.transform.position = new Vector3(path[index].transform.position.x, 0, path[index].transform.position.z);
                    }
                }
                else{
                    waiting = false;
                    index++;
                    currentNode = path[index];
                    gameObject.transform.position = new Vector3(path[index].transform.position.x, 0, path[index].transform.position.z);
                }
            }
        }
        else{
            if(state == WalkerState.Calm){
                gameObject.transform.position += ( new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * calmSpeed * Time.deltaTime;
            }
            else if(state == WalkerState.Rage){
                gameObject.transform.position += ( new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * rageSpeed * Time.deltaTime;
            }
            else if(state == WalkerState.Fear){
                gameObject.transform.position += ( new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * fearSpeed * Time.deltaTime;
            }
        }
    }
    public void PickDestination(){
        int temp = Random.Range(0, cityGraph.pedestrianEntranceNodes.Count);
        if(cityGraph.pedestrianEntranceNodes[temp] != currentNode){
            testingDestination = cityGraph.pedestrianEntranceNodes[temp];
        }
        else{
            PickDestination();
        }
    }
    public void PickDestinationInFear(){
        Debug.Log(fearedNode);
        Vector3 fearDirection = (currentNode.transform.position - fearedNode.transform.position).normalized * fearRange;
        Vector3 fearedDestination = fearDirection + fearedNode.transform.position;
        float championMagnitude = (fearedDestination - fearedNode.transform.position).magnitude;
        CityNode champion = fearedNode;
        foreach(CityNode node in cityGraph.allNodes){
            if((node.transform.position - fearedDestination).magnitude <= championMagnitude){
                champion = node;
                championMagnitude = (node.transform.position - fearedDestination).magnitude;
            }
        }
        testingDestination = champion;
    }








    public List<CityNode> FindPathToClosestSafeSpot(CityNode start, CityNode destination) {
        Queue<CityNode> frontier = new Queue<CityNode>();
        Dictionary<CityNode, CityNode> visited = new Dictionary<CityNode, CityNode>();
        frontier.Enqueue(start);

        int MAX = 1000;
        int control = 0;
        while(frontier.Count > 0 || control < MAX) {

            CityNode tmp = frontier.Dequeue();
            if (tmp == destination){
                break;
            }
            foreach(CityNode node in tmp.neighbors) {
                if (!visited.ContainsKey(node)) {
                    frontier.Enqueue(node);
                    visited[node] = tmp;
                }
            }
            if (frontier.Count == 0)
                return path;
            control++;
        }
        CityNode path_temp = destination;
        //Debug.Log(destination);
        path.Add(destination);
        while(true) {
            path_temp = visited[path_temp];
            path.Add(path_temp);
            if (visited[path_temp] == currentNode)
                break;
        }
        path.Add(currentNode);
        path.Reverse();

        return path;
    }
}
