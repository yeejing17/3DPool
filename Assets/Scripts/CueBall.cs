using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{

    public enum HitState { Idle, Before, Miss, Foul, Hit, HitPocketed };

    public GameObject ballGroup;
	public GameObject cueStick;

    public Vector3 startingPosition;

    public HitState hitState;

    public int firstBallHit = 1;

	public bool hitCue;

	Collider m_Collider;

	public float cueStickVelocity;
	private Vector3 cueStickLastPosition;

	public float velocity;
	public float angularVelocity;

	public Vector3 upVector2D;

	bool isPocketed;

	public GameObject originalParent;
	public GameObject controllerParent;
	public Collider roofCollider;

	// Use this for initialization
	void Start()
    {
        // initialize variables
        hitState = HitState.Idle;
        firstBallHit = 1;
		m_Collider = GetComponent<Collider>();
		hitCue = false;
		isPocketed = false;

		originalParent = this.transform.parent.gameObject;

		startingPosition = this.transform.position;

		cueStickLastPosition = cueStick.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		//print("HitCue: " + hitCue);
		print(hitState);
		velocity = this.GetComponent<Rigidbody>().velocity.magnitude;
		angularVelocity = this.GetComponent<Rigidbody>().angularVelocity.magnitude;

		var ballsOnTable = GameObject.FindGameObjectsWithTag("NumberBall");
		firstBallHit = 15;
		foreach (var ball in ballsOnTable)
		{
			if (ball.active && firstBallHit > ball.GetComponent<Ball>().ballNumber)
				firstBallHit = ball.GetComponent<Ball>().ballNumber;
		}

		//  TODO: change hitState based on rules and first ball hit (or not)
		switch (hitState)
        {
            case HitState.Idle:
                // waiting game start, do nothing
                break;

            case HitState.Before:

                break;

            case HitState.Miss:
                // if hit, change status to hit
				
                // if touch hole collider, foul

                // if touch any other ball, foul
                break;

            case HitState.Foul:
                // do nothing, waiting ball stopped
                break;

            case HitState.Hit:
                // if touch hole collider, foul
                // do nothing, waiting ball stopped
                break;

			case HitState.HitPocketed:

				break;
                
            default:
                break;
        }

		cueStickLastPosition = cueStick.transform.position;
	}

	private void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "HoleCollider")
		{
			hitState = HitState.Foul;
			//this.gameObject.SetActive(false);
			this.gameObject.GetComponent<MeshRenderer>().enabled = false;
			isPocketed = true;
		}

		if(col.gameObject == cueStick && hitCue == false && this.transform.parent == originalParent.transform)
		{
			hitCue = true;

			Vector3 upVector = col.transform.up;
			upVector2D = new Vector3(upVector.x, 0, upVector.z);

			cueStickVelocity = (cueStick.transform.position - cueStickLastPosition).magnitude;

			this.GetComponent<Rigidbody>().velocity = (upVector2D * cueStickVelocity * 300);

		}
	}

	void OnCollisionEnter(Collision collision)
    {
		//// collides with cue
		//if (collision.gameObject.name == "CueStick")
		//{
		//	hitCue = true;
		//}


		// collides with number ball
        if (hitState == HitState.Miss)
        {
			if (collision.transform.GetComponent<Ball>())
				if (collision.transform.GetComponent<Ball>().ballNumber == firstBallHit)
				{
					hitState = HitState.Hit;
				}
                else
                    hitState = HitState.Foul;
        }

		if (collision.gameObject.name == "Surface Collider")
		{
			this.GetComponent<CueBall>().roofCollider.GetComponent<Collider>().isTrigger = false;
		}
    }
	public void returnStartingPosition()
	{
		transform.position = startingPosition;
		this.GetComponent<MeshRenderer>().enabled = true;
		isPocketed = false;
	}
}
