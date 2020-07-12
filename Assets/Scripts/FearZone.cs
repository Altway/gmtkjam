using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearZone : MonoBehaviour
{
    public float range;
    public CityNode emittingNode;
    void Awake()
    {
        transform.localScale = new Vector3(range, range, range);
    }
    void OnTriggerStay(Collider col){
        if(col.transform.gameObject.tag == "Pedestrian"){
            col.transform.GetComponent<Walker>().state = WalkerState.Fear;
            col.transform.GetComponent<Walker>().fearRange = Mathf.RoundToInt(range);
            col.transform.GetComponent<Walker>().fearedNode = emittingNode;
        }
    }
}
