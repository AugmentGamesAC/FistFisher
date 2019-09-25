using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Hand))]
	public class HandEvents : MonoBehaviour
	{
		public bool lookingForInput = false;
		public SteamVR_Input_Sources explicitType = SteamVR_Input_Sources.Any;
		public Player m_Player;

		public UnityEvent onEmptyHandGrabPinchBegin;
		public UnityEvent onEmptyHandGrabPinchEnd;
		public UnityEvent onEmptyHandGrabPinchUpdate;

		public UnityEvent onEmptyHandGrabGripBegin;
		public UnityEvent onEmptyHandGrabGripEnd;
		public UnityEvent onEmptyHandGrabGripUpdate;

		public UnityEvent onFullHandGrabPinchBegin;
		public UnityEvent onFullHandGrabPinchEnd;
		public UnityEvent onFullHandGrabPinchUpdate;

		public UnityEvent onFullHandGrabGripBegin;
		public UnityEvent onFullHandGrabGripEnd;
		public UnityEvent onFullHandGrabGripUpdate;

		public SteamVR_Action_Vector2 Axis;
		public UnityEvent onAxisChange;

		public SteamVR_Action_Boolean PadClick;
		public UnityEvent onPadClick;

		public SteamVR_Action_Boolean PadTouch;
		public UnityEvent onPadTouch;
		public UnityEvent onPadTouchRelease;
		public UnityEvent onPadTouchUpdate;

		short m_MaxMessagesPerFrame = 1;
		short m_MessagesRecievedThisFrame = 0;

		private bool m_IsInitialized = false;

		public void Init()
		{
			Axis.AddOnAxisListener(OnAxisChange, GetComponent<Hand>().handType);
			PadClick.AddOnStateDownListener(OnPadClick, GetComponent<Hand>().handType);
			PadTouch.AddOnStateDownListener(OnPadTouch, GetComponent<Hand>().handType);
			PadTouch.AddOnStateUpListener(OnPadTouchRelease, GetComponent<Hand>().handType);
			PadTouch.AddOnUpdateListener(OnPadTouchUpdate, GetComponent<Hand>().handType);
			m_IsInitialized = true;
		}

		private void OnDestroy()
		{
			if (!m_IsInitialized)
				return;
			Axis.RemoveOnAxisListener(OnAxisChange, GetComponent<Hand>().handType);
			PadClick.RemoveOnStateDownListener(OnPadClick, GetComponent<Hand>().handType);
			PadTouch.RemoveOnStateDownListener(OnPadTouch, GetComponent<Hand>().handType);
			PadTouch.RemoveOnStateUpListener(OnPadTouchRelease, GetComponent<Hand>().handType);
			PadTouch.RemoveOnUpdateListener(OnPadTouchUpdate, GetComponent<Hand>().handType);
		}

		protected virtual void LateUpdate()
		{
			m_MessagesRecievedThisFrame = 0;
		}

		void RecieveMessage(UnityEvent e, bool ignoreMaxMessages = false, Hand hand = null)
		{
			if ((m_MessagesRecievedThisFrame >= m_MaxMessagesPerFrame && !ignoreMaxMessages) 
				|| (hand != null && explicitType != SteamVR_Input_Sources.Any && hand.handType != explicitType))
			{
				RejectMessage(e, hand);
				return;
			}

			e.Invoke();

			if(!ignoreMaxMessages)
				m_MessagesRecievedThisFrame++;
		}

		void RejectMessage(UnityEvent e, Hand hand)
		{
			Debug.LogWarning("Rejected a message!");
		}

		public virtual void OnPadTouchUpdate(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
		{
			RecieveMessage(onPadTouchUpdate, true);
		}

		public virtual void OnPadTouchRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
		{
			RecieveMessage(onPadTouchRelease, true);
		}

		public virtual void OnPadTouch(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
		{
			RecieveMessage(onPadTouch, true);
		}

		public virtual void OnPadClick(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
		{
			RecieveMessage(onPadClick, true);
		}

		public virtual void OnAxisChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
		{
			RecieveMessage(onAxisChange, true);
		}

		//-------------------------------------------------
		private void OnEmptyHandGrabPinchBegin(Hand hand)
		{
			RecieveMessage(onEmptyHandGrabPinchBegin, hand);
		}

		//-------------------------------------------------
		private void OnEmptyHandGrabPinchEnd(Hand hand)
		{
			RecieveMessage(onEmptyHandGrabPinchEnd, hand);
		}

		//-------------------------------------------------
		private void OnEmptyHandGrabPinchUpdate(Hand hand)
		{
			RecieveMessage(onEmptyHandGrabPinchUpdate, hand);
		}

		//-------------------------------------------------
		private void OnEmptyHandGrabGripBegin(Hand hand)
		{
			RecieveMessage(onEmptyHandGrabGripBegin, hand);
		}

		//-------------------------------------------------
		private void OnEmptyHandGrabGripEnd(Hand hand)
		{
			RecieveMessage(onEmptyHandGrabGripEnd, hand);
		}

		//-------------------------------------------------
		private void OnEmptyHandGrabGripUpdate(Hand hand)
		{
			RecieveMessage(onEmptyHandGrabGripUpdate, hand);
		}

		//-------------------------------------------------
		private void OnFullHandGrabPinchBegin(Hand hand)
		{
			RecieveMessage(onFullHandGrabPinchBegin, hand);
		}

		//-------------------------------------------------
		private void OnFullHandGrabPinchEnd(Hand hand)
		{
			RecieveMessage(onFullHandGrabPinchEnd, hand);
		}

		//-------------------------------------------------
		private void OnFullHandGrabPinchUpdate(Hand hand)
		{
			RecieveMessage(onFullHandGrabPinchUpdate, hand);
		}

		//-------------------------------------------------
		private void OnFullHandGrabGripBegin(Hand hand)
		{
			RecieveMessage(onFullHandGrabGripBegin, hand);
		}

		//-------------------------------------------------
		private void OnFullHandGrabGripEnd(Hand hand)
		{
			RecieveMessage(onFullHandGrabGripEnd, hand);
		}

		//-------------------------------------------------
		private void OnFullHandGrabGripUpdate(Hand hand)
		{
			RecieveMessage(onFullHandGrabGripUpdate, hand);
		}
	}
}