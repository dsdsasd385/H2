using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationHandler : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAni()
    {
        _animator.SetTrigger("Attack");
    }
    public void PlayDamagedAni()
    {
        _animator.SetTrigger("Damaged");
    }

    public void PlayDieAni()
    {
        _animator.SetTrigger("Die");
    }
}
