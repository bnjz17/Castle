using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl : MonoBehaviour
{
    public GameObject ring;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ring.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        isActive = true;

        GameManager.instance.CheckWin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ring.GetComponent<Renderer>().material.color = new Color32(156, 236, 128, 255);
        isActive = false;
    }
}
