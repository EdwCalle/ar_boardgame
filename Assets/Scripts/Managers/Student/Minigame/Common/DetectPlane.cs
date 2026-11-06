using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DetectPlane : MonoBehaviour
{

    public ARRaycastManager arRaycastManager;
    public ARPlaneManager aRPlaneManager;

    public Slider Slider;
    private List<ARRaycastHit> hits = new();

    //public GameObject objectToPlace;

    public bool isReadyforTap = false;
    public bool isTaped = false;

    //public GameObject items;
    // public GameObject scene;
    public GameObject plane;
    LayerMask layerForDEtect;
    //public GameObject objects;
    void Start()
    {

    }
    public void EnablePlaneDetection(bool value)
    {

        // aRPlaneManager.SetTrackablesActive(value);
        // aRPlaneManager.enabled = value;
    }
    public void EnableTap(bool value)
    {
        if (value)
        {
            plane.SetActive(true);
            //plane.transform.localPosition.X = 1f;
        }


        Slider.gameObject.SetActive(value);
        aRPlaneManager.enabled = value;
        isReadyforTap = value;
        aRPlaneManager.SetTrackablesActive(value);
        aRPlaneManager.enabled = value;
    }
    public void ChangeSale()
    {
        float value = Slider.value / 3;
        transform.localScale = new Vector3(value, value, value);
        isTaped = true;
    }


    void Update()
    {
        if (isReadyforTap)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                bool isUI = IsPointerOverUIObject(touch.position);

                if (!isUI && arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Get the intersection point with the detected plane
                    Pose hitPose = hits[0].pose;
                    transform.localPosition = hitPose.position;
                    transform.rotation = hitPose.rotation;
                }
            }
        }

    }
    public bool IsPointerOverUIObject(Vector2 touchPosition)
    {
        PointerEventData pointerEventData = new(EventSystem.current);
        pointerEventData.position = touchPosition;
        List<RaycastResult> raycastResults = new();

        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        return raycastResults.Count > 0;
    }
}
