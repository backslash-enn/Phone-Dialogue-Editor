using UnityEngine;

public class GraphWindowScript : MonoBehaviour
{
    private RectTransform r;
    public RectTransform grid;
    public float panSensitivity = 10;
    public float scrollSensitivity = 1;
    private Vector2 panDelta;
    private float zPos;

    void Start()
    {
        r = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            panDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * panSensitivity;
            r.anchoredPosition += panDelta * Mathf.Lerp(10, 60, (zPos + 222) / 1722);
            grid.anchoredPosition += panDelta * Mathf.Lerp(10, 60, (zPos + 222) / 1722);
        }

        zPos = Mathf.Clamp(zPos - Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity, -222, 1500);
        r.anchoredPosition3D = new Vector3(r.anchoredPosition.x, r.anchoredPosition.y, zPos);
        grid.anchoredPosition3D = new Vector3(grid.anchoredPosition.x, grid.anchoredPosition.y, zPos);
    }

    public void ResetPan()
    {
        r.anchoredPosition = grid.anchoredPosition = Vector2.zero;
    }

    public void ResetZoom()
    {
        r.localScale = grid.localScale = Vector3.one;
    }
}
