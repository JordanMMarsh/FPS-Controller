using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouching : MonoBehaviour, IPlayerStates
{
    private const PlayerState.playerStates CROUCH_STATE = PlayerState.playerStates.crouching;

    CapsuleCollider playerCollider;
    private bool activeState = false;
    private bool crouched = false;
    private bool crouchStarted = false;
    PlayerHead playerHead;

    #region Movement
    Vector3 forwardMovement;
    Vector3 strafeMovement;
    private float crouchedMoveSpeed = 3f;
    Rigidbody rb;
    #endregion

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        playerHead = GetComponentInChildren<PlayerHead>();
    }

    public PlayerState.playerStates Action()
    {
        activeState = true;
        if (!crouchStarted)
        {
            crouchStarted = true;
            SetCrouched(true);
        }
        var vertInput = Input.GetAxis("Vertical");
        var horInput = Input.GetAxis("Horizontal");

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl)) && playerHead.FreeAbove())
        {
            if (crouched)
            {
                SetCrouched(false);
            }
            activeState = false;
            return PlayerState.playerStates.idle;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (crouched)
            {
                SetCrouched(false);
            }
            activeState = false;
            return PlayerState.playerStates.walking;
        }

        //Get a forward movement based on transform.Forward and player input * moveSpeed (and horizontal movement)
        forwardMovement = vertInput * crouchedMoveSpeed * transform.forward;
        strafeMovement = horInput * crouchedMoveSpeed * transform.right;
        return CROUCH_STATE;
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

    private void SetCrouched(bool setter)
    {
        if (!setter)
        {
            crouched = false;
            crouchStarted = false;
            playerCollider.height = 2f;
        }
        else
        {
            crouched = true;
            playerCollider.height = 1f;
        }
    }
}
