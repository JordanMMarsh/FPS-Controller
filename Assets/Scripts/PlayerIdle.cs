using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : MonoBehaviour, IPlayerStates
{
    private const PlayerState.playerStates IDLE_STATE = PlayerState.playerStates.idle;
    
    public PlayerState.playerStates Action()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            return PlayerState.playerStates.walking;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return PlayerState.playerStates.jumping;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            return PlayerState.playerStates.crouching;
        }
        return IDLE_STATE;
    }
}
