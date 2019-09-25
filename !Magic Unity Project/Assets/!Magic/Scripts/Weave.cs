using System.Collections.Generic;
using UnityEngine;
using AirSig;

public class Weave : GestureHandle {

    // Gesture index to use for training and verifying custom gesture. Valid range is between 1 and 1000
    // Beware that setting to 100 will overwrite your player signature.
    const int STARTING_ID = 101;
    List<int> m_GestureIDs = new List<int>();
	List<int> m_AllGestureIDs = new List<int>();
	private int m_QueuedSpell = 0;
	private Valve.VR.InteractionSystem.Hand m_QueuedWeavingHand;

	// How many gesture we need to collect for each gesture type
    int DEFAULT_SAMPLES = 5;
	int SAMPLES;

	// Use these steps to iterate gesture when train 'Smart Train' and 'Custom Gesture'
	int currentPlayerGestureTarget; // 101 = heart, 102 = down

	// Callback for receiving signature/gesture progression or identification results
	AirSigManager.OnPlayerGestureMatch playerGestureMatch;
	AirSigManager.OnPlayerGestureAdd playerGestureAdd;

    public void SetSpellSelectionToAll()
    {
        m_GestureIDs = m_AllGestureIDs;
    }

    public void SetSpellSelectionIDs(List<int> SpellSelection)
    {
        m_GestureIDs = SpellSelection;
    }

	// Handling developer defined gesture match callback - This is invoked when the Mode is set to Mode.DeveloperDefined and a gesture is recorded.
	// gestureId - a serial number
	// gesture - gesture matched or null if no match. Only guesture in SetDeveloperDefinedTarget range will be verified against
	void HandleOnPlayerGestureMatch(long gestureId, int match)
	{
		if (gestureId == 0)
			return;

		// Do something //
		Debug.Log(string.Format("<color=purple>Gesture Match: {0}</color>", match));

		// Check whether this gesture match any custom gesture in the database
		float[] data = airsigManager.GetFromCache(gestureId);
		bool gestureExsists = airsigManager.IsPlayerGestureExisted(data);

		if (gestureExsists)
			Debug.Log("Gesture exsists in DB");

		m_QueuedSpell = match;
	}

	// Handling custom gesture adding callback - This is invoked when the Mode is set to Mode.AddPlayerGesture and a gesture is
	// recorded. Gestures are only added to a cache. You should call SetPlayerGesture() to actually set gestures to database.
	// gestureId - a serial number
	// result - return a map of all un-set custom gestures and number of gesture collected.
	void HandleOnPlayerGestureAdd(long gestureId, Dictionary<int, int> result)
	{
		print(string.Format("Current Gusture Target {0}", currentPlayerGestureTarget));
		int count = result[currentPlayerGestureTarget];
		if (count >= SAMPLES)
		{
			airsigManager.SetPlayerGesture(m_AllGestureIDs, false);
            airsigManager.DeletePlayerRecord(currentPlayerGestureTarget);
            SAMPLES = DEFAULT_SAMPLES;
            currentPlayerGestureTarget++;
            SwitchToIdentify();
		}
		else
		{
			print(string.Format("gesture {0}/{1} entered", count, SAMPLES));
			airsigManager.SetTarget(new List<int> { currentPlayerGestureTarget });
		}
	}

	void SwitchToIdentify()
	{
		print(m_GestureIDs);
		airsigManager.SetMode(AirSigManager.Mode.IdentifyPlayerGesture);
		airsigManager.SetTarget(m_GestureIDs);
	}

	// Set gesture training collection samples
	public void SetSamples(int samples)
	{
        DEFAULT_SAMPLES = samples;
		SAMPLES = DEFAULT_SAMPLES;
	}

	// Add gesture and specify training samples
	public void AddGestureWithSamples(int samples)
	{
		SAMPLES = samples;
		AddGesture();
	}

	// Add gesture through sample collection. Begins collecting samples
	public void AddGesture()
	{
		int target = STARTING_ID + m_AllGestureIDs.Count;
        m_AllGestureIDs.Add(target);
		airsigManager.SetMode(AirSigManager.Mode.AddPlayerGesture);
		airsigManager.SetTarget(m_AllGestureIDs);
		currentPlayerGestureTarget = target;
	}

	// Use this for initialization
	void Awake()
	{
		// Configure AirSig by specifying target 
		playerGestureAdd = new AirSigManager.OnPlayerGestureAdd(HandleOnPlayerGestureAdd);
		playerGestureMatch = new AirSigManager.OnPlayerGestureMatch(HandleOnPlayerGestureMatch);
		airsigManager.onPlayerGestureAdd += playerGestureAdd;
		airsigManager.onPlayerGestureMatch += playerGestureMatch;

		// Set AirSig mode
        airsigManager.SetMode(AirSigManager.Mode.IdentifyPlayerGesture);
        //airsigManager.SetPlayerGesture(new List<int> { });
        //airsigManager.SetClassifier("SampleGestureProfile", "");

        SAMPLES = DEFAULT_SAMPLES;

        checkDbExist();
    }


    void OnDestroy()
	{
        // Unregistering callback
		airsigManager.onPlayerGestureAdd -= playerGestureAdd;
		airsigManager.onPlayerGestureMatch -= playerGestureMatch;

		// Remove gesture data
		for (int i = 0; i < m_GestureIDs.Count - 1; i++)
			airsigManager.DeletePlayerRecord(i);
	}

    void Update()
	{
        UpdateUIandHandleControl();
    }

	// Resolve spell after recieving matching spell id. Used to call Unity methods outside of spellcasting thread
	void LateUpdate()
	{
		int spellID = m_QueuedSpell;
		Valve.VR.InteractionSystem.Hand hand = m_QueuedWeavingHand;
		m_QueuedSpell = 0;
		m_QueuedWeavingHand = null;

		if (spellID == 0)
			return;

		if (spellID == -1)
		{
			print("FAIL!");
			airsigManager.GetCastingHand().gameObject.GetComponent<FailParticle>().m_FailParticle.gameObject.SetActive(true);
		}
		
		// TODO exit point for Weave
		//airsigManager.m_Player.spellManager.AimSpell(spellID);
	}

	// Begin collecting gesture data
	public void StartWeaving(Valve.VR.InteractionSystem.Hand hand)
	{
		if (airsigManager.m_QueueHandDataCollection)
		{
			if (airsigManager.m_Player.GetHandedness() != hand.handType)
				return;

			if (airsigManager.m_Player.isRightHanded)
				airsigManager.startRightHandCollecting();
			else
				airsigManager.startLeftHandCollecting();

			GetTrack().Play();
		}
	}

	// Stop collecting gesture data
	public void StopWeaving(Valve.VR.InteractionSystem.Hand hand)
	{
		if (airsigManager.m_QueueHandDataCollection)
		{
			if (airsigManager.m_Player.GetHandedness() != hand.handType)
				return;

			if(airsigManager.m_Player.isRightHanded)
				airsigManager.stopRightHandCollecting();
			else
				airsigManager.stopLeftHandCollecting();

			m_QueuedWeavingHand = hand;

			GetTrack().Stop();
			GetTrack().Clear();
		}
	}

}