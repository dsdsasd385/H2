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

        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PlayDieAni()
    {
        _animator.SetTrigger("Die");

        yield return new WaitForSeconds(0.5f);
    }
}
