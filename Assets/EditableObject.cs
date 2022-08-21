using UnityEngine;

public class EditableObject : MonoBehaviour
{
    //Is Dragging Protected bool.
    protected bool isDragging = false;

    //Can Drag Bool
    public bool canDrag = true;

    [Header("Colors")]
    public Color NaturalColor;
    public Color DragColor;

    [Header("Bounds")]
    public float xBoundMax, xBoundMin, yBoundMax, yBoundMin;

    [Header("ID")]
    public string ID;

    private void OnMouseDown()
    {
        if (canDrag)
        {
            isDragging = true;
        }
    }

    private void OnMouseOver()
    {
        if (canDrag)
        {
            transform.GetComponent<SpriteRenderer>().color = DragColor;
        }
    }

    private void OnMouseExit()
    {
        if (canDrag)
        {
            transform.GetComponent<SpriteRenderer>().color = NaturalColor;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 rawMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 realMousePos = new Vector2(Mathf.Clamp(rawMousePos.x, xBoundMin, xBoundMax), Mathf.Clamp(rawMousePos.y, yBoundMin, yBoundMax));
            transform.position = realMousePos;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
