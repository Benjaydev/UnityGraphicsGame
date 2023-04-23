using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class KeycodeDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject keycodePrefab;

    // Start is called before the first frame update
    void Start()
    {
        string[] colourMarkups = new string[3] { "<color=red>", "<color=green>", "<color=blue>" };

        List<string> keys = Enumerable.ToList(CombinationIDGenerator.Instance.possibilities.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            GameObject newKeycode = Instantiate(keycodePrefab, transform);

            string text = "<b>";

            for(int j = 0; j < keys[i].Length; j++)
            {
                string c = keys[i][j].ToString();
                text += colourMarkups[int.Parse(c)-1];
                text += c;
                text += "</color>";
            }
            text+= "</b>: ";
            text += CombinationIDGenerator.Instance.possibilities[keys[i]];
            newKeycode.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
    }

}
