using UnityEngine;

public class GridBackgroundScript : MonoBehaviour
{
    private RectTransform r;

    void Start()
    {
        r = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (r.anchoredPosition.x < -400)
            r.anchoredPosition += new Vector2(400, 0);
        else if(r.anchoredPosition.x > 400)
            r.anchoredPosition += new Vector2(-400, 0);

        if (r.anchoredPosition.y < -400)
            r.anchoredPosition += new Vector2(0, 400);
        else if (r.anchoredPosition.y > 400)
            r.anchoredPosition += new Vector2(0, -400);
    }
}
