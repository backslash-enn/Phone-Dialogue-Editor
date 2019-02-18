using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePanelButtonScript : MonoBehaviour {

    private BoxCollider col;
    public RectTransform r;
    private float height;

	void Start () {
        col = GetComponent<BoxCollider>();
	}
	
	void Update () {
        height = r.rect.height;
        col.size = new Vector3(31, height, .03f);
        col.center = new Vector3(0, -height / 2, 0);
	}
}
