using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;


[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{

    [SerializeField]
    private GameObject prefab;

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        aRPlaneManager = GetComponent<ARPlaneManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0)
        {
            return;
        }

        if (arRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (var hit in hits)
            {
                Pose pose = hit.pose;
                GameObject gameObject = Instantiate(prefab, pose.position, pose.rotation);

                if (aRPlaneManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp)
                {
                    Vector3 postion = gameObject.transform.position;
                    postion.y = 0.0f;

                    Vector3 cameraPosition = Camera.main.transform.position;
                    cameraPosition.y = 0.0f;

                    Vector3 direction = cameraPosition - postion;
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    gameObject.transform.rotation = targetRotation;
                }
            }
        }
    }
}
