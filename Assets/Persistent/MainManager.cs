using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int chips;
    public int playerCurrentHealth;
    public int playerMaxHealth;
    public int playerHandSize;
    public int nextEncounter;
    public List<CardObject> deck;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
