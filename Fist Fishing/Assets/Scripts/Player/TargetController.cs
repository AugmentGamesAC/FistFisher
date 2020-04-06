using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public List<GameObject> m_fishInViewList;

    public GameObject m_targetedFish;

    public GameObject m_targetPrefab;

    public PlayerMotion m_playerRef;

    public ThirdPersonCamera m_camera;

    public bool m_targetingIsActive;

    public int m_fishInViewCount;

    public int m_currentFishTargetIndex;

    public float m_closestFishDistance;

    public float m_targetCloseness = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        m_fishInViewList = new List<GameObject>();

        m_camera = Camera.main.GetComponent<ThirdPersonCamera>();

        m_closestFishDistance = float.MaxValue;

        m_currentFishTargetIndex = -1;
        m_targetPrefab = Instantiate(m_targetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        m_targetPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_fishInViewCount = m_fishInViewList.Count;

        //List will be filled by each fish in view of the camera.
        //List is empty, don't update and deactivate targeting.
        if (m_fishInViewList.Count == 0)
        {
            ToggleTargetingReticle(false);
            return;
        }

        //if (ALInput.GetKeyDown(ALInput.KeyTarget))
        //    ToggleTargeting();

        //if (ALInput.GetKeyDown(ALInput.ForgetTarget))
        //    SelectNextTarget();


        if (m_targetedFish == null)
            return;

        if (!m_targetedFish.activeSelf)
        {
            ToggleTargetingReticle(false);
        }

        m_closestFishDistance = Vector3.Distance(m_targetedFish.transform.position, m_playerRef.transform.position);

        LockOn();
    }

    private void LateUpdate()
    {
        if(m_targetedFish == null)
        {
            SetTargetedFishToClosest();
        }

        if (m_targetingIsActive) //please don't spam errors
        {
            Vector3 targetPos = m_targetedFish.gameObject.transform.position;
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 newPos = Vector3.Lerp(cameraPos, targetPos, m_targetCloseness);

            m_targetPrefab.gameObject.transform.position = newPos;
        }

        //if targeting is off, return.
        if (!m_targetingIsActive)
            return;

        //Get a point between player and target.


        //Lerp camera direction towards NewCameraDir.
        //m_camera.SetFacingDirection(NewCameraDir);

        //m_camera.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void ToggleTargetingReticle(bool targetingIsActive)
    {
        m_targetingIsActive = targetingIsActive;
        m_targetPrefab.SetActive(m_targetingIsActive);
    }

    private void ForgetCurrentTarget()
    {
        m_targetedFish = null;
        ToggleTargetingReticle(false);
    }

    private void ToggleTargeting()
    {
        //update on a timer so it doesn't toggle all the time.
        if (m_targetingIsActive)
        {
            //turn off targeting.
            ToggleTargetingReticle(false);
        }
        //when targeting is off, check for closest fish and set targeting to true.
        else
        {
            SetTargetedFishToClosest();

            //turn on targeting.
            ToggleTargetingReticle(true);
        }
    }

    //Sets targeted fish to the closest one.
    private GameObject SetTargetedFishToClosest()
    {
        if (m_fishInViewList.Count == 0)
            return null;

        m_closestFishDistance = float.MaxValue;

        for (int i = 0; i < m_fishInViewList.Count; i++)
        {
            if (m_fishInViewList[i] == null)
                continue;

            float tempFishPlayerDist = Vector3.Distance(m_fishInViewList[i].transform.position, m_playerRef.transform.position);

            //if fishPlayerDist is bigger than last, replace it and keep
            if (m_closestFishDistance > tempFishPlayerDist)
            {
                m_closestFishDistance = tempFishPlayerDist;
                m_targetedFish = m_fishInViewList[i];
                m_currentFishTargetIndex = i;
            }
        }
        return m_targetedFish;
    }

    //Camera stuff.
    private void LockOn()
    {
        //change look direction to a point in between the target and the player. Trying to keep the player in sight.
        //m_camera.gameObject.transform.LookAt(m_targetedFish.gameObject.transform.position);
    }

    private void LockOff()
    {
        //Go to original lookPos.
    }

    //Sets m_targeted to next fish on the screen.
    private void SelectNextTarget()
    {
        if (!m_targetingIsActive)
            return;

        //Forward into the list. 
        m_currentFishTargetIndex++;

        //keep index inside the list bounds.
        m_currentFishTargetIndex %= m_fishInViewList.Count;

        //Set new Target.
        m_targetedFish = m_fishInViewList[m_currentFishTargetIndex];
    }

    //Sets m_targeted to previous fish on the screen.
    private void SelectLastTarget()
    {
        if (!m_targetingIsActive)
            return;

        //back into the list.
        m_currentFishTargetIndex--;

        //keep index inside the list bounds.
        m_currentFishTargetIndex %= m_fishInViewList.Count;

        if (m_currentFishTargetIndex < 0)
        {
            m_currentFishTargetIndex = m_fishInViewList.Count - 1;
        }

        //set new target.
        m_targetedFish = m_fishInViewList[m_currentFishTargetIndex];
    }
}
