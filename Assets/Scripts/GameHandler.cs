using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

	enum GameState { AwaitingStart, Player1Turn, Player2Turn, Pending1Turn, Pending2Turn };

	public GameObject cueBall;		// might switch type to CueBall
	public GameObject ballGroup;

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

				cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Idle;

				// TODO: disable all ball interactions with cue during GameState.AwaitingStart

				if (true /*confirm start game*/)
				{
					gameState = GameState.Player1Turn;
				}
				break;

			case GameState.Player1Turn:

				cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Before;

				// TODO: enable collision between cue and cueBall during GameState.Player1Turn

				if (true /*cue hit cueBall*/)
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
							if (true /*TODO: Set if player1 win game*/)
							{
								// TODO: Announce win condition during GameState.Pending1Turn && CueBall.HitState.Hit
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

				if (true /*cue hit cueBall*/)
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
							if (true /*TODO: Set if player2 win game*/)
							{
								// TODO: Announce win condition during GameState.Pending2Turn && CueBall.HitState.Hit
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
		// TODO: reset game and ball positions after win condition
	}
}
