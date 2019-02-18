using System.Collections.Generic;
using UnityEngine;

public class TransitionArrowScript : MonoBehaviour
{
    private RectTransform r, body;
    private RaycastHit[] hits;
    private Vector3 diffVector, targetVector, finalSegmentBasePos;
    public bool targetMouse;
    public Transform intent, target;
    public List<PhoneDialogue.Joint> joints;
    private Transform[] jointHeadObjects;
    public GameObject jointHeadPrefab;
    private RectTransform[] jointBodyObjects;
    public GameObject jointBodyPrefab;

    void Awake()
    {
        r = GetComponent<RectTransform>();
        body = transform.GetChild(0).GetComponent<RectTransform>();
        joints = new List<PhoneDialogue.Joint>();
        jointHeadObjects = new Transform[0];
    }

    void Update()
    {
        UpdateArrow();
    }

    public void UpdateArrow()
    {
        int i;

        r.position = intent.position;

        for (i = 0; i < jointHeadObjects.Length; i++)
        {
            Destroy(jointHeadObjects[i].gameObject);
            Destroy(jointBodyObjects[i].gameObject);
        }

        jointHeadObjects = new Transform[joints.Count];
        jointBodyObjects = new RectTransform[joints.Count];

        // Joints
        for (i = 0; i < joints.Count; i++)
        {
            jointHeadObjects[i] = Instantiate(jointHeadPrefab, transform).transform;
            jointHeadObjects[i].localPosition = new Vector3(joints[i].xPos, joints[i].yPos, 0);

            targetVector = jointHeadObjects[i].position;
            diffVector = targetVector - (i == 0 ? intent : jointHeadObjects[i-1]).position;

            jointBodyObjects[i] = Instantiate(jointBodyPrefab, transform).GetComponent<RectTransform>();
            jointBodyObjects[i].position = (i == 0 ? intent : jointHeadObjects[i - 1]).position;
            jointBodyObjects[i].eulerAngles = new Vector3(0, 0, Mathf.Atan2(diffVector.y, diffVector.x) * Mathf.Rad2Deg + 90);
            jointBodyObjects[i].sizeDelta = new Vector2(jointBodyObjects[i].sizeDelta.x, diffVector.magnitude);
        }

        // Final Segment
        finalSegmentBasePos = (joints.Count > 0 ? jointHeadObjects[jointHeadObjects.Length-1].position : intent.position);
        targetVector = !targetMouse ? target.position : Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 450));
        diffVector = targetVector - finalSegmentBasePos;

        if (!targetMouse)
        {
            hits = Physics.RaycastAll(finalSegmentBasePos, diffVector, diffVector.magnitude);
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.name == "Node Collider" && hit.transform.parent == target)
                    diffVector *= (hit.distance / diffVector.magnitude);
            }
        }

        body.position = finalSegmentBasePos;
        body.sizeDelta = new Vector2(r.sizeDelta.x, diffVector.magnitude);
        body.eulerAngles = new Vector3(0, 0, Mathf.Atan2(diffVector.y, diffVector.x) * Mathf.Rad2Deg + 90);
    }
}
