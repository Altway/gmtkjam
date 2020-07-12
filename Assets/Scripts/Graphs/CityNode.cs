using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType{Street, Pavement,PedestrianEntrance,CarEntrance, Building, CarExit}
public class CityNode : MonoBehaviour
{
    public NodeType type;
    public List<CityNode> neighbors = new List<CityNode>();
    public List<CityNode> possible_neighbors = new List<CityNode>();
    public Dictionary<Vector3, CityNode> orientedNeighbors = new Dictionary<Vector3, CityNode>();
    public LayerMask layers;

    public bool Right;
    public bool Left;

    public bool Top;

    public bool Bot;

    void Start()
    {
        FetchNeighbors();
        if (Right)
            possible_neighbors.Add(neighbors[0]);
        if (Left)
            possible_neighbors.Add(neighbors[1]);
        if (Top)
            possible_neighbors.Add(neighbors[2]);
        if (Bot)
            possible_neighbors.Add(neighbors[3]);
        //ChangeColor();
    }
    void FetchNeighbors(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(1, 0, 0), out hit, 1, layers)){
            if(!neighbors.Contains(hit.transform.gameObject.GetComponent<CityNode>()))
                neighbors.Add(hit.transform.gameObject.GetComponent<CityNode>());
                orientedNeighbors.Add(new Vector3(1,0,0),hit.transform.gameObject.GetComponent<CityNode>());
        }
        if(Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, 1, layers)){
            if(!neighbors.Contains(hit.transform.gameObject.GetComponent<CityNode>()))
                neighbors.Add(hit.transform.gameObject.GetComponent<CityNode>());
                orientedNeighbors.Add(new Vector3(-1,0,0),hit.transform.gameObject.GetComponent<CityNode>());
        }
        if(Physics.Raycast(transform.position, new Vector3(0, 0, 1), out hit, 1, layers)){
            if(!neighbors.Contains(hit.transform.gameObject.GetComponent<CityNode>()))
                neighbors.Add(hit.transform.gameObject.GetComponent<CityNode>());
                orientedNeighbors.Add(new Vector3(0,0,1),hit.transform.gameObject.GetComponent<CityNode>());
        }
        if(Physics.Raycast(transform.position, new Vector3(0, 0, -1), out hit, 1, layers)){
            if(!neighbors.Contains(hit.transform.gameObject.GetComponent<CityNode>()))
                neighbors.Add(hit.transform.gameObject.GetComponent<CityNode>());
                orientedNeighbors.Add(new Vector3(0,0,-1),hit.transform.gameObject.GetComponent<CityNode>());
        }
    }
    public void ChangeColor(){
        if(type == NodeType.Street){
            //gameObject.GetComponent<Renderer>().material.color = new Color(0.1f,0.1f,0.1f, 1);
        }
        else if(type == NodeType.Pavement){
            //gameObject.GetComponent<Renderer>().material.color = new Color(0.8f,0.8f,0.8f, 1);
        }
        else if(type == NodeType.PedestrianEntrance){
            //gameObject.GetComponent<Renderer>().material.color = new Color(0.2f,0.2f,0.2f, 1);
        }
        else if(type == NodeType.CarEntrance){
            //gameObject.GetComponent<Renderer>().material.color = new Color(0.4f,0.4f,0.4f, 1);
        }
        else if(type == NodeType.Building){
            //gameObject.GetComponent<Renderer>().material.color = new Color(1f,0f,0f, 1);
        }
    }
}
