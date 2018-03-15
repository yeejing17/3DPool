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
	public float angularVelocity;

    // Use this for initialization
    void Start()
    {
        isPocketed = false;
		startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		// TODO: set condition changes when ball is pocketed or not when trigger with hole collider
		if (isPocketed)
			//this.gameObject.SetActive(false);
			this.GetComponent<MeshRenderer>().enabled = false;

		velocity = this.GetComponent<Rigidbody>().velocity.magnitude;
		angularVelocity = this.GetComponent<Rigidbody>().angularVelocity.magnitude;
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "HoleCollider")
		{
			isPocketed = true;
			cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.HitPocketed;
		}
            
    }

	public void ReturnStartingPosition()
	{
		transform.position = startingPosition;
		this.GetComponent<MeshRenderer>().enabled = true;
		isPocketed = false;
	}
		
}
