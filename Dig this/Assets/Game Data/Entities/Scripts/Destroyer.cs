using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    List<GameObject> items = new List<GameObject>();

    private void Update()
    {
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                {
                    items[i].transform.localScale = Vector3.Lerp(items[i].transform.localScale, Vector3.zero, 0.1f);
                    items[i].transform.position = Vector3.Lerp(items[i].transform.position, transform.position, 0.1f);

                    if (items[i].transform.localScale.x <= 0.01f)
                    {
                        GameObject itemToDelete = items[i];
                        items.Remove(items[i]);
                        Destroy(itemToDelete);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            collision.GetComponent<Renderer>().enabled = false;
            items.Add(collision.gameObject);
        }
    }
}
