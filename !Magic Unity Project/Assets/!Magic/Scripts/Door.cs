using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
	private Animator m_animator;
    private bool m_IsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
		m_animator = GetComponent<Animator>();
    }
	
    public void OpenDoor(bool open)
    {
		m_animator.SetBool("open", open);
        m_IsOpen = open;
    }

    public void ToggleDoor()
    {
        OpenDoor(!m_IsOpen);
    }
}
