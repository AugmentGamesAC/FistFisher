using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandCustom : MonoBehaviour
{
	public SteamVR_Action_Boolean m_GrabAction = null;
	public SteamVR_Action_Boolean m_GripAction = null;
	public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;

	public GameObject m_PairedHand = null;

	private SteamVR_Behaviour_Pose m_Pose = null;
	public FixedJoint m_Joint { get; set; } = null;
	public FixedJoint m_EquipmentJoint { get; set; } = null;

	private InteractableCustom m_CurrentEquipment = null;
	private InteractableCustom m_CurrentInteractable = null;
	private List<InteractableCustom> m_ContactInteractable { get; set; } = new List<InteractableCustom>();

	public SteamVR_Behaviour_Pose GetPose() { return m_Pose; }

	public InteractableCustom GetCurrentInteractable() { return m_CurrentInteractable; }
	public void SetCurrentInteractable(InteractableCustom interactable) { m_CurrentInteractable = interactable; }

	public InteractableCustom GetCurrentEquipment() { return m_CurrentEquipment; }
	public void SetCurrentEquipment(InteractableCustom interactable) { m_CurrentEquipment = interactable; }

	public List<InteractableCustom> GetContactInteractable() { return m_ContactInteractable; }

    private void Awake()
    {
		m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
		FixedJoint[] jointComponents = GetComponents<FixedJoint>();
		m_Joint = jointComponents[0];
		m_EquipmentJoint = jointComponents[1];

	}

	void OnEnable()
	{
		if (m_GrabAction != null)
		{
			m_GrabAction.AddOnStateDownListener(OnTriggerDown, inputSource);
			m_GrabAction.AddOnStateUpListener(OnTriggerUp, inputSource);
		}
		if (m_GripAction != null)
		{
			m_GripAction.AddOnStateDownListener(OnGripDown, inputSource);
			m_GripAction.AddOnStateUpListener(OnGripUp, inputSource);
		}
	}


	private void OnDisable()
	{
		if (m_GrabAction != null)
		{
			m_GrabAction.AddOnStateDownListener(OnTriggerDown, inputSource);
			m_GrabAction.AddOnStateDownListener(OnTriggerUp, inputSource);
		}
		if (m_GripAction != null)
		{
			m_GripAction.AddOnStateDownListener(OnGripDown, inputSource);
			m_GripAction.AddOnStateDownListener(OnGripUp, inputSource);
		}
	}

	private void FixedUpdate()
    {

	}

	public void ForceTrigger(Collider other)
	{
		OnTriggerEnter(other);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Interactable")) return;

		InteractableCustom interactable = other.gameObject.GetComponent<InteractableCustom>();
		m_ContactInteractable.Add(interactable);
		InteractableCustom nearest = GetNearestInteractable();
		if (nearest == interactable)
			nearest.OnHoverBegin(this);
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.gameObject.CompareTag("Interactable")) return;

		InteractableCustom interactable = other.gameObject.GetComponent<InteractableCustom>();
		m_ContactInteractable.Remove(interactable);
		interactable.OnHoverEnd(this);
	}

	private void OnTriggerDown(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources)
	{
		if (sources != inputSource) return;
		Pickup();
	}

	private void OnTriggerUp(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources)
	{
		if (sources != inputSource) return;
		Drop();
	}

	private void OnGripDown(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources)
	{
		if (sources != inputSource) return;
		GripPickup();
	}

	private void OnGripUp(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources)
	{
		if (sources != inputSource) return;
		GripDrop();
	}

	private void Pickup()
	{
		if (m_ContactInteractable.Count < 1) return;

		// Get nearest interactable
		InteractableCustom interactable = GetNearestInteractable();
		// Null check
		if (!interactable) return;

		interactable.OnInteractBegin(this);
	}

	private void Drop()
	{
		if (m_CurrentInteractable)
			m_CurrentInteractable.OnInteractEnd(this);
		if (m_CurrentEquipment)
			m_CurrentEquipment.OnInteractEnd(this);
	}

	private void GripPickup()
	{
		if (m_CurrentInteractable)
			m_CurrentInteractable.OnInteractGripBegin(this);
		if (m_CurrentEquipment)
			m_CurrentEquipment.OnInteractGripBegin(this);
	}

	private void GripDrop()
	{
		if (m_CurrentInteractable)
			m_CurrentInteractable.OnInteractGripEnd(this);
		if (m_CurrentEquipment)
			m_CurrentEquipment.OnInteractGripEnd(this);
	}

	public InteractableCustom GetNearestInteractable()
	{
		InteractableCustom nearest = null;
		float minDistance = float.MaxValue;
		float distance = 0.0f;

		foreach(InteractableCustom interactable in m_ContactInteractable)
		{
			distance = (interactable.transform.position - transform.position).sqrMagnitude;
			if(distance < minDistance)
			{
				minDistance = distance;
				nearest = interactable;
			}
		}
		return nearest;
	}
}
