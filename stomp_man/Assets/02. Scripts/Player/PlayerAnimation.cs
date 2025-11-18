using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void TriggerStomp()
    {
        _animator.SetTrigger("Stomp");
    }

    public void TriggerJump()
    {
        _animator.SetTrigger("Jump");
    }

    public void TriggerIdle()
    {
        _animator.SetTrigger("Idle");
        _animator.ResetTrigger("Jump");
        _animator.ResetTrigger("Stomp");
    }
}
