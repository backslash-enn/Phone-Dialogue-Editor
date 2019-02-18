using UnityEngine;

public class PhoneUIButtonScript : MonoBehaviour {

    public bool pressed;
    public bool justReleased;
	
	void Update () {
        justReleased = pressed && TouchScreenScript.touchedObject == gameObject && !TouchScreenScript.beingTouched;
        pressed = TouchScreenScript.touchedObject == gameObject && TouchScreenScript.beingTouched;
	}
}
