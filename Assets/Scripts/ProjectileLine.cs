using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; //Singleton

    //fields set in unity inspector panel
    public float minDist = 0.1f;
    public bool __________;

    //fields set dynamically
    public LineRenderer line;
    private GameObject _poi;
    public List<Vector3> points;

    void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>(); // Get a reference to the LineRenderer
        line.enabled = false; // Disable the LineRenderer until it's needed
        points = new List<Vector3>(); // Initialize the points List
    }

    // This is a property (i.e. a method masquerading as a field)
    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                // When _poi is set to something new, it resets everything
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    // This can be used to clear the line directly
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }

    public void AddPoint()
    {
        // This is called to add a point to the Line
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return; // If the point isn't far enough from the last point, it returns
        }

        if (points.Count == 0)
        {
            // If this is the launch point..
            Vector3 launchPos = Slingshot.S.launchPoint.transform.position;
            Vector3 launchPosDiff = pt - launchPos;
            // ...it adds an extra bit of line to aid aiming later
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2; //line.SetVertexCount(2); is obselete
            line.SetPosition(0, points[0]); //set first 2 points
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else
        {
            // Normal behavior of adding a point
            points.Add(pt);
            line.positionCount = points.Count; //line.SetVertexCount(points.Count); is obselete
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    // Returns the location of the most recently added point
    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                return (Vector3.zero); // If there are no points, returns Vector3.zero
            }
            return (points[points.Count - 1]);
        }
    }

    void FixedUpdate()
    {
        if (poi == null)
        {
            // If there is no poi, search for one
            if (FollowCam.S.poi != null)
            {
                if (FollowCam.S.poi.tag == "Projectile")
                {
                    poi = FollowCam.S.poi;
                }
                else
                {
                    return; // Return if we didn't find a poi
                }
            }
            else
            {
                return; // Return if we didn't find a poi
            }
        }
        // If there is a poi, it's loc is added every FixedUpdate
        AddPoint();
        if (poi.GetComponent<Rigidbody>().IsSleeping())
        {
            poi = null; // Once the poi is sleeping, it is cleared
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
