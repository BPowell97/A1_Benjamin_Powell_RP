using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    // fields set in unity inspector panel
    public GameObject prefabProjectile;
    public float velocityMult = 4f;
    public bool ___________________________;
    // fields set dynamically
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    public Rigidbody rb;

    private void Start()
    {
        rb = projectile.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
    }

    void OnMouseEnter() //Do not Change
    {
        //print("Slingshot: OnMouseEnter()"); //Test if collider works
        launchPoint.SetActive(true);
    }

    void OnMouseExit() //Do Not Change
    {
        //print("SlingShot: OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        //The Player has pressed the mouse button while over slingshot
        aimingMode = true;
        //Instantiate a projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        //Start it at the launchPoint
        projectile.transform.position = launchPos;
        //Set it to isKinematic for now
        rb.isKinematic = false;
    }

    private void Update()
    {
        //If Slingshot is not in aimingMode, DOn't run this code
        if (!aimingMode) return;
        //Get the current mouse position in 2d screen coordinates 
        Vector3 mousePos2D = Input.mousePosition;
        //Convert mouse position to 3d world coordinates
        mousePos2D.z = Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //Find the delta from the launchPos to the mousePos3d
        Vector3 mouseDelta = mousePos3D - launchPos;
        //Limit mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //Move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            rb.isKinematic = false;
            rb.velocity = -mouseDelta * velocityMult;
            projectile = null;
        }
    }


}
