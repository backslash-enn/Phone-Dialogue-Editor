using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreenScript : MonoBehaviour
{

    public static Vector2 touchPosition;
    public static bool beingTouched;
    public static GameObject touchedObject;
    private Vector2 positionOffset;
    public RectTransform r, p;
    private RaycastHit hit;
    private LayerMask lm;

	void Start () {
        positionOffset = new Vector2(r.rect.width / 2, 70 / 2);
        lm = 1 << 5;
    }
	
	void Update () {

        touchPosition = r.anchoredPosition - positionOffset + new Vector2(Input.mousePosition.x / Screen.currentResolution.height * r.rect.width, Input.mousePosition.y / Screen.currentResolution.height * 70);
        touchPosition = new Vector2(Mathf.Clamp(touchPosition.x, -r.rect.width / 2, r.rect.width / 2), Mathf.Clamp(touchPosition.y, -70 / 2, 60f / 2));
        beingTouched = Input.GetMouseButton(0);
        if (beingTouched)
        {
            if (Physics.Raycast(p.position, p.transform.forward, out hit, 10f, lm))
                touchedObject = hit.collider.gameObject;
            else
                touchedObject = null;
        }   

        p.anchoredPosition = touchPosition;		
	}
}
