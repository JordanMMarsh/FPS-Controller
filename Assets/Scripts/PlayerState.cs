using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum playerStates
    {
        idle,
        walking,
        crouching,
        jumping
    };

    private playerStates state = playerStates.idle;

    PlayerIdle playerIdle;
    PlayerWalking playerWalking;
    PlayerCrouching playerCrouching;
    PlayerJumping playerJumping;
    
    private void Start()
    {
        playerIdle = GetComponent<PlayerIdle>();
        playerWalking = GetComponent<PlayerWalking>();
        playerCrouching = GetComponent<PlayerCrouching>();
        playerJumping = GetComponent<PlayerJumping>();
    }

    private void Update()
    {
        switch (state)
        {
            case playerStates.idle:
                {
                    state = playerIdle.Action();
                    break;
                }
            case playerStates.walking:
                {
                    state = playerWalking.Action();
                    break;
                }
            case playerStates.jumping:
                {
                    state = playerJumping.Action();
                    break;
                }
            case playerStates.crouching:
                {
                    state = playerCrouching.Action();
                    break;
                }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }        
}
