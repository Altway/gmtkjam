using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinLose : MonoBehaviour
{
    public GameObject loseObject;
    public int deathCount;
    public Text deathCountText;
    void Start()
    {
        loseObject = GameObject.Find("Lose");
    }
    void Update()
    {
        if(deathCount>50){
            loseObject.SetActive(true);
        }
        else{
            loseObject.SetActive(false);
        }
        deathCount.text = deathCountText.ToString();
    }
}
