using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHub : MonoBehaviour
{

    private int points;
    [SerializeField] private Text pointsText;

    void Start()
    {
        pointsText.text = "" + points;
    }

    
    void Update()
    {
        pointsText.text = "" + points;
    }

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

}
