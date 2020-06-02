using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChiselController : MonoBehaviour
{
    private Spiral spiral;
    private Rigidbody2D rb;
    private CircleCollider2D coll;
    private Camera cam;
    [SerializeField]
    private Text score_text;
    private bool pause = true, isCarving = false, canCarve = false;
    private GameObject carvedSpiral;
    private Spiral spiralScript;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wood")
        {
            canCarve = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wood")
        {
            canCarve = false;
            isCarving = false;
            carvedSpiral = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            pause = false;
            //Create spiral
            if (canCarve && isCarving == false)
            {
                carvedSpiral = GameObject.Instantiate(GameManager.instance.spiral);
                spiralScript = carvedSpiral.GetComponent<Spiral>();
                carvedSpiral.transform.position = new Vector3(transform.position.x + 8, transform.position.y, -spiralScript.height / 2);
                isCarving = true;
            }
            //Spiral update 
            if (carvedSpiral)
            {
                carvedSpiral.transform.position = new Vector3(transform.position.x + 8 + spiralScript.length, carvedSpiral.transform.position.y, -spiralScript.height / 2);
                spiralScript.length += GameManager.instance.spiral_speed * 0.01f;
                score += spiralScript.length * 100 * Time.fixedDeltaTime;
                spiralScript.width = GameManager.instance.spiral_thickness;
                spiralScript.height = Mathf.Clamp(spiralScript.height + 0.02f, 1, 3.08f);
                spiralScript.Refresh();
                spiralScript.GetComponent<CircleCollider2D>().radius = spiralScript.radius - 0.5f;
            }
            //Chisel movement
            transform.position = new Vector3(transform.position.x, 2.43f, transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -17);
        }
        else
        {
            //Spiral impulse
            if (isCarving == true)
            {
                float force = GameManager.instance.tool_speed * 20;
                carvedSpiral.GetComponent<Rigidbody2D>().AddForce(new Vector2(force, 20), ForceMode2D.Impulse);
                isCarving = false;
            }
            //Chisel movement
            transform.position = new Vector3(transform.position.x, 4.4f, transform.position.z);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            carvedSpiral = null;
            canCarve = false;
        }
        cam.transform.position = new Vector3(transform.position.x - 27, cam.transform.position.y, cam.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (!pause)
        {
            float speed = 10 * GameManager.instance.tool_speed * Time.fixedDeltaTime;
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            score_text.text = "Score : " + Mathf.Floor(score);
        }
    }
}
