using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int spiral_speed = 1, tool_speed = 1;
    public float spiral_thickness = 0.92f;
    public GameObject spiral;


    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
