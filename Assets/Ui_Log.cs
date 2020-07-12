using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Ui_Log : MonoBehaviour
{
    public TextMeshPro textpro;
    public Text largeText;

    void Update()
    {
        //string time = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy   HH:mm");
        string timeUS = System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yy   hh:mm tt");
        largeText.text = timeUS;
        textpro.text = timeUS;
    }
}
