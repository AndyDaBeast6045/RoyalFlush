using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartburnManager : PlayerHealth
{
    public int heartburn = 0;

        public void ReceiveHeartburn(int amount)
    {
        heartburn += amount;
    }

    public void TickHeartburn()
    {
        currentHealth -= heartburn * 2; //use damage script when done

        if (heartburn % 2 == 0) {

            heartburn /= 2; 
        } else { 

            heartburn = (heartburn - 1) / 2; 
        }


    }
}
