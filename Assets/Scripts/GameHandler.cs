using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {

	enum GameState { AwaitingStart, Player1Turn, Player2Turn, Pending1Turn, Pending2Turn };

	public CueBall cueBall;		// might switch type to CueBall
	public GameObject ballGroup;
    public Text stateText;
    public Text resultText;

	GameState gameState;
	public bool allBallsStopMoving;	// change to private

    public Collider roofCollider;
    public Collider ballRangeCollider;

	// Use this for initialization
	void Start ()
	{
		// initialize variables

		gameState = GameState.AwaitingStart;
		allBallsStopMoving = true;

        stateText.text = "Waiting game to start...";
        resultText.text = "";
	}

	// Update is called once per frame
	void Update ()
	{
		//print(gameState);
		allBallsStopMoving = CheckAllBallsNotMoving();

		// gameState changes
		switch (gameState)
		{
			case GameState.AwaitingStart:

				cueBall.GetComponent<CueBall> ().hitState = CueBall.HitState.Idle;
                
                // disable all ball interactions with cue during GameState.AwaitingStart
                //done	

                //cueStick.GetComponent<CueStick> ().m_disableCollider ();

                if (true /*confirm start game*/)
				{
					gameState = GameState.Player1Turn;
                    stateText.text = "Player 1's turn";
                }
				break;

			case GameState.Player1Turn:

                roofCollider.enabled = false;
				cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Before;
                
                if (cueBall.GetComponent<MeshRenderer>().enabled == false)	// cue ball is pocketed by previous player
				{
                    //cueBall.transform.position = cueBall.GetComponent<CueBall>().startingPosition; // TODO: change to another player holding the ball
                    cueBall.transform.parent = cueBall.GetComponent<CueBall>().controllerParent;
                    cueBall.transform.localPosition = new Vector3(0, 0, 0);
                    cueBall.GetComponent<Rigidbody>().isKinematic = true;
                    cueBall.GetComponent<MeshRenderer>().enabled = true;
                    cueBall.GetComponent<Collider>().enabled = true ;
                    cueBall.GetComponent<Collider>().isTrigger = true;
                    ballRangeCollider.enabled = false;
                }

				// enable collision between cue and cueBall during GameState.Player1Turn
				//done
				//cueStick.GetComponent<CueStick>().m_enableCollider();	// WTF

				//if (cueStick.GetComponent<CueStick>().hittedCueBall()) //TODO: check if cue hits cue ball: done
				if (cueBall.GetComponent<CueBall>().hitCue == true)
				{
					gameState = GameState.Pending1Turn;
					cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Miss;
					allBallsStopMoving = false;
				}
				break;

			case GameState.Pending1Turn:

                roofCollider.enabled = true;
				// check if all balls stop moving during GameState.Pending1Turn

				if (allBallsStopMoving)
				{
					//adding
					Ball[] nine_Ball = ballGroup.GetComponentsInChildren<Ball>();
					bool nineBallPocketed = nine_Ball[8].GetComponent<Ball>().isPocketed;   // TODO: change to another player holding the ball
					bool cueNotPocketed = true;
					if (cueBall.GetComponent<CueBall>().hitState == CueBall.HitState.Foul)
					{
						cueNotPocketed = false;
					}
					//end adding

					switch ((cueBall.GetComponent<CueBall>().hitState))
					{
					case CueBall.HitState.HitPocketed:							
							if (nineBallPocketed & cueNotPocketed) /* Set if player1 win game*/
							{
								// Announce win condition during GameState.Pending1Turn && CueBall.HitState.Hit
								print("player 1 wins!");
                                stateText.text = "Game ended";
                                resultText.text = "Player 1 wins!";
                                ResetGame();
								gameState = GameState.AwaitingStart;												
							}
							else
							{
                                if (nineBallPocketed)
                                {
                                    nine_Ball[8].ReturnStartingPosition();
                                }
                                gameState = GameState.Player1Turn;
                                stateText.text = "Player 1's turn";
                                UpdateResult("Player 1 continues");
                            }

							break;

						case CueBall.HitState.Hit:
							gameState = GameState.Player1Turn;
                            stateText.text = "Player 1's turn";
                            resultText.text = "Player 1 continues";
                            break;

						case CueBall.HitState.Miss:
							gameState = GameState.Player2Turn;
                            stateText.text = "Player 2's turn";
                            resultText.text = "Player 1 did not hit any ball\nPlayer 2's turn";
                            break;

						case CueBall.HitState.Foul:
							gameState = GameState.Player2Turn;

							if (nineBallPocketed)
							{
								nine_Ball[8].ReturnStartingPosition();
							}

                            stateText.text = "Player 2's turn";
                            resultText.text = "Player 1 fouled\nPlayer 2 can put cue ball anywhere";
                            // Set condition where player 2 can place cueBall during GameState.Pending1Turn && CueBall.HitState.Foul
                            break;

						default:
							break;
					}
					cueBall.GetComponent<CueBall>().hitCue = false;
				}
				
				break;

			case GameState.Player2Turn:
                roofCollider.enabled = false;
				cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Before;

				if (cueBall.GetComponent<MeshRenderer>().enabled == false)      // cue ball is pocketed by previous player
				{
                    //cueBall.transform.position = cueBall.GetComponent<CueBall>().startingPosition; // TODO: change to another player holding the ball
                    cueBall.transform.parent = cueBall.GetComponent<CueBall>().controllerParent;
                    cueBall.transform.localPosition = new Vector3(0, 0, 0);
                    cueBall.GetComponent<Rigidbody>().isKinematic = true;
					cueBall.GetComponent<MeshRenderer>().enabled = true;
                    cueBall.GetComponent<Collider>().enabled = true;
                    cueBall.GetComponent<Collider>().isTrigger = true;
                    ballRangeCollider.enabled = false;

                }

                //enable collision between cue and cueBall during GameState.Player2Turn
                //done
                //cueStick.GetComponent<CueStick> ().m_enableCollider ();
                if (cueBall.GetComponent<CueBall>().hitCue == true) //done TODO: check cue hits cue ball
				{
					gameState = GameState.Pending2Turn;
					cueBall.GetComponent<CueBall>().hitState = CueBall.HitState.Miss;
					allBallsStopMoving = false;
				}
				break;

			case GameState.Pending2Turn:
                roofCollider.enabled = true;
				// check if all balls stop moving during GameState.Pending2Turn

				if (allBallsStopMoving)
				{
					//adding
					Ball[] nine_Ball = ballGroup.GetComponentsInChildren<Ball>();
					bool nineBallPocketed = nine_Ball[8].GetComponent<Ball>().isPocketed;
					bool cueNotPocketed = true;
					if (cueBall.GetComponent<CueBall>().hitState == CueBall.HitState.Foul)
					{
						cueNotPocketed = false;
					}
					//end adding

					switch ((cueBall.GetComponent<CueBall>().hitState))
					{

						case CueBall.HitState.HitPocketed:
							if (nineBallPocketed & cueNotPocketed) /*Set if player2 win game*/
							{
								// Announce win condition during GameState.Pending2Turn && CueBall.HitState.Hit
								//done
								print("player 2 wins!");
                                stateText.text = "Game ended";
                                resultText.text = "Player 2 wins!";
                                ResetGame();
								gameState = GameState.AwaitingStart;
							}
							else
							{
								if (nineBallPocketed)
								{
									nine_Ball[8].ReturnStartingPosition();
								}
								gameState = GameState.Player2Turn;
                                stateText.text = "Player 2's turn";
                                UpdateResult("Player 2 continues");

                            }
							break;

						case CueBall.HitState.Hit:
							gameState = GameState.Player2Turn;
                            stateText.text = "Player 2's turn";
                            UpdateResult("Player 2 continues");
                            break;

						case CueBall.HitState.Miss:
							gameState = GameState.Player1Turn;
                            stateText.text = "Player 1's turn";
                            UpdateResult("Player 2 did not hit any ball\nPlayer 1's turn");
                            break;

						case CueBall.HitState.Foul:
							gameState = GameState.Player1Turn;

							if (nineBallPocketed)
							{
								nine_Ball[8].ReturnStartingPosition();
							}

							stateText.text = "Player 1's turn";
                            UpdateResult("Player 2 fouled\nPlayer 1 can put cue ball anywhere");
                            // Set condition where player 1 can place cueBall during GameState.Pending2Turn && CueBall.HitState.Foul
                            break;

						default:
							break;
					}
					cueBall.GetComponent<CueBall>().hitCue = false;

				}
				break;
		}

		
	}

	void ResetGame()
	{
		//define starting position in ball.cs?

		// reset game and ball positions after win condition
		Ball[] all9balls = ballGroup.GetComponentsInChildren<Ball>();
		foreach (Ball ball in all9balls){
			ball.GetComponent<Ball> ().ReturnStartingPosition();
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
		}
		cueBall.GetComponent<CueBall> ().returnStartingPosition();
		gameState = GameState.AwaitingStart;
	}

	bool CheckAllBallsNotMoving()
	{
		bool result = true;

		if (cueBall.GetComponent<Rigidbody>().velocity.magnitude > 0.7 && cueBall.GetComponent<MeshRenderer>().enabled == true)
		{
			result = false;
		}
		//else
		//{
		//	cueBall.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
		//}

		for (int i = 0; i < ballGroup.transform.childCount; i++)
		{
			//print(i + ", " + ballGroup.transform.GetChild(i).GetComponent<Rigidbody>().velocity.magnitude);
			if (ballGroup.transform.GetChild(i).GetComponent<Rigidbody>().velocity.magnitude > 0.7 && ballGroup.transform.GetChild(i).GetComponent<MeshRenderer>().enabled == true)
			{
				result = false;
			}
			//else
			//{
			//	ballGroup.transform.GetChild(i).GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
			//}
		}
		return result;
	}

    IEnumerator UpdateResult(string text)
    {
        resultText.text = text;
        yield return new WaitForSeconds(3);
        resultText.text = "";
    }
}
