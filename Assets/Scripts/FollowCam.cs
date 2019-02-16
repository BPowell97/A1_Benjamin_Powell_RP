using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public FollowCam S; //Singleton, followcam

    public float easing = 0.05f;
    public Vector2 minXY;
    public bool _____________;

    public GameObject poi; //Point of interest
    public float camZ; // The Z position of camera

    private void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (poi == null) return;  //return if no point of interest

        //Get position of poi
        Vector3 destination = poi.transform.position;
        //Limit the X/Y to min values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Interpolate from the current Cammera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //Retain a destination.z of cam
        destination.z = camZ;
        //Set the camera to the destination
        transform.position = destination;
        //Set the orthographicSize of camrea to keep ground in view
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
    }
}
