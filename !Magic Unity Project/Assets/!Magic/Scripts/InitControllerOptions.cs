using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Valve.VR.InteractionSystem.Sample
{
	public class InitControllerOptions : MonoBehaviour
	{
		public Hand hand;
		public UnityEvent onHandInitialized;
		void RenderModel_onControllerLoaded()
		{
			ControllerOptions();
		}

		private void ControllerOptions()
		{
			
		}

		private void OnHandInitialized(int deviceIndex)
		{ 
			RenderModel renderModel = GetComponent<Hand>().GetMainRenderModel();
			renderModel.onControllerLoaded += RenderModel_onControllerLoaded;
			if (hand.skeleton != null)
				hand.skeleton.BlendToPoser(GetComponent<SteamVR_Skeleton_Poser>(), .1f);
			onHandInitialized.Invoke();
		}
	}
}