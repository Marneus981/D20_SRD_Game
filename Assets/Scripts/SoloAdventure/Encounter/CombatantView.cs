using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CombatantView : MonoBehaviour
{
    public SpriteRenderer avatar;
    public SpriteRenderer shadow;
    public bool flipDirection;
}

public enum CombatantDirection
{
    Left,
    Right
}

public enum CombatantAnimation
{
    Attack,
    Death,
    Gesture,
    Idle,
    Walk
}

public interface ICombatantViewSystem : IDependency<ICombatantViewSystem>
{
    void SetLayerOrder(CombatantView view, int value);
    void SetAnimation(CombatantView view, CombatantAnimation animation);//looping
    UniTask PlayAnimation(CombatantView view, CombatantAnimation animation);//1 time
}

public class CombatantViewSystem : ICombatantViewSystem
{
    //Cache hash values for state names
    int attackState = Animator.StringToHash("Attack");
    int deathState = Animator.StringToHash("Death");
    int gestureState = Animator.StringToHash("Gesture");
    int idleState = Animator.StringToHash("Idle");
    int walkState = Animator.StringToHash("Walk");

    public void SetLayerOrder(CombatantView view, int value)
    /*
    Sets order for avatar and shadow sprite renderers
    x2 bc there are two renderers (to make sure that shadow and avatar are
    in correct order)
    */
    {
        view.shadow.sortingOrder = -value * 2;
        view.avatar.sortingOrder = -value * 2 + 1;
    }

    public void SetAnimation(CombatantView view, CombatantAnimation animation)
    //Grabs hash assosicated with anim enum + animator object is on
    //Then calls crossfade to transition to the new state
    {
        var hash = HashForState(animation);
        var animator = view.avatar.GetComponent<Animator>();
        animator.CrossFade(hash, 0);
    }

    public async UniTask PlayAnimation(CombatantView view, CombatantAnimation animation)
    //Similar to above but has loop that awaits next frame to check
    //if the animator’s current state has changed.
    {
        var hash = HashForState(animation);
        var animator = view.avatar.GetComponent<Animator>();
        animator.CrossFade(hash, 0);
        while (true)
        {
            await UniTask.NextFrame(animator.GetCancellationTokenOnDestroy());
            if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != hash)
                break;
        }
    }

    int HashForState(CombatantAnimation animState)
    //Private method to map anim state to anim hash
    {
        switch (animState)
        {
            case CombatantAnimation.Attack://hash
                return attackState; //state
            case CombatantAnimation.Death:
                return deathState;
            case CombatantAnimation.Gesture:
                return gestureState;
            case CombatantAnimation.Idle:
                return idleState;
            case CombatantAnimation.Walk:
                return walkState;
            default:
                return 0;
        }
    }
}