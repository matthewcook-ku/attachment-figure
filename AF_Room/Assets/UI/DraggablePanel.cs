using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    // The object in the UI hierarchy on the pannel that can be dragged. If none is given, drag this whole object.
    public GameObject draggableArea;

    private RectTransform rectTransform;

    // are we currently dragging the panel
    private bool dragging;

    // offset within this panel object to avoid snapping to center when dragging.
    private Vector2 offset;

    public void Start()
    {
        // if no sub panel area is supplied, use the whole panel.
        if(draggableArea == null)
        {
            draggableArea = this.gameObject;
        }
        rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        if (dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - offset;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // check if the click was within the draggable area.
        if(eventData.pointerPressRaycast.gameObject == draggableArea)
        {
            //Debug.Log("Dragging!");
            dragging = true;
            offset = eventData.position - new Vector2(transform.position.x, transform.position.y);

            // place this panel on top
            rectTransform.SetAsLastSibling();
        }
        else
        {
            Debug.Log("Not a dragging location on this panel.");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Dragging ended");
        dragging = false;
    }
}