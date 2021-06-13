using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehaviour : MonoBehaviour
{
    [Header("Component References")]
    public Animator playerAnimator;

    //Animation String IDs
    private int playerMovementAnimationID;
    private int playerHorzAnimationID;
    private int playerVertAnimationID;
    private int playerShotAnimationID;
    private int playerPullBackAnimationID;
    private int playerRunningAnimationID;
    private int playerAimAnimationID;
    private int playerJumpAnimationID;

    public void SetupBehaviour()
    {
        SetupAnimationIDs();
    }

    void SetupAnimationIDs()
    {
        playerHorzAnimationID = Animator.StringToHash("horz");
        playerVertAnimationID = Animator.StringToHash("vert");
        playerPullBackAnimationID = Animator.StringToHash("ShotStrength");
        playerShotAnimationID = Animator.StringToHash("Shot");
        playerRunningAnimationID = Animator.StringToHash("isRunning");
        playerAimAnimationID = Animator.StringToHash("Draw");
        playerJumpAnimationID = Animator.StringToHash("inAir");
    }

    public void UpdateHorzMovementAnimation(float movementBlendValue)
    {
        playerAnimator.SetFloat(playerHorzAnimationID, movementBlendValue);
    }

    public void UpdateVertMovementAnimation(float movementBlendValue)
    {
        playerAnimator.SetFloat(playerVertAnimationID, movementBlendValue);
    }

    public void UpdatePullBackAnimation(float shotStrength)
    {
        playerAnimator.SetFloat(playerPullBackAnimationID, shotStrength);
    }

    public void UpdateShotAnimation(bool isShooting)
    {
        playerAnimator.SetBool(playerShotAnimationID, isShooting);
    }

    public void UpdateRunAnimation(bool isRunning)
    {
        playerAnimator.SetBool(playerRunningAnimationID, isRunning);
    }

    public void UpdateAimAnimation(bool isDrawn)
    {
        playerAnimator.SetBool(playerAimAnimationID, isDrawn);
    }

    public void UpdateJumpAnimation(bool inAir)
    {
        playerAnimator.SetBool(playerJumpAnimationID, inAir);
    }

}
