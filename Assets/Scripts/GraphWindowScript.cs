using UnityEngine;

public class GraphWindowScript : MonoBehaviour
{
    private RectTransform r;
    public RectTransform grid;
    public float panSensitivity = 10;
    public float scrollSensitivity = 1;
    private Vector2 panDelta;
    public float zPos;
    private Vector2 mouseStartPos, windowStartPos, gridStartPos;

    void Start()
    {
        r = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mouseStartPos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
            windowStartPos = r.anchoredPosition;
            gridStartPos = grid.anchoredPosition;
        }
        if (Input.GetMouseButton(1))
        {
            panDelta = (mouseStartPos - new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height));
            panDelta = new Vector2(panDelta.x, panDelta.y / Screen.width * Screen.height);
            r.anchoredPosition = windowStartPos - panDelta * Mathf.Lerp(465, 4000, (zPos + 222) / 1722);
            grid.anchoredPosition = (gridStartPos - panDelta * Mathf.Lerp(465, 4000, (zPos + 222) / 1722));
        }

        zPos = Mathf.Clamp(zPos - Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity, -222, 1500);
        r.anchoredPosition3D = new Vector3(r.anchoredPosition.x, r.anchoredPosition.y, zPos);
        grid.anchoredPosition3D = new Vector3(grid.anchoredPosition.x, grid.anchoredPosition.y, zPos);
        grid.anchoredPosition = new Vector2(grid.anchoredPosition.x % 400, grid.anchoredPosition.y % 400);
    }

    public void ResetPan()
    {
        r.anchoredPosition = grid.anchoredPosition = Vector2.zero;
    }

    public void ResetZoom()
    {
        zPos = 0;
    }
}
