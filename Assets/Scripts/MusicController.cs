
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public GameManager manager;
    
    FMOD.Studio.STOP_MODE IMMEDIATE;

    public FMODUnity.EventReference musicEventRef;
    private string music;
    public FMODUnity.EventReference sfxEventRef;
    private string sfx;
   
    FMOD.Studio.EventInstance musicEvent;
    FMOD.Studio.EventInstance sfxEvent;



    // Start is called before the first frame update
    void Start()
    {
       
        
        music = "event:/Music";
        musicEvent = FMODUnity.RuntimeManager.CreateInstance(music);
        musicEvent.start();

        sfx = "event:/Footsteps";
        sfxEvent = FMODUnity.RuntimeManager.CreateInstance(sfx);
        

    }

    private void Update()
    {
        Speed();
    }

    public void Feet()
    {
        sfxEvent.start();
    }

    public void Stop()
    {
        musicEvent.stop(IMMEDIATE);
    }

    public void Speed()
    {
        musicEvent.setParameterByName("Speed", manager.speed);
    }


}