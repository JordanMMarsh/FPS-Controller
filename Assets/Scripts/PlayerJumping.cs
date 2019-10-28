using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour, IPlayerStates
{
    Rigidbody rb;
    PlayerFeet playerFeet;
    private bool hasJumped = false;
    private bool jumpDelay = false;
    private float jumpVelocity = 4f;

    private void Start()
    {
        playerFeet = GetComponentInChildren<PlayerFeet>();
        rb = GetComponent<Rigidbody>();
    }

    public PlayerState.playerStates Action()
    {
        if (playerFeet.IsGrounded() && !hasJumped)
        {
            if (!jumpDelay)
            {
                StartCoroutine(JumpDelay());
                jumpDelay = true;
                rb.velocity += new Vector3(0, jumpVelocity, 0);
            }
        }
        else if (playerFeet.IsGrounded() && hasJumped)
        {
            hasJumped = false;
            return PlayerState.playerStates.idle;
        }

        return PlayerState.playerStates.jumping;
    }

    //Jump delay exists to allow a small margin of starting a jump and telling the rest of the code we are jumping.
    //This is to prevent the few frames where the player has started their jump but the "feet" collider hasn't left the ground and thereby tells everyone else that we haveJumped and are on the ground
    //when in fact we haven't left the ground yet.
    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.5f);
        hasJumped = true;
        jumpDelay = false;
    }
}
