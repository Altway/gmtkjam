using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearZone : MonoBehaviour
{
    public float range;
    public CityNode emittingNode;
    public float duration;
    void Awake()
    {
        transform.localScale = new Vector3(range, range, range);
    }
    void Update(){
        duration -= Time.deltaTime;
        if(duration <= 0){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider col){
        if(col.transform.gameObject.tag == "Pedestrian"){
            col.transform.GetComponent<Walker>().state = WalkerState.Fear;
            col.transform.GetComponent<Walker>().fearRange = Mathf.RoundToInt(range);
            col.transform.GetComponent<Walker>().fearedNode = emittingNode;
        }
    }
}
