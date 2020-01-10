using UnityEngine;

[System.Serializable]
public class FollowCameraBehaviour : CameraBehaviour
{
    public float m_cameraHorizPosEaseSpeed = 5.0f;
    public float m_cameraVertPosEaseSpeed = 4.0f;
    public float m_lookPosEaseSpeed = 5.0f;

    public Vector3 m_playerMaxDistLocalLookPos = Vector3.zero;
    public Vector3 m_playerMinDistLocalLookPos = Vector3.zero;

    public Vector3 m_playerLocalPivotPos = new Vector3(0.0f, 3.0f, 0.0f);

    public float m_yawRotateSpeed = 1.0f;
    public float m_pitchRotateSpeed = 1.0f;
    public float m_maxVerticalAngle = 70.0f;

    public float m_maxDistFromPlayer = 6.0f;
    public float m_minHorizDistFromPlayer = 5.0f;

    public override void Activate()
    {
        base.Activate();

        m_goalPos = m_camera.transform.position;
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void UpdateRotation(float yawAmount, float pitchAmount)
    {
        m_yawInput = yawAmount;
        m_pitchInput = pitchAmount;
    }

    public override void SetFacingDirection(Vector3 direction)
    {

    }

    public override Vector3 GetControlRotation()
    {
        return base.GetControlRotation();
    }



    class dummyCamera : MonoBehaviour
    {
        orbitpoint MyPoint;
        orbitpoint LookatPoint;

        GameObject PivotPoit;

        float m_RotationSpeed;
        /// <summary>
        /// update mypoint and lookatpoint when needed
        /// set the transform of the camera to the location and rotation for those point
        /// </summary>
        /// <param name="n"></param>
        /// <param name="nn"></param>
        void update(float n, float nn)
        {

        }
    }

    class orbitpoint
    {
        float YawRotationAroundPivit;
        float PitchRotationAroundPivit;
        float distanceFromPivot;

        void Increment(float n, float nn) { }
        Vector3 ReturnTargetPoint() { return default; }
    }

    public override void UpdateCamera()
    {
        //player's pivot point.
        Vector3 worldPivotPos = m_player.transform.TransformPoint(m_playerLocalPivotPos);


     { // new camera rotation point based on increment 
        //incrcement rotation
        m_camera.PivotRotation.y += m_yawInput * m_yawRotateSpeed;

        //increment rotation & clamp
        m_camera.PivotRotation.x = Mathf.Clamp(m_camera.PivotRotation.x - (m_pitchInput * m_pitchRotateSpeed), -m_maxVerticalAngle, m_maxVerticalAngle);
    }

    Vector3 offsetFromPlayer;
        float distFromPlayer;
        { // maintaing distance from player

            //Canmera's desired position is behind the player by OffSetFromPlayer.Magnitude();
            offsetFromPlayer = m_goalPos - worldPivotPos;
            //distance
            distFromPlayer = offsetFromPlayer.magnitude;

            //Clamp a maximum distance for camera to be. 
            distFromPlayer = Mathf.Clamp(distFromPlayer, m_minHorizDistFromPlayer, m_maxDistFromPlayer);
        }
        //Set Camera Position with offset and the rotation from input.
         offsetFromPlayer = Quaternion.Euler(m_camera.PivotRotation.x, m_camera.PivotRotation.y, 0.0f) * Vector3.forward;

        //Bring camera out by distance given. this can be modified for different camera behaviours.
        offsetFromPlayer *= distFromPlayer;

        m_camera.transform.position = offsetFromPlayer + worldPivotPos;


        ////position for camera to be is in the back of worldPivotPos(Player's pivot point) by offsetFromPlayer
        //m_goalPos = offsetFromPlayer + worldPivotPos;
        //{
        //    //Keep local variable of old Camera Position to be changed and applied.
        //    Vector3 newCameraPosition = m_camera.transform.position;

        //    //Change only x values to move towards m_GoalPos.
        //    newCameraPosition = MathUtils.SlerpToHoriz(
        //        m_cameraHorizPosEaseSpeed,
        //        newCameraPosition,
        //        m_goalPos,
        //        worldPivotPos,
        //        Time.deltaTime);

        //    //lerp to GoalPos y position.
        //    newCameraPosition.y = MathUtils.LerpTo(
        //        m_cameraVertPosEaseSpeed,
        //        newCameraPosition.y,
        //        m_goalPos.y,
        //        Time.deltaTime);

        //    //Set Camera's current position to new position now having input applied.
        //    m_camera.transform.position = newCameraPosition;
        //}


        //Deal with obstacles
        float moveUpDist = HandleObstacles();

        //Update Look Position
        {
            float lookPosPercent = moveUpDist / m_maxDistFromPlayer;
            Vector3 localLookPos = Vector3.Lerp(m_playerMinDistLocalLookPos, m_playerMaxDistLocalLookPos, lookPosPercent);

            Vector3 goalLookPos = m_player.transform.TransformPoint(localLookPos);

                //goalLookPos.y = m_Camera.LookPos.y;

            m_camera.LookPos = MathUtils.LerpTo(
                m_lookPosEaseSpeed,
                m_camera.LookPos,
                goalLookPos,
                Time.deltaTime
                );

            Vector3 lookDir = m_camera.LookPos - m_camera.transform.position;
            if(lookDir!= Vector3.zero)
                m_camera.transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    Vector3 m_goalPos;

    float m_yawInput;
    float m_pitchInput;

    float m_timeTillAutoRotate;
    bool m_allowAutoRotate;
}
