using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CityGraph cityGraph; 
    public AgentsManager agentsManager;
    public CityNode testingDestination;    
    public CityNode currentNode;
    public CarState state;
    public Renderer myRend;
    public bool waiting;
    public float waitingCD;
    public float waitingTimer;
    public float rageCooldown;
    public float rageTimer;
    public float calmSpeed;
    public float rageSpeed;
    public List<CityNode> path = new List<CityNode>();
    public bool calmPathPicked;
    public bool ragePathPicked;
    public int index;
    void Awake()
    {
        cityGraph = GameObject.Find("CityGraph").GetComponent<CityGraph>();
        agentsManager = GameObject.Find("GameManager").GetComponent<AgentsManager>();
        agentsManager.allCars.Add(this);
        myRend = gameObject.GetComponent<Renderer>();
    }

    public List<CityNode> FindPath(CityNode start, CityNode destination) {
        path.Clear();
        Queue<CityNode> frontier = new Queue<CityNode>();
        frontier.Enqueue(start);
        Dictionary<CityNode, CityNode> visited = new Dictionary<CityNode, CityNode>();
        visited[start] = null;
        int MAX = 1000;
        int control = 0;
        while(frontier.Count > 0 || control < MAX) {

            if (frontier.Count == 0){
                break;
            }
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
        if (frontier.Count != 0) {
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
        }
        return path;

    }
    public List<CityNode> FindPathCalm(CityNode start, CityNode destination) {
        path.Clear();
        Queue<CityNode> frontier = new Queue<CityNode>();
        frontier.Enqueue(start);
        Dictionary<CityNode, CityNode> visited = new Dictionary<CityNode, CityNode>();
        visited[start] = null;

        int MAX = 1000;
        int control = 0;
        while(frontier.Count > 0 || control < MAX) {

            if (frontier.Count == 0){
                break;
            }
            CityNode tmp = frontier.Dequeue();
            if (tmp == destination) {
                break;
            }

            foreach(CityNode node in tmp.neighbors) {
                if (!visited.ContainsKey(node)) {
                    if(node.type == NodeType.CarEntrance || node.type == NodeType.Street || node.type == NodeType.CarEntrance){
                        frontier.Enqueue(node);
                        visited[node] = tmp;
                    }
                }
            }
            control++;

        }
        if (frontier.Count != 0) {
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
        }
        return path;

    }

    /*public void WalkTo(CityNode destination) {
        StartCoroutine(move(destination));
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M)) {
            foreach(CityNode node in cityGraph.allNodes)
                Debug.Log(node.transform.position);
        }
        /*if (Input.GetKeyUp(KeyCode.P)) 
            WalkTo(testingDestination);
        */
    }

    /*IEnumerator move(CityNode destination)
    {
        Dictionary<CityNode, CityNode> path_dict = FindPath(currentNode, destination);

        /*foreach (KeyValuePair<CityNode, CityNode> kvp in path_dict)
        {
            if (kvp.Key != null && kvp.Value != null)
            print("Key =" + kvp.Key.transform.position +"Value =" + kvp.Value.transform.position);
        }

        foreach (CityNode node in path) {
            if (node != null)
                print("LIST_NODE: " + node.transform.position);
        }
        

        foreach(CityNode node in path) {
            gameObject.transform.position = new Vector3(node.transform.position.x, 1, node.transform.position.z);
            currentNode = node;
            yield return new WaitForSeconds(0.1f);
        }
    }*/

    public void MoveToNextNode(){
        if((new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z)).magnitude < 0.1f){
            if(state == CarState.Rage){
                index++;
                currentNode = path[index];
                gameObject.transform.position = new Vector3(path[index].transform.position.x, 1, path[index].transform.position.z);
            }
            else{
                if(path.Count - index > 2){
                    if(path[index+2].type == NodeType.Pavement||path[index+2].type == NodeType.PedestrianEntrance)
                    {
                        waiting = true;
                    }
                    else{
                        waiting = false;
                        index++;
                        currentNode = path[index];
                        gameObject.transform.position = new Vector3(path[index].transform.position.x, 1, path[index].transform.position.z);
                    }
                }
                else{
                    waiting = false;
                    index++;
                    currentNode = path[index];
                    gameObject.transform.position = new Vector3(path[index].transform.position.x, 1, path[index].transform.position.z);
                }
            }
        }
        else{
            if(state == CarState.Calm){
                gameObject.transform.position += ( new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * calmSpeed * Time.deltaTime;
            }
            else if(state == CarState.Rage){
                gameObject.transform.position += ( new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * rageSpeed * Time.deltaTime;
            }
        }
    }
    public void PickDestination(){
        int temp = Random.Range(0, cityGraph.carEntranceNodes.Count);
        if(cityGraph.carEntranceNodes[temp] != currentNode){
            testingDestination = cityGraph.carEntranceNodes[temp];
        }
        else{
            PickDestination();
        }
    }
}
