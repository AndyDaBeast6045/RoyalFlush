using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;
using Quaternion = UnityEngine.Quaternion;
using System.Numerics;


public class Map : MonoBehaviour
{
    public GameObject path;
    public Vector2[,] eventSpots = new Vector2[3, 10];
    public GameObject[] nodeTypes = new GameObject[7];
    private bool[,] madeNodes = new bool[3, 10];


    // Start is called before the first frame update
    void Start()
    {
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
                eventSpots[r, c].x += 1;

                int numNodes = 0;
                if (nodesInColumn(c) == 1)
                {
                    if (madeNodes[r, c] && madeNodes[r, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                    }
                    if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                    }
                    if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
                    }
                }
                if (nodesInColumn(c) == 2 && nodesInColumn(c + 1) == 3)
                {
                    if (madeNodes[r, c] && madeNodes[r, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                    }
                    if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                    }
                    if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
                    }
                }
                if (nodesInColumn(c) == 2 && nodesInColumn(c + 1) == 2)
                {
                    if (madeNodes[r, c] && madeNodes[r, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                        numNodes++;
                    }
                    if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                        numNodes++;
                    }
                    if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                    {
                        GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
                    }
                }
                else if (madeNodes[r, c] && madeNodes[r, c + 1])
                {
                    GameObject pathInstance = Instantiate(path, eventSpots[r, c], Quaternion.identity);
                }
                else if (madeNodes[r, c] && isInBounds(r - 1) && madeNodes[r - 1, c + 1])
                {
                    GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y + 1), transform.rotation * Quaternion.Euler(0f, 0f, 45f));
                }
                else if (madeNodes[r, c] && isInBounds(r + 1) && madeNodes[r + 1, c + 1])
                {
                    GameObject pathInstance = Instantiate(path, new Vector2(eventSpots[r, c].x, eventSpots[r, c].y - 1), transform.rotation * Quaternion.Euler(0f, 0f, -45f));
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
        Debug.Log(madeNodes[0, 1]);
        Debug.Log(madeNodes[0, 2]);
        Debug.Log(madeNodes[1, 0]);
        Debug.Log(madeNodes[1, 1]);
        Debug.Log(madeNodes[1, 2]);
        Debug.Log(madeNodes[2, 0]);
        Debug.Log(madeNodes[2, 1]);
        Debug.Log(madeNodes[2, 2]);
        */


    }


    // Update is called once per frame
    void Update()
    {

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

    public GameObject selectNodeType()
    {
        int index = Random.Range(0, 15);

        if (index < 10)
        {
            return nodeTypes[0];
        }
        else if (index == 10)
        {
            return nodeTypes[1];
        }
        else if (index == 11)
        {
            return nodeTypes[2];
        }
        else if (index == 12)
        {
            return nodeTypes[3];
        }
        else if (index == 13)
        {
            return nodeTypes[4];
        }
        else if (index == 14)
        {
            return nodeTypes[5];
        }

        return null;
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
        double baseX = transform.position.x + 1.65;
        double baseY = transform.position.y + 2;
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                eventSpots[r, c] = new Vector2((float) baseX,(float) baseY);
                baseY = baseY - 2;

            }
            baseX = baseX + 1.65;
            baseY = transform.position.y + 2;
        }
    }

    /*
     * Iterates through a 2d array of coordinates and instantiates events by first selecting a random number of events to create on a
     * column and then choosing a corresponding row based on the position of the previous column of events.
     */
    public void instantiateEvents()
    {
        int index = 1;
        //GameObject eventInstance = Instantiate(selectNodeType(), eventSpots[index, 0], Quaternion.identity);
        //madeNodes[1, 0] = true;
        bool[] previousNodes = new bool[] { false, true, false };
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
                numNodeSelector = Random.Range(1, 4);
            }

            if (numNodeSelector == 0) //Instantiate 1 node
            {
                int singleIndex = checkPossibleSingleNode(previousNodes);
                GameObject event1Instance = Instantiate(selectNodeType(), eventSpots[singleIndex, c], Quaternion.identity) as GameObject;

                previousNodes = new bool[] { false, false, false };
                previousNodes[singleIndex] = true;


            }
            else if (numNodeSelector == 1 || numNodeSelector == 2) //Instantiate 2 node
            {
                int[] ints = checkPossibleDoubleNode(previousNodes);
                GameObject event2Instance = Instantiate(selectNodeType(), eventSpots[ints[0], c], Quaternion.identity);
                GameObject eventI3nstance = Instantiate(selectNodeType(), eventSpots[ints[1], c], Quaternion.identity);

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
                        GameObject event4Instance = Instantiate(selectNodeType(), eventSpots[i, c], Quaternion.identity);
                    }
                    previousNodes = new bool[] { true, true, true }; //Not exactly what's happening but equivalent
                }
                else //If 3 nodes aren't possible, create 2
                {
                    int[] ints = checkPossibleDoubleNode(previousNodes);
                    GameObject event5Instance = Instantiate(selectNodeType(), eventSpots[ints[0], c], Quaternion.identity);
                    GameObject event6Instance = Instantiate(selectNodeType(), eventSpots[ints[1], c], Quaternion.identity);

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
        GameObject eventInstance3 = Instantiate(nodeTypes[6], new Vector2(2, -4), Quaternion.identity);

        
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
   
}
