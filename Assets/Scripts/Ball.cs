using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int ballNumber;
    public Vector3 startingPosition;

    public bool isPocketed;

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
            this.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "HoleCollider")
            isPocketed = true;
    }

	public void returnStartingPosition()
	{
		transform.position = startingPosition;
	}
		
}
