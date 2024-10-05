using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public Vector2[,] eventSpots = new Vector2[3, 8];
    public GameObject event1;

    // Start is called before the first frame update
    void Start()
    {
        int baseX = -7;
        int baseY = 2;
        for (int c = 0; c < 8; c++)
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
        for (int c = 0; c < 8; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                rand = Random.Range(1, 3); //Obviously terrible way to do this, but represents the basic idea.
                if (rand == 1)4
                {
                    GameObject eventInstance = Instantiate(event1, eventSpots[r, c], Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
