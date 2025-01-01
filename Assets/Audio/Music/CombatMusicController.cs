using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startStandardClip;
    [SerializeField] private AudioClip loopStandardClip;
    [SerializeField] private AudioClip victoryClip;
    [SerializeField] private AudioClip startBossClip;
    [SerializeField] private AudioClip loopBossClip;
    private AudioClip startClip;
    private AudioClip loopClip;

    // Start is called before the first frame update
    void Start()
    {
        if (MainManager.Instance.miniboss || MainManager.Instance.finalBattle)
        {
            startClip = startBossClip;
            loopClip = loopBossClip;
        }
        else
        {
            startClip = startStandardClip;
            loopClip = loopStandardClip;
        }

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
