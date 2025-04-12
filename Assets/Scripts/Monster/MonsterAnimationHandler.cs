using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public IEnumerator PlayAttackAni()
    {
        _animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator PlayDamagedAni()
    {
        _animator.SetTrigger("Damaged");

        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator PlayDieAni()
    {
        _animator.SetTrigger("Die");

        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PlayRunAni()
    {
        _animator.SetTrigger("Run");
        Debug.Log("달리기 애니메이션");
        yield return null;
    }

    public IEnumerator PlayIdle()
    {
        _animator.SetTrigger("Idle");
        yield return null;

    }
}
