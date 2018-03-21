using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{

    public enum HitState { Idle, Before, Miss, Foul, Hit, HitPocketed };

    public GameObject ballGroup;
	public GameObject cueStick;
    public GameObject cueController;
    public GameObject ballRange;

    public HitState hitState;

    public int firstBallHit = 1;

	public bool hitCue;

	public float velocity;
	public float cueStickVelocity;

	public Vector3 startingPosition;
	public Vector3 upVector2D;
	private Vector3 cueStickLastPosition;

	public Transform originalParent;
    public Transform controllerParent;

	// Use this for initialization
	void Start()
    {
        // initialize variables
        hitState = HitState.Idle;
        firstBallHit = 1;
		hitCue = false;
        originalParent = this.transform.parent;
		startingPosition = this.transform.position;
		cueStickLastPosition = cueStick.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		print(hitState);
		velocity = this.GetComponent<Rigidbody>().velocity.magnitude;

		// get the lowest number ball
		var ballsOnTable = GameObject.FindGameObjectsWithTag("NumberBall");
		firstBallHit = 15;
		foreach (var ball in ballsOnTable)
		{
			if (ball.GetComponent<MeshRenderer>().enabled && firstBallHit > ball.GetComponent<Ball>().ballNumber)
				firstBallHit = ball.GetComponent<Ball>().ballNumber;
		}

		cueStickLastPosition = cueStick.transform.position;
	}

	private void OnTriggerEnter(Collider col)
	{
		// if pocketed
		if(col.gameObject.tag == "HoleCollider")
		{
			hitState = HitState.Foul;
			this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
		}

		// trigger collision between cue and cue ball
		if(col.gameObject == cueStick && hitCue == false &&
           cueController.GetComponent<ControllerInput>().canHitCueBall == true)
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

    private void OnTriggerExit(Collider col)
    {
		// if ball flies out of table
        if (col.gameObject == ballRange)
        {
            hitState = HitState.Foul;
            //this.gameObject.SetActive(false);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }
    }
    public void returnStartingPosition()
	{
		// reset starting properties when game ends
		transform.position = startingPosition;
		this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
	}
}
