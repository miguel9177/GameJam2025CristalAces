using UnityEngine;

public class Ai_PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;

    public const string WalkAnimation = "Moving";
    public const string IdleAnimation = "Idle";
    public const string TalkingAnimation1 = "Talking";
    public const string TalkingAnimation2 = "Talking2";
    public const string TalkingOnThePhoneAnimation = "TalkingOnPhone";
    public const string RappingAnimation = "Rapping";

    private bool walking = false;

    public void StartSpecificAnimation(string animation)
    {
        walking = false;
        animator.SetTrigger(animation);
    }

    public void StartWalkingAnimation()
    {
        if (walking)
            return;

        walking = true;
        animator.SetTrigger(WalkAnimation);
    }

    public void StartIdleAnimation()
    {
        walking = false;
        animator.SetTrigger(IdleAnimation);
    }

    private void ResetAllAnimations()
    {
        walking = false;
        animator.ResetTrigger(WalkAnimation);
        animator.ResetTrigger(IdleAnimation);
        animator.ResetTrigger(TalkingAnimation1);
        animator.ResetTrigger(TalkingAnimation2);
        animator.ResetTrigger(TalkingOnThePhoneAnimation);
        animator.ResetTrigger(RappingAnimation);
    }
}
