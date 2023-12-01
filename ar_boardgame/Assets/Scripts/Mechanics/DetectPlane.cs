using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DetectPlane : MonoBehaviour
{

    public ARRaycastManager arRaycastManager;
    public ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //public GameObject objectToPlace;

    public bool isReadyforTap = false;
    public bool isTaped = false;
    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0 && !isTaped)
        {
            Touch touch = Input.GetTouch(0);

            if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                isTaped = true;
                transform.localPosition = hitPose.position;
                transform.rotation = hitPose.rotation;
                aRPlaneManager.enabled = false;
                aRPlaneManager.SetTrackablesActive(false);

                //  Instantiate(gameObject, hitPose.position, hitPose.rotation);
            }
        }
    }
}
