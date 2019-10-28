using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour, IPlayerStates
{
    private const PlayerState.playerStates MOVE_STATE = PlayerState.playerStates.walking;

    #region Movement
    Vector3 forwardMovement;
    Vector3 strafeMovement;
    float activeSprintMultiplier = 1.5f;
    float sprintMultiplier = 1f;
    float defaultSprintMultiplier = 1f;
    float strafePenalty = .5f;
    [SerializeField] private float moveSpeed = 5f;
    #endregion

    Rigidbody rb;

    //Used to tell FixedUpdate when to apply velocity to rigid body and when to ignore
    private bool activeState = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Acts as update for this state.
    public PlayerState.playerStates Action()
    {
        activeState = true;
        var vertInput = Input.GetAxis("Vertical");
        var horInput = Input.GetAxis("Horizontal");

        #region Change State
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activeState = false;
            return PlayerState.playerStates.jumping;                      
        }

        //If player isn't pressing a move key, return them to idle
        if (vertInput == 0 && horInput == 0)
        {
            activeState = false;
            return PlayerState.playerStates.idle;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            activeState = false;
            return PlayerState.playerStates.crouching;
        }
        #endregion

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintMultiplier = activeSprintMultiplier;
        }
        else
        {
            sprintMultiplier = defaultSprintMultiplier;
        }

        //Get a forward movement based on transform.Forward and player input * moveSpeed (and horizontal movement)
        forwardMovement = vertInput * moveSpeed * transform.forward * sprintMultiplier;
        strafeMovement = horInput * moveSpeed * transform.right * strafePenalty;        
        return MOVE_STATE;
    }

    private void FixedUpdate()
    {
        if (!activeState)
        {
            return;
        }
        forwardMovement.y = rb.velocity.y;
        rb.velocity = forwardMovement;
        rb.velocity += strafeMovement;
    }
}
