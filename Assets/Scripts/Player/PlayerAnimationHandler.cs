using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    public IEnumerator PlayAttackAni()
    {
        Debug.Log("애니메이션 실행");
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

    public IEnumerator PlayRunToAttackAni()
    {
        _animator.SetTrigger("Run");

        yield return PlayAttackAni();

        yield return new WaitForSeconds(0.5f);
    }

}
