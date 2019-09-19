using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EquipableCustom : InteractableCustom
{
	//[HideInInspector]
	
	public Vector3 m_HeldRotation = new Vector3();
	public Vector3 m_HeldOffset = new Vector3();

	public override void Parent()
	{
		//if (!m_ActiveHand.GetCurrentInteractable()) return;
		transform.rotation = m_ActiveHand.transform.rotation;
		transform.Rotate(m_HeldRotation);
		transform.position = m_ActiveHand.transform.position;
		transform.Translate(m_HeldOffset);
		if (!m_ActiveHand) return;

		// Attach to controller
		Rigidbody targetBody = GetComponent<Rigidbody>();
		m_ActiveHand.m_EquipmentJoint.connectedBody = targetBody;
	}

	public override void OnHoverBegin(HandCustom hand)
	{
		if (!m_ActiveHand)
			Highlight(true);
	}

	public override void OnInteractBegin(HandCustom hand)
	{
		// Already held check
		if (m_ActiveHand) return;

		// Set active hand
		m_ActiveHand = hand;

		m_ActiveHand.SetCurrentEquipment(this);

		// Parent to controller
		Parent();

		// Clear Interactables
		m_ActiveHand.GetContactInteractable().Clear();

		// Remove Highlight
		Highlight(false);
	}

	public override void OnInteractEnd(HandCustom hand)
	{

	}

	public override void OnInteractGripBegin(HandCustom hand)
	{
		// Detach from controller
		m_ActiveHand.m_EquipmentJoint.connectedBody = null;

		// Clear Interactables
		m_ActiveHand.GetContactInteractable().Clear();

		// Force tigger enter
		m_ActiveHand.ForceTrigger(m_ActiveHand.GetCurrentEquipment().GetComponent<Collider>());

		// Clear variables
		m_ActiveHand.SetCurrentEquipment(null);
		m_ActiveHand = null;
	}

	public override void OnInteractGripEnd(HandCustom hand)
	{
		
	}

}
