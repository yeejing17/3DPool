using System.Collections;
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
	public GameObject cueBall;

    public Quaternion cueStickRotation;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start()
    {
        cueStickRotation = cueStick.transform.localRotation;
    }

    // Update is called once per frame
    void Update ()
    {
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

			if (Controller.GetHairTriggerDown() && 
				cueBall.transform.parent == this.transform)
				// TODO: set another condition where below is the surface collider
			{
				cueBall.transform.parent = cueBall.GetComponent<CueBall>().originalParent.transform;
				cueBall.GetComponent<Rigidbody>().useGravity = true;
				cueBall.GetComponent<Rigidbody>().isKinematic = false;				
			}
        }
		
	}
}
