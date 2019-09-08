using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour {

	public AudioClip[] Tracks;
	public AudioSource MyAudioSource;
	public int LastTrack;
	public float Timer = 0;
	public int Track;
	public List<int> AlreadyPlayed = new List<int>();
	public bool ShouldPlay = true;

	// Update is called once per frame
	void Update () {

		if(!MyAudioSource.isPlaying)
		{
			Timer = Timer + Time.deltaTime;
		}


        if(Timer > 3.0)
		{
			Track = Random.Range(0, Tracks.Length); //generate a track to play

			if (Track != LastTrack && GameObject.Find("MenuController").GetComponent<SaveMusicToggle>().music_toggle == 1) //see if it is the Last Track Played.
			{
				ShouldPlay = true; //make this true for the evalutation below to turn it false if needed.

				LastTrack = Track; //Make the Last Track the Track we're going to play now.

				if (AlreadyPlayed.Count != 0) //see if we've played it in the line up already
				{
					foreach (int t in AlreadyPlayed)
					{
						if (t == Track)
						{
							ShouldPlay = false; //if we've played it in the line up, then dont play it.
						}
					}
				}

				if (AlreadyPlayed.Count == Tracks.Length) //if we've played all of the songs then we'll start over.
				{
					foreach (int t in AlreadyPlayed)
					{
						AlreadyPlayed.Remove(t);
					}
				}

				if (ShouldPlay)
				{
					AlreadyPlayed.Add(Track); //if we haven't played anything then we'll add the track to the list and play it.	
				}

			}
			else
			{
				ShouldPlay = false;
			}


            //if the above logic did not turn ShouldPlay to False, then kick it.
            if (ShouldPlay)
			{
				MyAudioSource.clip = Tracks[Track];
                MyAudioSource.Play();
                Timer = 0;
			}         
		}
	}
}
