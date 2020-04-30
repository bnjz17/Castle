using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cutter : MonoBehaviour
{
    public float radius = 1f;

    bool isMoving = false;
    bool canMove = true;
    EventTrigger inputButton;
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputButton = GameObject.FindWithTag("Player Input").GetComponent<EventTrigger>();
    }

    void Start()
    {
        SetInput();
    }

    private void Update()
    {
        if (canMove && isMoving)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
            transform.position = mousePos;

            foreach (RegionGenerator region in LevelManager.Instance.activeRegions)
                region.Cut(mousePos, radius);
        }
    }

    void SetInput()
    {
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        EventTrigger.Entry entryUp = new EventTrigger.Entry();

        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });

        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => { OnPointerUpDelegate((PointerEventData)data); });

        inputButton.triggers.Add(entryDown);
        inputButton.triggers.Add(entryUp);
    }

    void OnPointerDownDelegate(PointerEventData data)
    {
        isMoving = true;
    }

    void OnPointerUpDelegate(PointerEventData data)
    {
        isMoving = false;
    }

    public void CanMove(bool value)
    {
        canMove = value;
    }
}
