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
        isPocketed = false;
		startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		// TODO: set condition changes when ball is pocketed or not when trigger with hole collider
		if (isPocketed)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }
			//this.gameObject.SetActive(false);
			


        velocity = this.GetComponent<Rigidbody>().velocity.magnitude;
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "HoleCollider")
		{
			isPocketed = true;
            if (cueBall.GetComponent<CueBall>().hitState != CueBall.HitState.Foul)
			    cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.HitPocketed;
		}
            
    }

	public void ReturnStartingPosition()
	{
		transform.position = startingPosition;
		this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<Collider>().enabled = true;
        isPocketed = false;
	}
		
}
