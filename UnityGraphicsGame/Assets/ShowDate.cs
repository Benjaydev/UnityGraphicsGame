using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDate : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string date = System.DateTime.Now.ToString("dd/MM/1989  hh:mm:ss.fff");

        text.text = date;
    }
}
