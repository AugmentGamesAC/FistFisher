using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

using AirSig;

public class GestureHandle : MonoBehaviour {

    // Reference to AirSigManager for setting operation mode and registering listener
    public AirSigManager airsigManager;


	protected ParticleSystem GetTrack()
	{
		return airsigManager.GetCastingHand().GetComponentInChildren<ParticleSystem>(false);
	}

	protected void checkDbExist()
	{
        bool isDbExist = airsigManager.IsDbExist;
        if (!isDbExist)
            Debug.LogError("Cannot find DB files!\nMake sure\n'Assets/AirSig/StreamingAssets'\nis copied to\n'Assets/StreamingAssets'");
    }

    protected void UpdateUIandHandleControl()
	{
		
    }

}
