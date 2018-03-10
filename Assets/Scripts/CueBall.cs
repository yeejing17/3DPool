using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{

    public enum HitState { Idle, Before, Miss, Foul, Hit };

    public GameObject ballGroup;

    public Vector3 startingPosition;

    public HitState hitState;

    public int firstBallHit = 1;

	Collider m_Collider;

    // Use this for initialization
    void Start()
    {
        // initialize variables
        hitState = HitState.Idle;
        firstBallHit = 1;
		m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HoleCollider")
        {
            hitState = HitState.Foul;
            this.gameObject.SetActive(false);
        }

        if (hitState == HitState.Miss)
        {
            if (collision.GetType() == typeof(SphereCollider))
                if (collision.gameObject.GetComponent<Ball>().ballNumber == firstBallHit)
                    hitState = HitState.Hit;
                else
                    hitState = HitState.Foul;
        }
    }
	public void returnStartingPosition()
	{
		transform.position = startingPosition;
	}
}
