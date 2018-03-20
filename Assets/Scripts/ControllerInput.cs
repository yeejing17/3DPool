﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GameObject otherController;

    public GameObject cueStick;

    public Quaternion cueStickRotation;

    public bool canHitCueBall;

    public GameObject cueBall;

    public Collider surfaceCollider;
    public Collider ballRangeCollider;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start()
    {
        cueStickRotation = cueStick.transform.localRotation;
        canHitCueBall = false;
    }

    // Update is called once per frame
    void Update ()
    {
        RaycastHit hit;
        Ray ray = new Ray(cueBall.transform.position, new Vector3(0, -1, 0));
        
        // test raycast hit
        //if (cueBall.transform.parent == this.transform && Physics.Raycast(ray, out hit))
        //{
        //    print(hit.collider);
        //}

        if (this.gameObject.name == "Controller (left)")
        {
            if (Controller.GetHairTrigger())
            {
                cueStick.transform.LookAt(this.transform);
                cueStick.transform.Rotate(90, 0, 0);
            }
            else
            {
                cueStick.transform.localRotation = cueStickRotation;
            }

            if (Controller.GetHairTriggerDown() && cueBall.transform.parent == this.transform)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == surfaceCollider)
                    {
                        print(hit.collider);
                        cueBall.transform.parent = cueBall.GetComponent<CueBall>().originalParent;
                        cueBall.GetComponent<Rigidbody>().isKinematic = false;
                        cueBall.GetComponent<Collider>().isTrigger = false;
                    ballRangeCollider.enabled = true;
                    }
                    
                }
            }
        }

        if (this.gameObject.name == "Controller (right)")
        {
            if (Controller.GetHairTrigger() && cueBall.transform.parent == cueBall.GetComponent<CueBall>().originalParent )
            {
                canHitCueBall = true;
            }
            else
            {
                canHitCueBall = false;
            }
        }
		
	}
}
