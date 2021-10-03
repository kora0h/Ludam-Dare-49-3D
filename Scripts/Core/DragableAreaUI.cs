using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragableAreaUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public GameObject referAreaPrefab;

    private Vector2 lastMousePosition;

    private void Start()
    {

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.bEnableGame)
            return;
        
        //Debug.Log("Begin Drag");
        lastMousePosition = eventData.position;
        GameManager.Instance.tileManager.bDragUI = true;

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, transform.position.z);
        Vector3 oldPos = rect.position;
        rect.position = newPosition;
        if (!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        //Implement your funtionlity here
        GameManager.Instance.tileManager.bDragUI = false;

        if (GameManager.Instance.tileManager.currentMouseGroundID == -1)
        {
            // reset position;
            GameManager.Instance.tileManager.areaUIHolder.GetComponent<GridLayoutGroup>().enabled = false;
            GameManager.Instance.tileManager.areaUIHolder.GetComponent<GridLayoutGroup>().enabled = true;
        }
        else
        {
            if(GameManager.Instance.tileManager.ReplaceArea(referAreaPrefab, this))
            {
                Destroy(this.gameObject);
            }
            else
            {
                GameManager.Instance.tileManager.areaUIHolder.GetComponent<GridLayoutGroup>().enabled = false;
                GameManager.Instance.tileManager.areaUIHolder.GetComponent<GridLayoutGroup>().enabled = true;
            }
        }
    }

    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }
}
