using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public CityGraph cityGraph; 
    public AgentsManager agentsManager;
    public CityNode testingDestination;    
    public CityNode currentNode;
    public CityNode next_node;
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
    public Vector3 oldPosition;
    void Awake()
    {
        cityGraph = GameObject.Find("CityGraph").GetComponent<CityGraph>();
        agentsManager = GameObject.Find("GameManager").GetComponent<AgentsManager>();
        agentsManager.allCars.Add(this);
        myRend = gameObject.GetComponent<Renderer>();
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
        /* foreach (KeyValuePair<CityNode, CityNode> kvp in visited)
        {
            if (kvp.Key != null && kvp.Value != null)
                print("Key =" + kvp.Key.transform.position +"Value =" + kvp.Value.transform.position);
            else
                print(kvp.Key.transform.position);
        }
 */     CityNode path_temp = destination;
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
            if (tmp == destination) {
                break;
            }

            foreach(CityNode node in tmp.neighbors) {
                if (!visited.ContainsKey(node)) {
                    if(node.type == NodeType.CarEntrance || node.type == NodeType.Street) {
                        frontier.Enqueue(node);
                        visited[node] = tmp;
                    }
                }
            }
            if (frontier.Count == 0) {
                return path;
            }
            control++;

        }
        /* foreach (KeyValuePair<CityNode, CityNode> kvp in visited)
        {
            if (kvp.Key != null && kvp.Value != null)
                print("Key =" + kvp.Key.transform.position +"Value =" + kvp.Value.transform.position);
            else
                print(kvp.Key.transform.position);
        } */
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

    public void PickNode(CityNode node) {
            PickNode(node);
    }

    public void MoveToNextNode(){
        bool hasArrived = ((new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(next_node.transform.position.x, 0, next_node.transform.position.z)).magnitude < 0.1f);
        if(state == CarState.Rage){
            if (hasArrived) {
                gameObject.transform.position += ( new Vector3(path[index+1].transform.position.x, 0, path[index+1].transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * rageSpeed * Time.deltaTime;
            } else {
                index++;
                currentNode = path[index];
                gameObject.transform.position = new Vector3(path[index].transform.position.x, 0, path[index].transform.position.z);
            }
        
        } else if (state == CarState.Calm) {
            if (hasArrived) {
                currentNode = next_node;
                next_node= currentNode.possible_neighbors[Random.Range(0, currentNode.possible_neighbors.Count)];
            } else {
                if (next_node.type == NodeType.CarEntrance || next_node.type == NodeType.Street) 
                    gameObject.transform.position += ( new Vector3(next_node.transform.position.x, 0, next_node.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * calmSpeed * Time.deltaTime;
            }

/*             if (next_node == null)
                hasArrived = true;
            if (hasArrived) {
                next_node = currentNode.possible_neighbors[Random.Range(0, currentNode.possible_neighbors.Count)];
                gameObject.transform.position = new Vector3(next_node.transform.position.x, 0, next_node.transform.position.z);
                
                print("CURRENT: " + currentNode.transform.position);
                print("NEXT NODE: " + next_node.transform.position);
            }
            gameObject.transform.position += ( new Vector3(next_node.transform.position.x, 0, next_node.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z) ).normalized * calmSpeed * Time.deltaTime;
  */       }
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
