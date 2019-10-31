using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour /*, IWeapon*/
{
    public Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        m_animator.SetTrigger("Base Attack");
    }

    //example for second trigger on the Idle animator state.
    //public void PerformSpecialAttack()
    //{
    //    m_animator.SetTrigger("Special Attack");
    //}
}
