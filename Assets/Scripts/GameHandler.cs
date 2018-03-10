using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	enum GameState { AwaitingStart, Player1Turn, Player2Turn, Pending1Turn, Pending2Turn };

	public CueBall cueBall;		// might switch type to CueBall
	public GameObject ballGroup;
	public CueStick cueStick;

	GameState gameState;
	bool allBallsStopMoving;

	// Use this for initialization
	void Start ()
	{
		// initialize variables

		gameState = GameState.AwaitingStart;
		allBallsStopMoving = true;
	}

	// Update is called once per frame
	void Update ()
	{
		// gameState changes
		switch (gameState)
		{
			case GameState.AwaitingStart:

				cueBall.GetComponent<CueBall> ().hitState = CueBall.HitState.Idle;

				// TODO: disable all ball interactions with cue during GameState.AwaitingStart
				//done	

				cueStick.GetComponent<CueStick> ().m_disableCollider ();

				if (true /*confirm start game*/)
				{
					gameState = GameState.Player1Turn;
				}
				break;

			case GameState.Player1Turn:

				cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Before;

				// TODO: enable collision between cue and cueBall during GameState.Player1Turn
				//done
				cueStick.GetComponent<CueStick>().m_enableCollider();

				if (cueStick.GetComponent<CueStick>().hittedCueBall()) //TODO: check if cue hits cue ball: done
				{
					gameState = GameState.Pending1Turn;
					cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Miss;
					allBallsStopMoving = false;
				}
				break;

			case GameState.Pending1Turn:

				// TODO: check if all balls stop moving during GameState.Pending1Turn

				if (allBallsStopMoving)
				{
					switch ((cueBall.GetComponent<CueBall>().hitState))
					{
						case CueBall.HitState.Hit:
							
							bool allPocketed = true;
							Ball[] all9balls = ballGroup.GetComponentsInChildren<Ball>();
							foreach (Ball ball in all9balls) {
								allPocketed = allPocketed & ball.GetComponent<Ball> ().isPocketed; //if all balls are pocketed: allPocketed = 1*1*1*1.... = 1
							}

							if (allPocketed) /*TODO: Set if player1 win game*/
							{
								// TODO: Announce win condition during GameState.Pending1Turn && CueBall.HitState.Hit
								print("player 1 wins!");
								ResetGame();
								gameState = GameState.AwaitingStart;
							
							
							}
							else
							{
								gameState = GameState.Player1Turn;
										
							}

							break;

						case CueBall.HitState.Miss:
							gameState = GameState.Player2Turn;
							break;

						case CueBall.HitState.Foul:
							gameState = GameState.Player2Turn;
							
							// TODO: Set condition where player 2 can place cueBall during GameState.Pending1Turn && CueBall.HitState.Foul
							break;

						default:
							break;
					}
				}
				break;

			case GameState.Player2Turn:

				cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Before;

				// TODO: enable collision between cue and cueBall during GameState.Player2Turn
				//done
				cueStick.GetComponent<CueStick> ().m_enableCollider ();
				if (cueStick.GetComponent<CueStick>().hittedCueBall()) //done TODO: check cue hits cue ball
				{
					gameState = GameState.Pending2Turn;
					cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Miss;
					allBallsStopMoving = false;
				}
				break;

			case GameState.Pending2Turn:

				// TODO: check if all balls stop moving during GameState.Pending2Turn

				if (allBallsStopMoving)
				{
					switch ((cueBall.GetComponent<CueBall>().hitState))
					{
						case CueBall.HitState.Hit:

							bool allPocketed = true;
							Ball[] all9balls = ballGroup.GetComponentsInChildren<Ball>();
							foreach (Ball ball in all9balls) {
								allPocketed = allPocketed & ball.GetComponent<Ball> ().isPocketed; //if all balls are pocketed: allPocketed = 1*1*1*1.... = 1
							}

							if (allPocketed) /*TODO: Set if player2 win game*/
							{
								// TODO: Announce win condition during GameState.Pending2Turn && CueBall.HitState.Hit
								//done
								print("player 2 wins!");
								ResetGame();
								gameState = GameState.AwaitingStart;
							}
							else
							{
								gameState = GameState.Player2Turn;

							}
							break;

						case CueBall.HitState.Miss:
							gameState = GameState.Player1Turn;
							break;

						case CueBall.HitState.Foul:
							gameState = GameState.Player1Turn;
							// TODO: Set condition where player 1 can place cueBall during GameState.Pending2Turn && CueBall.HitState.Foul
							break;

						default:
							break;
					}

				}
				break;
		}
	}

	void ResetGame()
	{
		//define starting position in ball.cs?

		// TODO: reset game and ball positions after win condition
		Ball[] all9balls = ballGroup.GetComponentsInChildren<Ball>();
		foreach (Ball ball in all9balls){
			ball.GetComponent<Ball> ().returnStartingPosition();
		}
		cueBall.GetComponent<CueBall> ().returnStartingPosition();
		gameState = GameState.AwaitingStart;
	}
}
