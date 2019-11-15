using UnityEngine;

public class FollowCameraBehaviour : CameraBehaviour
{
    public float m_cameraHorizPosEaseSpeed = 5.0f;
    public float m_cameraVertPosEaseSpeed = 4.0f;
    public float m_lookPosEaseSpeed = 5.0f;

    public Vector3 m_playerMaxDistLocalLookPos = Vector3.zero;
    public Vector3 m_playerMinDistLocalLookPos = Vector3.zero;

    public Vector3 m_playerLocalPivotPos = Vector3.zero;

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

    public override bool UsesStandardControlRotation()
    {
        return false;
    }

    public override void UpdateCamera()
    {
        //player's pivot point.
        Vector3 worldPivotPos = m_player.transform.TransformPoint(m_playerLocalPivotPos);

        //Canmera's desired position is behind the player by OffSetFromPlayer.Magnitude();
        Vector3 offsetFromPlayer = m_goalPos - worldPivotPos;

        //distance
        float distFromPlayer = offsetFromPlayer.magnitude;

        //Get the Camera Rotation based on the input gathered from thirdPersonCamera.
        Vector3 rotateAmount = new Vector3(m_pitchInput * m_pitchRotateSpeed, m_yawInput * m_yawRotateSpeed);

        //get current YRotation:
        Vector3 pivotRotation = m_camera.PivotRotation;

        //XMouse Movement passed on.
        //GetAmount of degrees rotated by last input call.
        pivotRotation.y += rotateAmount.y;

        //pivotRotation.y += m_Player.GroundAngularVelocity.y * Time.deltaTime;

        //Change rotation for YMouse MOvement
        pivotRotation.x -= rotateAmount.x;

        //Clamp so you can't spin vertically infinitely.
        pivotRotation.x = Mathf.Clamp(pivotRotation.x, -m_maxVerticalAngle, m_maxVerticalAngle);

        //Set our camera's angles to the values after input.
        m_camera.PivotRotation = pivotRotation;

        //Clamp a maximum distance for camera to be. 
        distFromPlayer = Mathf.Clamp(distFromPlayer, m_minHorizDistFromPlayer, m_maxDistFromPlayer);

        //Set Camera Position with offset and the rotation from input.
        offsetFromPlayer = Quaternion.Euler(pivotRotation.x, pivotRotation.y, 0.0f) * Vector3.forward;

        //Bring camera out by distance given. this can be modified for different camera behaviours.
        offsetFromPlayer *= distFromPlayer;

        //position for camera to be is in the back of worldPivotPos(Player's pivot point) by offsetFromPlayer
        m_goalPos = offsetFromPlayer + worldPivotPos;

        //Keep local variable of old Camera Position to be changed and applied.
        Vector3 newCameraPosition = m_camera.transform.position;

        //Change only x values to move towards m_GoalPos.
        newCameraPosition = MathUtils.SlerpToHoriz(
            m_cameraHorizPosEaseSpeed,
            newCameraPosition,
            m_goalPos,
            worldPivotPos,
            Time.deltaTime);

        //lerp to GoalPos y position.
        newCameraPosition.y = MathUtils.LerpTo(
            m_cameraVertPosEaseSpeed,
            newCameraPosition.y,
            m_goalPos.y,
            Time.deltaTime);

        //Set Camera's current position to new position now having input applied.
        m_camera.transform.position = newCameraPosition;

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


            m_camera.transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    Vector3 m_goalPos;

    float m_yawInput;
    float m_pitchInput;

    float m_timeTillAutoRotate;
    bool m_allowAutoRotate;
}
