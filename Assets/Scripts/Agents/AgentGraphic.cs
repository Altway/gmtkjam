using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGraphic : MonoBehaviour
{
    public int sens;
    public float x;
    public float animSpeed;
    public Vector2 oldPos;
    void Start()
    {
        transform.position += new Vector3(Random.Range(-0.25f, 0.25f),0, Random.Range(-0.25f, 0.25f));
        GameObject.Find("GameManager").GetComponent<AgentJumpAnim>().agentsGraphics.Add(this);
        sens = 1;
        transform.parent.GetComponent<Walker>().jumpanim = this;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        GameObject.Find("GameManager").GetComponent<AgentJumpAnim>().agentsGraphics.Remove(this);
    }
}
