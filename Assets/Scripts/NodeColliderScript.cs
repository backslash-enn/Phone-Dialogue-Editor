using UnityEngine;

public class NodeColliderScript : MonoBehaviour
{
    private Vector3 startPos, mouseStartPos;
    private RectTransform r;
    public float sens = 1;
    GraphWindowScript gws;

    void Start()
    {
        r = transform.parent.GetComponent<RectTransform>();
        gws = GameObject.Find("Graph Window").GetComponent<GraphWindowScript>();
    }

    private void OnMouseDown()
    {
        mouseStartPos = Input.mousePosition;
        startPos = r.anchoredPosition3D;
    }

    private void OnMouseDrag()
    {
        //if(Input.GetMouseButtonDown(0))
        r.anchoredPosition3D = startPos + (Input.mousePosition - mouseStartPos) * Mathf.Lerp(.245f, 2.1f, (gws.zPos + 222) / 1722);
    }
}
