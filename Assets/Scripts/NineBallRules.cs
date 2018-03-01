using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NineBallRules : MonoBehaviour {

    public int firstBallHit = -1; // the first ball the player hit
    private int lowestNumberBall = 1; // the ball the player should hit first
    public List<int> ballPocketed = new List<int>(); // ball pocketed in last hit
    public List<int> ballOnTable = new List<int>(); // balls still on table
    private bool miss = false; // if true, change player
    private bool foul = false; // if true, change player and opponent can place cue ball anywhere

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void hitCheck()
    {
        firstBallHit = // which ball collider trigger with cue ball first


        if ()
        { // holeCollider collides with any ball
          //remove the ball from scene
            ballPocketed.Add(); // add the ball number to this list
        }

        //stops until all ball stop moving
        ruleCheck();
    }

    void ruleCheck()
    {
        // Check if the player hit the lowestNumberBall 
        if (firstBallHit != lowestNumberBall)
            foul = true;

        // Check if player pockets any ball
        if (ballPocketed.Count == 0)
            miss = true;
        else if (ballPocketed.Contains(0))
        {// if pocketed, check whether the cue ball is pocketed
            foul = true;
            ballPocketed.Remove(0);
        }

        // Check if 9 ball is pocketed
        if (ballPocketed.Contains(9))
        {
            if (foul)
            { // if pocketed, but player foul
              // remove all ballPocketed from ballOnTable except 9
                ballPocketed.Remove(9);
                foreach (int number in ballPocketed)
                    ballOnTable.Remove(number);
                // place 9 ball on foot spot
                // if another is on foot spot, put as near to it as possible
            }
            else // if 9 ball pocketed and player did not foul, the player win the game
                win;
        }
        else
        {
            // remove all ballPocketed from ballOnTable
            foreach (int number in ballPocketed)
                ballOnTable.Remove(number);
        }

        // find the smallest number in ballOnTable, assign the number to lowestNumberBall
        ballOnTable.Sort();
        lowestNumberBall = ballOnTable[0];

        // reset for next play
        ballOnTable.Clear();
        firstBallHit = -1;
    }
}
