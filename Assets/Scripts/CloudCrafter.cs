using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    //Fields set in the unity inspector
    public int numClouds = 40;
    public GameObject cloudPrefabs;
    public Vector3 cloudPosMin;
    public Vector3 cloudPosMax;
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 5;
    public float cloudSpeedMult = 0.5f;

    public bool _____________;

    //fields set dynamically
    public GameObject cloudInstances;

    void Awake()
    {
        // Make an array large enough to hold all the Clou_ instances
        cloudInstances = new GameObject[numClouds];
        // Find the CloudAnchor parent GameObject
        GameObject anchor = GameObject.Find("CloudAnchor");
        // Iterate through and make Cloud_s
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // Pick an int between 0 and cloudPrefabs.Length - 1
            // Random.Range will not ever pick as high as the top number
            int prefabNum = Random.Range(0, cloudPrefabs.Length);
            // Make an instance
            cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;
            // Position cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            // Scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // Smaller clouds (with smaller scaleU) should be nearer the ground
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            // Smaller clouds should be further away
            cPos.z = 100 - (90 * scaleU);
            // Apply these transforms to the cloud
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            // Make cloud a child of the anchor
            cloud.transform.parent = anchor.transform;
            // Add the cloud to cloudInstances
            cloudInstances[i] = cloud;
        }
    }

    void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            if (cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }
            cloud.transform.position = cPos;
        }
        
    }
}
