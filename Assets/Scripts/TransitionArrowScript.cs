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
    private List<Transform> jointHeadObjects;
    public GameObject jointHeadPrefab;
    private List<RectTransform> jointBodyObjects;
    public GameObject jointBodyPrefab;

    void Awake()
    {
        r = GetComponent<RectTransform>();
        body = transform.GetChild(0).GetComponent<RectTransform>();
        joints = new List<PhoneDialogue.Joint>();
        jointHeadObjects = new List<Transform>();
        jointBodyObjects = new List<RectTransform>();
    }

    void Update()
    {
        UpdateArrow();
    }

    public void UpdateArrow()
    {
        int i;

        r.position = intent.position;

        // Make the object count match the joint count
        // If we have too many objects
        for (i = joints.Count; i < jointHeadObjects.Count; i++)
        {
            Destroy(jointHeadObjects[i].gameObject);
            jointHeadObjects.RemoveAt(i);
            Destroy(jointBodyObjects[i].gameObject);
            jointBodyObjects.RemoveAt(i);
        }
        // If we have too few objects
        for (i = jointHeadObjects.Count; i < joints.Count; i++)
        {
            jointHeadObjects.Add(Instantiate(jointHeadPrefab, transform).transform);
            jointBodyObjects.Add(Instantiate(jointBodyPrefab, transform).GetComponent<RectTransform>());
        }

        // Joints
        for (i = 0; i < joints.Count; i++)
        {
            jointHeadObjects[i].localPosition = new Vector3(joints[i].xPos, joints[i].yPos, 0);

            targetVector = jointHeadObjects[i].position;
            diffVector = targetVector - (i == 0 ? intent : jointHeadObjects[i-1]).position;

            jointBodyObjects[i].position = (i == 0 ? intent : jointHeadObjects[i - 1]).position;
            jointBodyObjects[i].eulerAngles = new Vector3(0, 0, Mathf.Atan2(diffVector.y, diffVector.x) * Mathf.Rad2Deg + 90);
            jointBodyObjects[i].sizeDelta = new Vector2(jointBodyObjects[i].sizeDelta.x, diffVector.magnitude);
        }

        // Final Segment
        finalSegmentBasePos = (joints.Count > 0 ? jointHeadObjects[jointHeadObjects.Count-1].position : intent.position);
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
