using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
	//[HideInInspector]
	public Hand m_ActiveHand = null;

	public Material m_HighlightMaterial = null;

	public virtual void Parent()
	{
		if (!m_ActiveHand) return;

		// Attach to controller
		Rigidbody targetBody = GetComponent<Rigidbody>();
		m_ActiveHand.m_Joint.connectedBody = targetBody;
	}

	public virtual void OnHoverBegin(Hand hand)
	{
		if (hand.GetCurrentInteractable() != this)
			Highlight(true);
	}

	public virtual void OnHoverEnd(Hand hand)
	{
		if (hand.m_PairedHand)
		{
			if (!hand.m_PairedHand.GetComponent<Hand>().GetContactInteractable().Contains(this))
				Highlight(false);
		}
		else
			Highlight(false);
	}

	public virtual void OnInteractBegin(Hand hand)
	{
		if(hand.GetCurrentInteractable()) return;

		// Already held check
		if (m_ActiveHand)
			OnInteractEnd(m_ActiveHand);

		// Set active hand
		m_ActiveHand = hand;

		m_ActiveHand.SetCurrentInteractable(this);

		// Parent to controller
		Parent();

		// Clear Interactables
		m_ActiveHand.GetContactInteractable().Clear();

		// Remove Highlight
		Highlight(false);
	}

	public virtual void OnInteractEnd(Hand hand)
	{
		if (!m_ActiveHand) return;

		// Apply velocity
		Rigidbody targetBody = m_ActiveHand.GetCurrentInteractable().GetComponent<Rigidbody>();
		targetBody.velocity = m_ActiveHand.GetPose().GetVelocity();
		targetBody.angularVelocity = m_ActiveHand.GetPose().GetAngularVelocity();

		// Detach from controller
		m_ActiveHand.m_Joint.connectedBody = null;

		// Clear Interactables
		m_ActiveHand.GetContactInteractable().Clear();

		// Force tigger enter
		m_ActiveHand.ForceTrigger(m_ActiveHand.GetCurrentInteractable().GetComponent<Collider>());

		// Clear variables
		m_ActiveHand.SetCurrentInteractable(null);
		m_ActiveHand = null;
	}

	public virtual void OnInteractGripBegin(Hand hand)
	{

	}

	public virtual void OnInteractGripEnd(Hand hand)
	{
		
	}

	public void Highlight(bool state)
	{
		Material[] materials = GetComponent<Renderer>().materials;
		if (materials.Length < 1) return;
		materials[1] = state ? m_HighlightMaterial : null;
		GetComponent<Renderer>().materials = materials;
	}
}
