using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Valve.VR.InteractionSystem
{
	public class Pen : MonoBehaviour
	{
		public UnityEvent OnActionDown;
		public UnityEvent OnActionUp;

		GrabTypes DrawInput = GrabTypes.Pinch;

		// Update is called once per frame the object is attached to a hand
		void HandAttachedUpdate(Hand hand)
		{
			GrabTypes grab = hand.GetGrabStarting(DrawInput);
			GrabTypes release = hand.GetGrabEnding(DrawInput);
			if (grab != GrabTypes.None)
			{
				OnActionDown.Invoke();
			}
			if (release != GrabTypes.None)
			{
				OnActionUp.Invoke();
			}
		}
	}
}