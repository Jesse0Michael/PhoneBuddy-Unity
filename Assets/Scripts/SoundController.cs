using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{

    public AudioClip Growl1;
    public AudioClip Growl2;
    public AudioClip Growl3;
    public AudioClip Growl4;
    public AudioClip Bark1;
    public AudioClip Bark2;
    public AudioClip Bark3;
    public AudioClip Bark4;
    public AudioClip Bark5;
    public AudioClip Eat1;
    public AudioClip Drink2;

    private AudioSource source;
    private AudioClip[] growlClips;
    private AudioClip[] barkClips;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        growlClips = new AudioClip[4] { Growl1, Growl2, Growl3, Growl4 };
        barkClips = new AudioClip[5] { Bark1, Bark2, Bark3, Bark4, Bark5 };
    }

    public void Bark()
    {
        int barkSelection = Random.Range(0, 5);
        source.clip = barkClips[barkSelection];
        source.Play();
    }

    void Growl()
    {
        int growlSelection = Random.Range(0, 4);
        source.clip = growlClips[growlSelection];
        source.Play();
    }

    void Eat()
    {

    }

    void Drink()
    {

    }

    void Stop()
    {

    }

}
