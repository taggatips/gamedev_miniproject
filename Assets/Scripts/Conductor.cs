using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;
    //The number of seconds for each song beat
    public float secPerBeat;
    //Current song position, in seconds
    public float songPosition;
    //Current song position, in beats
    public float songPositionInBeats;
    //How many seconds have passed since the song started
    public float dspSongTime;
    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;
    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;
    //the number of beats in each loop
    public float beatsPerLoop;
    //the total number of loops completed since the looping clip first started
    public int completedLoops = 0;
    //The current position of the song within the loop in beats.
    public float loopPositionInBeats;
    //The current relative position of the song within the loop measured between 0 and 1.
    public float loopPositionInAnalog;
    //Conductor instance
    public static Conductor instance; 
    // Error Margin for missing the beat in s
    public float errorMargin = 0.2f; 
    void Awake()
    {
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Load the AudioSource attached to the Conductor GameObject
        musicSource = GetComponent<AudioSource>();
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        //Start the music
        musicSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        //determine how many beats since the song started
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
            completedLoops++;
        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;
        songPositionInBeats = songPosition / secPerBeat;
        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
    }

    public bool onBeat(){
        bool onBeat = false;
        if((songPosition % secPerBeat - errorMargin) <= 0){
            onBeat = true;
        }
        return onBeat; 
    }

    public float timeToNextBeat(){
        return songPosition % secPerBeat; 
    }
}
