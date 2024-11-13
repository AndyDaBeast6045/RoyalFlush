using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "Enemy/Encounter", order = 1)]
public class Encounter : ScriptableObject
{
    //Variables
    [SerializeField] private string encounterName;
    [SerializeField] private GameObject[] enemies;

    public string GetName()
    {
        return encounterName;
    }

    public GameObject[] GetEnemies()
    {
        return enemies;
    }
}
