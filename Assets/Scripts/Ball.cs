using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public GameObject cueBall;

    public int ballNumber;

    public Vector3 startingPosition;

    public bool isPocketed;

	public float velocity;

    // Use this for initialization
    void Start()
    {
		// initialise variables
        isPocketed = false;
		startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		// disappear and disable collider properties if pocketed
		if (isPocketed)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }
		
        velocity = this.GetComponent<Rigidbody>().velocity.magnitude;
    }

    void OnTriggerEnter(Collider col)
    {
		// if pocketed, set isPocketed to true to be fetched by gameHandler
        if (col.gameObject.tag == "HoleCollider")
		{
			isPocketed = true;
			// exclude case where cue ball hits the first ball which is not the smallest number
			if (cueBall.GetComponent<CueBall>().hitState != CueBall.HitState.Foul)		
			    cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.HitPocketed;
		}           
    }

	// reset starting properties when game ends
	public void ReturnStartingPosition()
	{
		transform.position = startingPosition;
		this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
        isPocketed = false;
	}
		
}
