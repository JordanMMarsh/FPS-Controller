using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerStates
{
    //Action will be called by the PlayerState script in each update, effectively giving that state the Update function without each state needing to check for itself.
    PlayerState.playerStates Action();
}
