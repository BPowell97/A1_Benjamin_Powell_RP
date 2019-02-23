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

    void FixedUpdate()
    {
        Vector3 destination;
        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = poi.transform.position;
            if (poi.tag == "Projectile")
            {
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    poi = null;
                    return;
                }
            }
        }
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
