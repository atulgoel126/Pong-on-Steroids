using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour {


    private AudioSource audioSource;

    //-----------------------------------------------------------------ASSIGN VALUES IN EDITOR-------------------------------------------
    public AudioClip ballStrikesPaddleSound;
    public AudioClip ballStrikesBallSound;
    public AudioClip ballStrikesBoundarySound;

    public AudioClip ballEntersDeathzoneSound;
    public AudioClip ballSpawnSound;
    public AudioClip gameOverSound;
    //----------------------------------------------------------------END AUDIO CLIPS----------------------------------------------------

    public static SFXManager sFXManager;
    // Use this for initialization
    void Start ()
    {
        //Since we require is already, we don't have to check. :)
        audioSource = GetComponent<AudioSource>();
        
        //Singleton assignment. Good practice to have if condition instead of directly assinging it.
        if (sFXManager == null)
        {
            sFXManager = this;
        }
        else
        {
            //Delete immediately. You do not that type of negativity in your life.
            Destroy(gameObject);
        }
	}

    public void playSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }


}
