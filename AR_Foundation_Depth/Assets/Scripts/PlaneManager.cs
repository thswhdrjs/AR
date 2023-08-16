using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneManager : MonoBehaviour
{
    public GameObject placeObj;
    private ARRaycastManager raycastMgr;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        raycastMgr = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if(touch.phase == TouchPhase.Began)
        {
            if (raycastMgr.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                Instantiate(placeObj, hits[0].pose.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
}
