/* Code by Logan Nyquist */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    private List<string> lines;
    public bool endsGame = false;

    public List<string> Lines
    {
        get { return lines; }
        set { lines = value; } 
    }
}
