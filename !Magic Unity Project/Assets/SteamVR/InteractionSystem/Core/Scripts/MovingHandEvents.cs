using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Hand))]
	public class MovingHandEvents : HandEvents
	{
		public Vector2 m_Axis = new Vector2();
		public Camera m_Camera;
		private bool m_Moving = false;
		public float m_MoveSpeed = 100f;

		public override void OnAxisChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
		{
			m_Axis = axis;
			base.OnAxisChange(fromAction, fromSource, axis, delta);
		}

		public override void OnPadTouchUpdate(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
		{
			if (!m_Moving)
				return;

			Vector3 orientationEuler = new Vector3(0f, m_Camera.transform.rotation.eulerAngles.y, 0f);
			Quaternion orientation = Quaternion.Euler(orientationEuler);

			Vector3 movement = Vector3.zero;

			movement += orientation * (2f * m_Axis.y * Vector3.forward) * Time.deltaTime;
			movement += orientation * (2f * m_Axis.x * Vector3.right) * Time.deltaTime;
			m_Player.GetComponent<Rigidbody>().velocity = movement * m_MoveSpeed;

			//Hand[] hands = m_Player.hands;
			//foreach(Hand hand in hands)
			//{
			//	if(hand && hand.currentAttachedObject)
			//		hand.currentAttachedObject.transform.position += movement;
			//}
		}

		public override void OnPadClick(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
		{
			m_Moving = true;

			base.OnPadClick(fromAction, fromSource);
		}

		public override void OnPadTouchRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
		{
			m_Moving = false;
			m_Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
			m_Axis.Set(0f, 0f);
			base.OnPadTouchRelease(fromAction, fromSource);
		}
	}
}