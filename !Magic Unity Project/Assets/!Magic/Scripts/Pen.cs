using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Valve.VR.InteractionSystem
{
	public class Pen : MonoBehaviour
	{

		public bool drawing = false;
		public UnityEvent OnActionDown;
		public UnityEvent OnActionUp;

		// Update is called once per frame the object is attached to a hand
		void HandAttachedUpdate(Hand hand)
		{
			GrabTypes bestGrab = hand.GetBestGrabbingType(GrabTypes.Pinch, true);
			if (!drawing && bestGrab != GrabTypes.None)
			{
				drawing = true;
				OnActionDown.Invoke();
			}
			else if (drawing && bestGrab == GrabTypes.None)
			{
				drawing = false;
				OnActionUp.Invoke();
			}
		}
	}
}