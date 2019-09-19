using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem.Sample
{
	[RequireComponent(typeof(Valve.VR.InteractionSystem.Sample.SkeletonUIOptions))]
	public class InitControllerOptions : MonoBehaviour
	{
		private static Valve.VR.InteractionSystem.Sample.SkeletonUIOptions m_SkeletonUIOptions;

		void RenderModel_onControllerLoaded()
		{
			ControllerOptions();
		}

		private void ControllerOptions()
		{
			m_SkeletonUIOptions = GetComponent<Valve.VR.InteractionSystem.Sample.SkeletonUIOptions>();
			m_SkeletonUIOptions.HideController();
			m_SkeletonUIOptions.AnimateHandWithoutController();
		}

		private void OnHandInitialized(int deviceIndex)
		{ 
			RenderModel renderModel = GetComponent<Hand>().GetMainRenderModel();
			renderModel.onControllerLoaded += RenderModel_onControllerLoaded;
		}
	}
}