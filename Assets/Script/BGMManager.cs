using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BGMManager : MonoBehaviour {

    public AudioSource audioSource;
    //Singleton
    public static BGMManager bGMManager;

	// Use this for initialization
	void Start () {

        if (bGMManager == null)
        {
            bGMManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Loop the background music forever
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.loop = true;
	}
}
