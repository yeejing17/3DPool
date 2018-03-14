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
        }
		
	}
}
