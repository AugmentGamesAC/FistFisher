using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
	private Animator m_animator;
    // Start is called before the first frame update
    void Start()
    {
		m_animator = GetComponent<Animator>();
    }
	
    public void OpenDoor(bool open)
    {
		m_animator.SetBool("open", open);
    }
}
