using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;
using System.Numerics;


public class Map : MonoBehaviour
{

    public Sprite playerSprite;
    public GameObject[,] madeEventSpots = new GameObject[3, 20];
    //public int[,] instanceIds = new int[3, 10];
    public bool[,,] findNextNodes = new bool[3, 10, 3];
    [SerializeField] public GameObject path;
    public Vector2[,] eventSpots = new Vector2[3, 10];
    public GameObject[] nodeTypes = new GameObject[9];

    private bool[,] madeNodes = new bool[3, 10];
    public Vector2[,] eventSpotsCopy = new Vector2[3, 10];

    public static int amountOfShops;
    public static int numOfEvents;
    public static int mapNum;
    
    void Start()
    {
        mapNum = 1;
        createMap();
    }

    // Start is called before the first frame update
    public void createMap()
    {
        amountOfShops = 0;
        numOfEvents = 0;
        //Creates a 3x9 grid of x and y coordinates to potentially spawn nodes on.
        setPossibleNodeCoords();

        //Chooses which spaces and events are used/created
        instantiateEvents();

        //Path test code:

        //Problem is that paths need to be instantiated starting at the parent object, not the first instantiated event space


        for (int c = 0; c < 9; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                eventSpots[r, c].x += .8f;

                //int numNodes = 0;
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

        //GameObject pathInstance = Instantiate(path, eventSpots[0, 0], Quaternion.identity);
        //GameObject pathInstance1 = Instantiate(path, new Vector2(eventSpots[1, 0].x, eventSpots[1, 0].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));

        /*
        Debug.Log(madeNodes[0, 0]);
        Debug.Log(madeNodes[1, 0]);
        Debug.Log(madeNodes[2, 0]);
        Debug.Log(madeNodes[0, 1]);
        Debug.Log(madeNodes[1, 1]);
        Debug.Log(madeNodes[2, 1]);
        Debug.Log(madeNodes[0, 2]);
        Debug.Log(madeNodes[1, 2]);
        Debug.Log(madeNodes[2, 2]);
        Debug.Log(madeNodes[0, 3]);
        Debug.Log(madeNodes[1, 3]);
        Debug.Log(madeNodes[2, 3]);
        Debug.Log(madeNodes[0, 4]);
        Debug.Log(madeNodes[1, 4]);
        Debug.Log(madeNodes[2, 4]);
        Debug.Log(madeNodes[0, 5]);
        Debug.Log(madeNodes[1, 5]);
        Debug.Log(madeNodes[2, 5]);
        */


        //Debug.Log("0: " +madeEventSpots[0, 0]);
        //Debug.Log("1: " +madeEventSpots[0, 1]);
        //Debug.Log("2: " +madeEventSpots[0, 2]);
        //Debug.Log("Row 0, Col 0; next col row 0? " + findNextNodes[0, 0, 0] + " next col row 1? " + findNextNodes[0, 0, 1]);
        /*
        Debug.Log(madeNodes[0, 0]);
        Debug.Log(madeNodes[1, 0]);
        Debug.Log(madeNodes[2, 0]);
        Debug.Log(madeNodes[0, 1]);
        Debug.Log(madeNodes[1, 1]);
        Debug.Log(madeNodes[2, 1]);
        Debug.Log(madeNodes[0, 2]);
        Debug.Log(madeNodes[1, 2]);
        Debug.Log(madeNodes[2, 2]);
        */


        for (int r = 0; r < 3; r++)
        {
             if (madeNodes[r, 0])
             {
                //Debug.Log(r);
                madeEventSpots[r, 0].GetComponent<BoxCollider2D>().enabled = true;
                //madeEventSpots[r, 0].GetComponent<BoxCollider2D>().enabled = true;
             }
        }
        //Map is Done!
        mapNum++;

        //Proves madeNodes and madeEventSpots match
        /*
        //madeNodes[0, 9] = true;
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                if (madeNodes[r, c] == true && madeEventSpots[r, c] != null)
                {
                    Debug.Log("true");
                }
                else if (madeNodes[r, c] == false && madeEventSpots[r, c] == null)
                {
                    Debug.Log("true");
                }
                else
                {
                    Debug.Log("false");
                }
            }
        }
        */
        


    }

    /*
     * Selects a type of event based off random chance, with battles being far more likely. Change index and corresponding ifs to
     * manipulate probability.
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

    public GameObject selectNodeType(int c)
    {
        if (c == 8 && amountOfShops < 2)
        {
            Debug.Log(amountOfShops);
            return nodeTypes[7];
        }
        
        int index = Random.Range(0, 17);
        if (c == 1 || c== 2)
        {
            index = Random.Range(0, 14);
        }
        if ((c == 6 || c ==7) && numOfEvents < 4)
        {
            index = Random.Range(11, 16);
        }
        
        if (index < 10)
        {
            return nodeTypes[0];
        }
        else if (index == 10)
        {
            numOfEvents++;
            return nodeTypes[1];
        }
        else if (index == 11)
        {
            numOfEvents++;
            return nodeTypes[2];
        }
        else if (index == 12)
        {
            numOfEvents++;
            return nodeTypes[3];
        }
        else if (index == 13)
        {
            numOfEvents++;
            return nodeTypes[4];
        }
        else if (index == 14)
        {
            numOfEvents++;
            return nodeTypes[5];
        }
        else
        {
            if (amountOfShops < 2)
            {
                amountOfShops++;
                return nodeTypes[7];
            }
            return nodeTypes[1];
        }
        
    }

    /*
     * Checks to make sure that previous nodes have a path to a new sets of double nodes and returns an array of qualifying indexes.
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
    public void instantiateEvents()
    {
        //GameObject eventInstance = Instantiate(selectNodeType(), eventSpots[index, 0], Quaternion.identity);
        //madeNodes[1, 0] = true;
        bool[] previousNodes = new bool[] { false, true, false };
        GameObject spawnedEvent;
        //Above was changed.

        for (int c = 0; c < 9; c++)
        {
            //Creates a boolean 2d array of spawned events
            /*
            for (int i = 0; i < 3; i++)
            {
                //c-1
                madeNodes[i, c] = previousNodes[i];
            }
            */

            //index = Random.Range(0, 3);

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

            //Debug.Log(previousNodes[0]);
            //Debug.Log(previousNodes[1]);
            //Debug.Log(previousNodes[2]);
            for (int i = 0; i < 3; i++)
            {
                //c-1
                madeNodes[i, c] = previousNodes[i];
            }
            

        }

        //Creates the boss event node
        if (mapNum == 1)
        {
            spawnedEvent = Instantiate(nodeTypes[6], eventSpots[1, 9], Quaternion.identity);
        } else
        {
            spawnedEvent = Instantiate(nodeTypes[8], eventSpots[1, 9], Quaternion.identity);
        }
        
        madeEventSpots[1, 9] = spawnedEvent;

        
        for (int i = 0; i < 3; i++)
        {
            madeNodes[i, 8] = previousNodes[i];
        }
        madeNodes[1, 9] = true;
        
    }
    /*
     * retired methods that might be of use later.
    public bool[] checkPossibleNodes (int index, bool[] possibleNodes)
    {
        if (index == 0)
        {
            possibleNodes[0] = true;
            possibleNodes[1] = true;
        }
        if (index == 1)
        {
            for (int i = 0; i < possibleNodes.Length; i++)
            {
                possibleNodes[i] = true;
            }
        }
        if (index == 2)
        {
            possibleNodes[1] = true;
            possibleNodes[2] = true;
        }

        return possibleNodes;
    }
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
     * Use Vector2 eventNodes to cross reference event transform.positions to get r and c values. 
     * 
     * 
     */
    /*
     * Pass in the row and column of the player's current node and next available nodes will
     * become clickable.
     * (int r, int c)
     */
    public void manageNodes(float xValue, float yValue)
    {
        Debug.Log("called");
        /*
        if (madeEventSpots[2, 0] != null)
        {
            Debug.Log("true");
        }
        */

        setPossibleNodeCoords();

        //Needed because of offset because of the difference between eventSpots[] values and actual coords
        //xValue = xValue + 6.85f;
        //yValue = yValue - 2f;

        int row = -1;
        int col = -1;

        //int idValue = GetInstanceID();

        //Debug.Log("ID: " + idValue);
        //Debug.Log("ID: " + madeEventSpots[0, 0].GetInstanceID());
        //Debug.Log("x: " + xValue);
        //Debug.Log("y: " + yValue);
        //Debug.Log("Array x: " + eventSpots[2, 0].x);
        //Debug.Log("Array y: " + eventSpots[2, 0].y);

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

        Debug.Log("Row: " + row);
        Debug.Log("Col: " + col);

        for (int p = 0; p < 3; p++)
        {
            if (findNextNodes[row, col, p])
            {
                Debug.Log("Made row " + p + ", col " + (col + 1) + "hitbox");
                madeEventSpots[p, col + 1].GetComponent<BoxCollider2D>().enabled = true;
            }
            if (madeNodes[p, col])
            {
                madeEventSpots[p, col].GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        //madeEventSpots[row, col].GetComponent<SpriteRenderer>().sprite = playerSprite;
    

        /*
        for (int c = 0; c < 9; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                if (instanceIds[r, c] == idValue)
                {
                    for (int p = 0; p < 3; p++)
                    {
                        if (findNextNodes[r, c, p])
                        {
                            madeEventSpots[p, c + 1].GetComponent<BoxCollider2D>().enabled = true;
                        }
                        madeEventSpots[p, c].GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
            }
        }
        */

    }
}
