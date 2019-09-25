using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Hand))]
	public class CastingHandEvents : HandEvents
	{
		public Vector2 m_Axis = new Vector2();
		public GameObject m_Head;
		public override void OnAxisChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
		{
			m_Axis = axis;
			base.OnAxisChange(fromAction, fromSource, axis, delta);
		}

		public override void OnPadClick(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
		{
			float rotation = m_Axis.x > 0 ? 1 : -1;
			m_Player.gameObject.transform.RotateAround(m_Head.transform.position, Vector3.up, 45f * rotation);
			base.OnPadClick(fromAction, fromSource);
		}

		public override void OnPadTouchRelease(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
		{
			m_Axis.Set(0f, 0f);
			base.OnPadTouchRelease(fromAction, fromSource);
		}
	}
}