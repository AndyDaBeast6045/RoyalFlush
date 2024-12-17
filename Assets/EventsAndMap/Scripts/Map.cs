using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;
using System.Numerics;


public class Map : MonoBehaviour
{
    [SerializeField] public GameObject path;

    //Random arrays used throughout
    public GameObject[,] madeEventSpots = new GameObject[3, 20];
    public bool[,,] findNextNodes = new bool[3, 10, 3];
    public Vector2[,] eventSpots = new Vector2[3, 10];
    public GameObject[] nodeTypes = new GameObject[9];
    private bool[,] madeNodes = new bool[3, 10];

    public GameObject fadeIn;
    public GameObject fadeObject;

    public static int amountOfShops;
    public static int numOfEvents;
    public static int mapNum;
    
    void Start()
    {
        Debug.Log(MainManager.Instance.map2);
        //MainManager.Instance.map2 = true;
        if (MainManager.Instance.map2) {
            SceneManager.UnloadSceneAsync("Combat");
            fadeIn.SetActive(true);
            createMap(nodeTypes[8]);
            fadeObject.GetComponent<FadeScript>().fadeInB = true;
        } else {
            createMap(nodeTypes[6]);
        }
        MainManager.Instance.map2 = true;
    }

    // Start is called before the first frame update
    public void createMap(GameObject currentMapBoss)
    {
        amountOfShops = 0;
        numOfEvents = 0;
        //Creates a 3x9 grid of x and y coordinates to potentially spawn nodes on. 
        setPossibleNodeCoords();

        //Chooses which spaces and events are used/created
        instantiateEvents(currentMapBoss);


        //Creates path prefabs between nodes
        for (int c = 0; c < 9; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                //Spacing between nodes / 2
                eventSpots[r, c].x += .8f;

                if (nodesInColumn(c) == 1)
                {
                    if (madeNodes[r, c] && madeNodes[r, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                        findNextNodes[r, c, r] = true;
                    }
                    if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                        findNextNodes[r, c, r - 1] = true;
                    }
                    if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
                        findNextNodes[r, c, r + 1] = true;
                    }
                }
                if (nodesInColumn(c) == 2 && nodesInColumn(c + 1) == 3)
                {
                    if (madeNodes[r, c] && madeNodes[r, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                        findNextNodes[r, c, r] = true;
                    }
                    if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                        findNextNodes[r, c, r - 1] = true;
                    }
                    if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
                        findNextNodes[r, c, r + 1] = true;
                    }
                }
                if (nodesInColumn(c) == 2 && nodesInColumn(c + 1) == 2)
                {
                    if (madeNodes[r, c] && madeNodes[r, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                        findNextNodes[r, c, r] = true;
                    }
                    if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                        findNextNodes[r, c, r - 1] = true;
                    }
                    if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
                        findNextNodes[r, c, r + 1] = true;
                    }
                }
                else if (madeNodes[r, c] && madeNodes[r, c + 1])
                {
                    GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                    findNextNodes[r, c, r] = true;
                }
                else if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                {
                    GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                    findNextNodes[r, c, r - 1] = true;
                }
                else if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                {
                    GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
                    findNextNodes[r, c, r + 1] = true;
                }
                

            }
        }


        //Spawns paths from the start node.
        if (madeNodes[0, 0])
        {
            GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[0, 0].x - 2, eventSpots[0, 0].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
        }
        if (madeNodes[1, 0])
        {
            GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[1, 0].x - 2, eventSpots[1, 0].y), Quaternion.identity);
        }
        if (madeNodes[2, 0])
        {
            GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[2, 0].x - 2, eventSpots[2, 0].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
        }

        //Makes the first column of nodes clickable
        for (int r = 0; r < 3; r++)
        {
             if (madeNodes[r, 0])
             {
                madeEventSpots[r, 0].GetComponent<BoxCollider2D>().enabled = true;
             }
        }
        //Map is Done! Incremenet map num by 1
        mapNum++;

    }


     /*
     * Returns the num of made nodes in the provided column c.
     */
    public int nodesInColumn(int c)
    {
        int nodesInCol = 0;
        for (int i = 0; i < 3; i++)
        {
            if (madeNodes[i, c])
            {
                nodesInCol++;
            }
        }
        return nodesInCol;
    }

    /*
    * Selects a type of event based off random chance, with battles being far more likely. Change index and corresponding ifs to
    * manipulate probability.
    */
    public GameObject selectNodeType(int c)
    {
        if (c == 8 && amountOfShops < 2)
        {
            Debug.Log(amountOfShops);
            return nodeTypes[7];
        }
        
        int index = Random.Range(0, 17);
        if (numOfEvents > 6) //Chooses battles if there are too many event nodes
        {
            index = 1;
        }

        if (c < 5) //Stops shops from spawning in the first half of the map
        {
            index = Random.Range(0, 14);
        }

        if ((c == 6 || c ==7) && numOfEvents < 4) //Chooses events in the latter half of the map if there are barely any
        {
            index = Random.Range(11, 16);
        }
        
        if (index < 10) //Returns battle 
        {
            return nodeTypes[0];
        }
        else if (index == 10) //Returns Roulette event
        {
            numOfEvents++;
            return nodeTypes[1];
        }
        else if (index == 11) //Returns ManInSuit event
        {
            numOfEvents++;
            return nodeTypes[2];
        }
        else if (index == 12) //Returns SnackBar event
        {
            numOfEvents++;
            return nodeTypes[3];
        }
        else if (index == 13) //Returns Bathroom event
        {
            numOfEvents++;
            return nodeTypes[4];
        }
        else if (index == 14) //Returns Bouncer event
        {
            numOfEvents++;
            return nodeTypes[5];
        }
        else
        {
            if (amountOfShops < 2) //Spawns shops at end if less than two have already spawned
            {
                amountOfShops++;
                return nodeTypes[7];
            }
            return nodeTypes[1];
        }
        
    }

    /*
     * Checks to make sure that previous nodes have a path to a new sets of double nodes and returns an array of qualifying indexes.
     * Used for spawning 2 nodes in the next column.
     */
    public int[] checkPossibleDoubleNode(bool[] previousNodes)
    {
        int nodeCount = 0;
        int index;
        if (previousNodes[0] && !previousNodes[1] && !previousNodes[2])
        {
            return new int[] {0, 1};
        }
        if (previousNodes[2] && !previousNodes[0] && !previousNodes[1])
        {
            return new int[] {1, 2};
        }

        int temp = -1;
        int[] outs = new int[2];
        int i = 0;
        do
        {
            index = Random.Range(0, 3);
            if (temp != index)
            {
                outs[i] = index;
                nodeCount++;
                i++;
            }
            temp = index;
        } while (nodeCount < 2);

        return outs;
    }

    /*
     * Checks to make sure that previous nodes have a path to a new single node and returns a qualifying index.
     */
    public int checkPossibleSingleNode(bool[] previousNodes)
    {
        int index;
        if (previousNodes[0] && previousNodes[2])
        {
            return 1;
        }
        if (previousNodes[0] && previousNodes[1])
        {
            index = Random.Range(0, 2);
            return index;
        }
        if (previousNodes[1] && previousNodes[2])
        {
            index = Random.Range(1, 3);
            return index;
        }
        if (previousNodes[1])
        {
            index = Random.Range(0, 3);
            return index;
        }
        return 1;
    }

    /*
     * Creates a 2d array of x and y coordinates on which to potentially spawn events.
     */
    public void setPossibleNodeCoords()
    { // +2
        double baseX = transform.position.x + 1.6;
        double baseY = transform.position.y + 2;
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                eventSpots[r, c] = new Vector2((float)baseX, (float)baseY);
                baseY = baseY - 2;

            }
            baseX = baseX + 1.6;
            baseY = transform.position.y + 2;
        }
    }

    /*
     * Iterates through a 2d array of coordinates and instantiates events by first selecting a random number of events to create on a
     * column and then choosing a corresponding row based on the position of the previous column of events.
     */
    public void instantiateEvents(GameObject currentMapBoss)
    {
        //Represents the presence of nodes in the previous column
        bool[] previousNodes = new bool[] { false, true, false };
        GameObject spawnedEvent;

        for (int c = 0; c < 9; c++)
        {
            int numNodeSelector;
            if (c != 0)
            {
                numNodeSelector = Random.Range(0, 4);
            } else
            {
                numNodeSelector = 3;
            }

            if (numNodeSelector == 0) //Instantiate 1 node
            {
                int singleIndex = checkPossibleSingleNode(previousNodes);
                spawnedEvent = Instantiate(selectNodeType(c), eventSpots[singleIndex, c], Quaternion.identity);
                madeEventSpots[singleIndex, c] = spawnedEvent;

                previousNodes = new bool[] { false, false, false };
                previousNodes[singleIndex] = true;


            }
            else if (numNodeSelector == 1 || numNodeSelector == 2) //Instantiate 2 node
            {
                int[] ints = checkPossibleDoubleNode(previousNodes);
                Debug.Log("Should be different: " + ints[0] + " and " + ints[1]);
                spawnedEvent = Instantiate(selectNodeType(c), eventSpots[ints[0], c], Quaternion.identity);
                madeEventSpots[ints[0], c] = spawnedEvent;
                spawnedEvent = Instantiate(selectNodeType(c), eventSpots[ints[1], c], Quaternion.identity);
                madeEventSpots[ints[1], c] = spawnedEvent;

                previousNodes = new bool[] { false, false, false };
                previousNodes[ints[0]] = true;
                previousNodes[ints[1]] = true;

            }
            else if (numNodeSelector == 3) //Instantiate 3 node (only if nodes are reachable)
            {
                if (previousNodes[1] || (previousNodes[0] && previousNodes[2]))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        spawnedEvent = Instantiate(selectNodeType(c), eventSpots[i, c], Quaternion.identity);
                        madeEventSpots[i, c] = spawnedEvent;
                    }
                    previousNodes = new bool[] { true, true, true }; //Not exactly what's happening but equivalent
                }
                else //If 3 nodes aren't possible, create 2
                {
                    int[] ints = checkPossibleDoubleNode(previousNodes);
                    Debug.Log("Should be different2: " +ints[0]+ " and " + ints[1]);
                    spawnedEvent = Instantiate(selectNodeType(c), eventSpots[ints[0], c], Quaternion.identity);
                    madeEventSpots[ints[0], c] = spawnedEvent;
                    spawnedEvent = Instantiate(selectNodeType(c), eventSpots[ints[1], c], Quaternion.identity);
                    madeEventSpots[ints[1], c] = spawnedEvent;

                    previousNodes = new bool[] { false, false, false };
                    previousNodes[ints[0]] = true;
                    previousNodes[ints[1]] = true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                madeNodes[i, c] = previousNodes[i];
            }
            

        }

        //Creates the boss event node
        
        spawnedEvent = Instantiate(currentMapBoss, eventSpots[1, 9], Quaternion.identity);    
        
        madeEventSpots[1, 9] = spawnedEvent;


        //MAkes the boss event and previious column appear in madeNodes
        for (int i = 0; i < 3; i++)
        {
            madeNodes[i, 8] = previousNodes[i];
        }
        madeNodes[1, 9] = true;
        
    }

    /*
     * Returns whether a given int is between 0 and 2.
     * Used when spawning nodes to make sure a potential value for the row is inbounds.
     */
    public bool isInBounds(int index)
    {
        if (index > 2 || index < 0)
        {
            return false;
        } else
        {
            return true;
        }
    }

    /*
     * Pass in the row and column of the player's current node and next available nodes will
     * become clickable.
     * (int r, int c)
     */
    public void manageNodes(float xValue, float yValue)
    {
        //Breaks if its not there, blame me for calling it from a different script
        setPossibleNodeCoords();

        int row = -1;
        int col = -1;

        //Finds the currently clicked on node by comparing its x and y coordinates to ever x and y coord store
        //in eventSpots, which is made by calling setPossibleNodeCoords().
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                if (eventSpots[r, c].x == xValue && eventSpots[r, c].y == yValue)
                {
                    Debug.Log("found r and c: " +r+ ", " +c);
                    col = c;
                    row = r;
                }
            }
        }

        //Makes the hitboxes of the nodes in the next column that are accessible from the current node clickable
        for (int p = 0; p < 3; p++)
        {
            if (findNextNodes[row, col, p])
            {
                //Debug.Log("Made row " + p + ", col " + (col + 1) + "hitbox");
                madeEventSpots[p, col + 1].GetComponent<BoxCollider2D>().enabled = true;
            }
            if (madeNodes[p, col]) //Turns off hitboxes in the current column
            {
                madeEventSpots[p, col].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
