using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaunchControl : MonoBehaviour
{
    public Button startCountdownButton;
    public TMP_Text countdownText;
    public float engineThrust;

    private GameObject rocket;
    private bool rocketLaunched;
    

    void Start()
    {
        //Hide the start countdown button initially
        startCountdownButton.gameObject.SetActive(false);

    }

    public void OnRocketPlaced()
    {

        //Find the rocket in the scene
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        
        //Show the start countdown button initially
        startCountdownButton.gameObject.SetActive(true);
    }

    public void OnStartCountdownPressed()
    {
        StartCoroutine(startCountdown());

    }

    private IEnumerator startCountdown()
    {
        //Start the countdown at 10
        int countdown = 10;


        //Keep counting down until we reach zero
        while(countdown > 0)
        {

            //Update the countdown text on the screen
            countdownText.text = countdown.ToString();

            //Wait for a second
            yield return new WaitForSeconds(1f);

            //If there are 3 seconds left 
            if(countdown == 3)
            {
                //Turn on the particle effects
                TurnOnParticleEffects();
          
            }

            //Decrease the countdown
            countdown -= 1;
    }

        //Display the liftoff
        countdownText.text = "Liftoff!";

        //Launch the rocket
        rocketLaunched = true;

        //Wait for 3 second
        yield return new WaitForSeconds(3f);

       

        //Clear the liftoff message
        countdownText.text = "";

    }

    private void TurnOnParticleEffects()
    {
        //For each particle system under the rocket
        ParticleSystem[] foundParticleSystems = rocket.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particleSystem in foundParticleSystems)
        {
            //Start the particle system
            particleSystem.Play();
        }
    }

    private void Update()
    {
        //If the rocket has been launched
        if(rocketLaunched)
        {
            //Apply an upaward engine force to the rocket
            rocket.GetComponent<Rigidbody>().AddForce(rocket.transform.up * engineThrust * Time.deltaTime);


        }


    }
}
