using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is meant to represent the behaviors of a pressure plate, executing a specific action 
 * when a player (or perhaps other entity) collides with it. Because the desired behaviors for when
 * the player collides with a pressure plate have not yet been decided, this class is mostly just a
 * framework which can be expanded in the future. 
 */
public class PressurePlate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The OnCollisionEnter2D method is called whenever the collider of another GameObject first 
    // overlaps with the collider belonging to the object which has this script attached to it
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Checks if the tag of the GameObject which has collided with this GameObject is "Player",
        // which (assuming we give the Player GameObject a "Player" tag) means the player has 
        // collided with this GameObject
        if(other.gameObject.CompareTag("Player")){
            // Can specify which behavior we want a pressure plate to execute when it collides with
            // the Player here (eg. a door opening)
        } else {
            // Can add other behaviors if other GameObjects collided with the pressure plate
        }
    }

    // The OnCollisionExit2D method is called whenever the collider of another GameObject which 
    // was previously overlapping with the collider of this GameObject stops overlapping
    // with this GameObject's collider
    private void OnCollisionExit2D(Collision2D other)
    {
        // Checks if the tag of the GameObject which has stoped colliding with this GameObject is 
        // "Player", which (assuming we give the Player GameObject a "Player" tag) means the player 
        // has stopped colliding with this GameObject
        if(other.gameObject.CompareTag("Player")){
            // Can specify which behavior we want a pressure plate to execute when it stops colliding 
            // with the Player here (eg. a door closing)
        } else {
            // Can add other behaviors if other GameObjects stopped colliding with the pressure plate
        }
    }
}
