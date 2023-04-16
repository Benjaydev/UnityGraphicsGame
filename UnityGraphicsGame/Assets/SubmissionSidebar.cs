using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubmissionSidebar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] idDisplays = new TextMeshProUGUI[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IdButtonPress(int id)
    {
        foreach (TextMeshProUGUI disp in idDisplays)
        {
            // Clear
            if(id == -1)
            {
                disp.text = "";
                continue;
            }

            // Text box is next empty
            if(disp.text == "")
            {
                disp.text = id.ToString();
                return;
            }
        }
    }

}
