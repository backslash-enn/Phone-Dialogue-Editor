using System.Collections;
using UnityEngine;

public class ListViewScript : MonoBehaviour
{
    private RectTransform r;
    private bool moving, open;
    private float ref1;
    private WaitForSeconds w;

    void Start()
    {
        r = GetComponent<RectTransform>();
        w = new WaitForSeconds(1);
    }

    void Update()
    {
        if (moving)
            r.anchoredPosition = new Vector2(Mathf.SmoothDamp(r.anchoredPosition.x, (open ? 88.8f : -88.75f), ref ref1, .15f), -.125f);
    }

    public void Transition(bool isOpen)
    {
        open = isOpen;
        StopAllCoroutines();
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        moving = true;
        yield return w;
        moving = false;
    }
}
