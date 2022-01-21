using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSteps : MonoBehaviour
{
    private FMOD.Studio.EventInstance footsteps; //declare FMOD audio event
    // Start is called before the first frame update
    public GameManager manager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //interface with FMOD API
    private void PlayFootstep(float speed)
    {
        speed = manager.speed;
        footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps"); //assign to known Unity event
        footsteps.setParameterByName("Speed", speed); //set terrain as passed in from PlayFootstep
        
       
        footsteps.start(); //play the sound
        footsteps.release(); //end playback
    }
}
