using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombinationIDGenerator : MonoBehaviour
{
    public static CombinationIDGenerator Instance;

    public Dictionary<string, string> possibilities = new Dictionary<string, string>()
    {
        { "111",""},
        { "112",""},
        { "121",""},
        { "211",""},
        { "113",""},
        { "131",""},
        { "311",""},
        { "122",""},
        { "212",""},
        { "221",""},
        { "123",""},
        { "132",""},
        { "213",""},
        { "231",""},
        { "312",""},
        { "321",""},
        { "133",""},
        { "313",""},
        { "331",""},
        { "222",""},
        { "223",""},
        { "232",""},
        { "322",""},
        { "233",""},
        { "323",""},
        { "332",""},
        { "333",""},
    };

    public List<string> randomWords = new List<string>()
    {
        "bat",
        "map",
        "sin",
        "end",
        "bow",
        "cow",
        "spy",
        "pot",
        "lay",
        "hit",
        "tap",
        "use",
        "bay",
        "bet",
        "hot",
        "lid",
        "jet",
        "mud",
        "tin",
        "bus",
        "gap",
        "pan",
        "war",
        "jaw",
        "tax",
        "win",
        "leg",
        "gun",
        "fit",
        "pig",
        "ask",
        "nut",
        "bed",
        "oak",
        "far",
        "law",
        "bin",
        "hay",
        "fun",
        "pop",
        "pay",
        "fog",
        "air",
        "egg",
        "man",
        "get",
        "cut",
        "eat",
        "art",
        "owe"
    };

    private void Awake()
    {
        Instance = this;
        GeneratePossibilities();
    }

    public string GetWord(string[] id)
    {
        string comb = string.Join("", id);
        return possibilities[comb];
    }

    public void GeneratePossibilities()
    {
        List<string> randomWordsCopy = new List<string>(randomWords);
        // Fill possibilities with random words
        for (int i = 0; i < possibilities.Keys.Count; i++)
        {           
            int randomIndex = Random.Range(0, randomWordsCopy.Count);
            possibilities[possibilities.ElementAt(i).Key] = randomWordsCopy[randomIndex];
            randomWordsCopy.RemoveAt(randomIndex);
            
        }
    }
}
