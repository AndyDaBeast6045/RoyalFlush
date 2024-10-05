using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public Vector2[,] eventSpots = new Vector2[3, 9];
    public GameObject[] nodeTypes = new GameObject[7];
    

    // Start is called before the first frame update
    void Start()
    {
        int baseX = (int) transform.position.x + 2;
        int baseY = (int) transform.position.y + 2;
        for (int c = 0; c < 9 ; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                eventSpots[r, c] = new Vector2(baseX, baseY);
                baseY = baseY - 2;

            }
            baseX = baseX + 2;
            baseY = 2;
        }


        int rand;
        for (int c = 0; c < 9; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                //rand = Random.Range(1, 3); //Obviously terrible way to do this, but represents the basic idea.
                /*
                if (rand == 1)
                {
                    GameObject eventInstance = Instantiate(nodeTypes[0], eventSpots[r, c], Quaternion.identity);
                }
                */
                GameObject eventInstance = Instantiate(nodeTypes[0], eventSpots[r, c], Quaternion.identity);
            }
        }
        GameObject bossInstance = Instantiate(nodeTypes[6], new Vector2(baseX, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}