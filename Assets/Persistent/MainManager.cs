using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int chips;
    public double chipsMultiplier;
    public int playerCurrentHealth;
    public int playerMaxHealth;
    public int playerHandSize;
    public int nextEncounter;
    public List<CardObject> deck;
    public bool finalBattle;
    public bool map2;
    public bool miniboss;

    private void Awake()
    {
        map2 = false;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
