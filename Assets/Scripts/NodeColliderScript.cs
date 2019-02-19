using UnityEngine;

public class NodeColliderScript : MonoBehaviour
{
    private Vector2 startPos, mouseStartPos;
    private RectTransform r;
    public float sens = 1;
    GraphWindowScript gws;
    private Vector2 mouseDelta;

    void Start()
    {
        r = transform.parent.GetComponent<RectTransform>();
        gws = GameObject.Find("Graph Window").GetComponent<GraphWindowScript>();
    }

    private void OnMouseDown()
    {
        mouseStartPos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        startPos = r.anchoredPosition;
    }

    private void OnMouseDrag()
    {
        mouseDelta = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height) - mouseStartPos;
        mouseDelta = new Vector2(mouseDelta.x, mouseDelta.y / Screen.width * Screen.height);
        r.anchoredPosition = startPos + mouseDelta * Mathf.Lerp(465, 4000, (gws.zPos + 222) / 1722);
    }
}
