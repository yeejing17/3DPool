using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{

    public enum HitState { Idle, Before, Miss, Foul, Hit };

    public GameObject ballGroup;
	public GameObject cueStick;

    public Vector3 startingPosition;

    public HitState hitState;

    public int firstBallHit = 1;

	public bool hitCue;

	Collider m_Collider;

	private float cueStickVelocity;
	private Vector3 cueStickLastPosition;

	// Use this for initialization
	void Start()
    {
        // initialize variables
        hitState = HitState.Idle;
        firstBallHit = 1;
		m_Collider = GetComponent<Collider>();
		hitCue = false;

		startingPosition = this.transform.position;

		cueStickLastPosition = cueStick.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		//print(hitCue);
		print(hitState);

        //  TODO: change hitState based on rules and first ball hit (or not)
        switch (hitState)
        {
            case HitState.Idle:
                // waiting game start, do nothing
                break;

            case HitState.Before:
                var ballsOnTable = GameObject.FindGameObjectsWithTag("NumberBall");
                firstBallHit = 15;
                foreach (var ball in ballsOnTable)
                {
                    if (ball.active && firstBallHit > ball.GetComponent<Ball>().ballNumber)
                        firstBallHit = ball.GetComponent<Ball>().ballNumber;
                }
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
		}

		if(col.gameObject == cueStick)
		{
			hitCue = true;

			Vector3 upVector = col.transform.up;
			Vector3 upVector2D = new Vector3(upVector.x, 0, upVector.z);

			cueStickVelocity = (this.transform.position - cueStickLastPosition).magnitude;

			this.GetComponent<Rigidbody>().velocity = (upVector2D * cueStickVelocity);

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
    }
	public void returnStartingPosition()
	{
		transform.position = startingPosition;
	}
}
