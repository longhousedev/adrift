using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public AudioSource taunt;
    public Text gameOver;
    
    void Update()
    {
        //Will update the doppelgangers destination to the where the player is
        agent.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the doppelganger collides with the player it will kill the player
        //shout a taunt and display the game over text
        if (other.gameObject.tag.Equals("Player"))
        {
            taunt.Play();
            Destroy(other.gameObject);
            gameOver.enabled = true;
        }
    }
}
