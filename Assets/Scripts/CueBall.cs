using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour {

	public enum HitState { Idle, Miss, Foul, Hit };

	public GameObject ballGroup;

	public Vector3 startingPosition;

	public HitState hitState;

	// Use this for initialization
	void Start ()
	{
		// initialize variables
		hitState = HitState.Idle;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//  TODO: change hitState based on rules and first ball hit (or not)
		switch (hitState)
		{
			case HitState.Idle:

				break;

			case HitState.Miss:

				break;

			case HitState.Foul:

				break;

			case HitState.Hit:

				break;

			default:
				break;
		}
	}
}
