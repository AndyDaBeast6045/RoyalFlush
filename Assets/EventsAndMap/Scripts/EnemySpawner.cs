using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class EnemySpawner : MonoBehaviour
{
    public int difficultyLevel = 3;
    public int difficultyMult = 20;

    public GameObject[] enemies = new GameObject[10];
    public GameObject[] spawnSpaces = new GameObject[5];
    private bool[] fullSpots = new bool[] { false, false, false, false, false };

    // Start is called before the first frame update
    void Start()
    {
        //Testing code
        //Vector3 spot = new Vector2(spawnSpaces[0].transform.position.x, spawnSpaces[0].transform.position.y);
        //GameObject cardsEnemyInstance = Instantiate(enemies[5], spot, Quaternion.identity);
        spawnEnemies(difficultyLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Spawns # of enemies and types of enemies determined by a provided difficultyLevel parameter. Each enemy has a 10% chance to spawn, with
     * tougher enemies being locked behind a difficultyTotal threshold. difficultyTotal is calculated by multiplying the difficultyLevel by a
     * difficultyMult, currently defined as 20. From there, each time an enemy is spawned on a random spawn point, the difficultyTotal is subtracted
     * by 10-30 points depending on the percieved strength of the enemy. Once difficultyTotal < 0 or the required # of enemies has been spawned,
     * the method is done.
     * 
     * Side note: this was breaking (infinite loops) everytime I put "5" as the difficultyLevel and occasionally with lower levels as well. I do not know how I
     * managed to fix it so be wary when changing things around.
     */
    public void spawnEnemies(int difficultyLevel)
    {
        int count = 0;
        //Determines the number of enemies that will be spawned based on a passed-in "difficulty level" 1-5
        int numOfEnemies = difficultyLevel + Random.Range(-1, 1);

        if (numOfEnemies < 1)
        {
            numOfEnemies = 1;
        }
        else if (numOfEnemies > 5)
        {
            numOfEnemies = 5;
        }

        int difficultyTotal = difficultyLevel * difficultyMult; //3 enemies would have 60

        while (difficultyTotal > 0 && numOfEnemies > 0)
        {
            int range = Random.Range(1, 100);
            int chosenSpot = getEmptySpot();
            Vector2 spot = new Vector2(spawnSpaces[chosenSpot].transform.position.x, spawnSpaces[chosenSpot].transform.position.y);
            count++;

            /*
             * Mulligan: if difficultyLevel is too low to trigger enemies besides card or dice, spawns one of them without
             * relying on int range.
             */
            if (difficultyTotal <= 10)
            {
                int rand = Random.Range(1, 2);
                if (rand == 1)
                {
                    GameObject cardsEnemyInstance = Instantiate(enemies[0], spot, Quaternion.identity);
                    difficultyTotal = difficultyTotal - 10;
                }
                else
                {
                    GameObject diceEnemyInstance = Instantiate(enemies[1], spot, Quaternion.identity);
                    difficultyTotal = difficultyTotal - 10;
                }
                range = -1;

            }

            if (range <= 10 && range > 0)
            {
                GameObject cardsEnemyInstance = Instantiate(enemies[0], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 10;
            }

            if (range <= 20 && range > 10)
            {
                GameObject diceEnemyInstance = Instantiate(enemies[1], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 10;
            }

            if (difficultyTotal >= 15 && range <= 30 && range > 20)
            {
                GameObject goblinEnemyInstance = Instantiate(enemies[3], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 15;
            }

            if (difficultyTotal >= 20 && range <= 40 && range > 30)
            {
                GameObject pachinkoEnemyInstance = Instantiate(enemies[4], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 20;
            }

            if (difficultyTotal >= 20 && range <= 50 && range > 40)
            {
                GameObject slotsEnemyInstance = Instantiate(enemies[5], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 20;
            }

            if (difficultyTotal >= 25 && range <= 60 && range > 50)
            {
                GameObject rouletteEnemyInstance = Instantiate(enemies[6], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 20;
            }

            if (difficultyTotal >= 25 && range <= 70 && range > 60)
            {
                GameObject horseEnemyInstance = Instantiate(enemies[7], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 25;
            }

            if (difficultyTotal >= 25 && range <= 80 && range > 70)
            {
                GameObject mahjongEnemyInstance = Instantiate(enemies[8], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 25;
            }

            if (difficultyTotal >= 30 && range <= 90 && range > 80)
            {
                GameObject gachaponnemyInstance = Instantiate(enemies[9], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 30;
            }

            if (range > 90) //Maybe a lower chance?
            {
                GameObject slimeEnemyInstance = Instantiate(enemies[1], spot, Quaternion.identity);
                difficultyTotal = difficultyTotal - 5;
            }

            fullSpots[chosenSpot] = true;
            numOfEnemies--;



        }
        Debug.Log(count);

        if (difficultyTotal > 10)
        {
            makeStrongEnemy();
        }

    }

    public void makeStrongEnemy()
    {
        //Multiplies enemy stats by 1.25. Probably need to wait till enemies are done to implement.
        Debug.Log("One strong boi");

    }


    /*
     * Repeatedly picks a random spot in the boolean array fullSpots until it finds and returns the index of an empty spot.
     */
    public int getEmptySpot()
    {
        int chosenSpot;
        do
        {
            chosenSpot = Random.Range(0, 5);
        } while (fullSpots[chosenSpot] == true);
        return chosenSpot;
    }

}
