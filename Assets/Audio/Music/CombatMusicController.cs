using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip loopClip;
    [SerializeField] private AudioClip victoryClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.loop = false;
        audioSource.clip = startClip;
        audioSource.Play();
        StartCoroutine(EndFirst());
    }

    IEnumerator EndFirst()
    {
        yield return new WaitForSeconds(startClip.length);
        audioSource.clip = loopClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Victory()
    {
        audioSource.clip = victoryClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Defeat()
    {
        Destroy(gameObject);
    }
}
