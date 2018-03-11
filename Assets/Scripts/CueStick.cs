using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueStick : MonoBehaviour {

	//public SteamVR_TrackedObject frontController;
	//public SteamVR_TrackedObject backController;
	//Collider m_Collider;

	//private Rigidbody cueRB;
	//private Vector3 frontPos;
	//private Vector3 backPos;
	//private bool hittedCue;


	//// Use this for initialization
	//void Start () {
	//	cueRB = gameObject.GetComponent<Rigidbody> ();
	//	m_Collider = GetComponent<Collider>();
	//}

	//// Update is called once per frame
	//void Update () {
	//	//frontPos = frontController.transform.position;
	//	//backPos = backController.transform.position;

	//	//cueRB.MovePosition (0.75f * backPos + 0.25f * frontPos);
	//	//cueRB.MoveRotation (Quaternion.LookRotation(frontPos-backPos)*Quaternion.Euler(90f,0f,0f));
	//}

	//public void m_disableCollider()
	//{
	//	m_Collider.enabled = false;
	//}

	//public void m_enableCollider()
	//{
	//	m_Collider.enabled = true;
	//}

	//private void onCollisionEnter(Collision col)
	//{
	//	if (col.gameObject.name == "CueBall") {
	//		hittedCue = true;
	//	} else {
	//		hittedCue = false;
	//	}
	//}

	//public bool hittedCueBall()
	//{
	//	return hittedCue;
	//}

	private Vector3 upVector;
	private float velocity;
	private float maxVelocity;

	private Vector3 lastPosition;

	private void Start()
	{
		lastPosition = this.transform.position;
		maxVelocity = 0;
	}

	private void Update()
	{
		upVector = this.transform.up;


		velocity = (this.transform.position - lastPosition).magnitude;
		if (velocity > maxVelocity)
		{
			maxVelocity = velocity;
		}
		lastPosition = this.transform.position;
		print(maxVelocity);
	}

}
