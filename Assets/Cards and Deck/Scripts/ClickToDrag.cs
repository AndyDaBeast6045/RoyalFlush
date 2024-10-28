using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickToDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, 
    IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    bool isDragging;
    bool isHovering;

    Vector2 fromMouse;
    [SerializeField]
    float moveSpeedLimit;

    float pointerUpTime;
    float pointerDownTime;

    public bool selected; // true once card has been dragged onto a target
    public float selectionOffset;

    [HideInInspector] public UnityEvent<ClickToDrag> PointerEnterEvent;
    [HideInInspector] public UnityEvent<ClickToDrag> PointerExitEvent;
    [HideInInspector] public UnityEvent<ClickToDrag, bool> PointerUpEvent;
    [HideInInspector] public UnityEvent<ClickToDrag> PointerDownEvent;
    [HideInInspector] public UnityEvent<ClickToDrag> BeginDragEvent;
    [HideInInspector] public UnityEvent<ClickToDrag> EndDragEvent;
    [HideInInspector] public UnityEvent<ClickToDrag, bool> SelectEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClampPosition();

        if (isDragging)
        {
            Vector2 targetPosition = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - fromMouse;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
    }
    void ClampPosition()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
        transform.position = new Vector3(clampedPosition.x, clampedPosition.y, 0);
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        BeginDragEvent.Invoke(this);
        if (selected)
        {
            selected = false; 
            // cancel targeting
        }
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fromMouse = (Vector2) transform.position - mouse;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent.Invoke(this);
        isDragging = false;
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        PointerEnterEvent.Invoke(this);
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerEnterEvent.Invoke(this);
        isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        PointerDownEvent.Invoke(this);
        pointerDownTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        PointerDownEvent.Invoke(this);
        pointerUpTime = Time.time;
    }
}
