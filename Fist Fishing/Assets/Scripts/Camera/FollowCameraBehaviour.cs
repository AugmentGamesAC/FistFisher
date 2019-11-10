using UnityEngine;

public class FollowCameraBehaviour : CameraBehaviour
{
    public float CameraHorizPosEaseSpeed = 5.0f;
    public float CameraVertPosEaseSpeed = 4.0f;
    public float LookPosEaseSpeed = 5.0f;

    public Vector3 PlayerMaxDistLocalLookPos = Vector3.zero;
    public Vector3 PlayerMinDistLocalLookPos = Vector3.zero;

    public Vector3 PlayerLocalPivotPos = Vector3.zero;

    public float YawRotateSpeed = 1.0f;
    public float PitchRotateSpeed = 1.0f;
    public float MaxVerticalAngle = 70.0f;

    public float MaxDistFromPlayer = 6.0f;
    public float MinHorizDistFromPlayer = 5.0f;
    public float AutoRotateDelayTime = 1.0f;

    public FollowCameraBehaviour()
    {

    }

    public override void Activate()
    {
        base.Activate();

        m_GoalPos = m_Camera.transform.position;
        m_AllowAutoRotate = false;
        m_TimeTillAutoRotate = AutoRotateDelayTime;
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void UpdateRotation(float yawAmount, float pitchAmount)
    {
        m_YawInput = yawAmount;
        m_PitchInput = pitchAmount;
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
        Vector3 worldPivotPos = m_Player.transform.TransformPoint(PlayerLocalPivotPos);

        //Canmera's desired position is behind the player by OffSetFromPlayer.Magnitude();
        Vector3 offsetFromPlayer = m_GoalPos - worldPivotPos;

        //distance
        float distFromPlayer = offsetFromPlayer.magnitude;

        //Get the Camera Rotation based on the input gathered from thirdPersonCamera.
        Vector3 rotateAmount = new Vector3(m_PitchInput * PitchRotateSpeed, m_YawInput * YawRotateSpeed);

        //don't need this auto rotate stuff.
        //m_TimeTillAutoRotate -= Time.deltaTime;

        //if(!MathUtils.AlmostEquals(rotateAmount.y, 0.0f))
        //{
        //    m_AllowAutoRotate = false;
        //    m_TimeTillAutoRotate = AutoRotateDelayTime;
        //}
        //else if( m_TimeTillAutoRotate <= 0.0f)
        //{
        //    m_AllowAutoRotate = true;
        //}

        //Horizontal Rotation

        //if(m_AllowAutoRotate)
        //{
        //    Vector3 anglesFromPlayer = Quaternion.LookRotation(offsetFromPlayer).eulerAngles;
        //    pivotRotation.y = anglesFromPlayer.y;
        //}
        //else
        //{

        //    Debug.Log(rotateAmount.y);
        //}

        //get current YRotation:
        Vector3 pivotRotation = m_Camera.PivotRotation;

        //XMouse Movement passed on.
        //GetAmount of degrees rotated by last input call.
        pivotRotation.y += rotateAmount.y;

        //pivotRotation.y += m_Player.GroundAngularVelocity.y * Time.deltaTime;

        //Change rotation for YMouse MOvement
        pivotRotation.x -= rotateAmount.x;

        //Clamp so you can't spin vertically infinitely.
        pivotRotation.x = Mathf.Clamp(pivotRotation.x, -MaxVerticalAngle, MaxVerticalAngle);

        //Set our camera's angles to the values after input.
        m_Camera.PivotRotation = pivotRotation;

        //Clamp a maximum distance for camera to be. 
        distFromPlayer = Mathf.Clamp(distFromPlayer, MinHorizDistFromPlayer, MaxDistFromPlayer);

        //Set Camera Position with offset and the rotation from input.
        offsetFromPlayer = Quaternion.Euler(pivotRotation.x, pivotRotation.y, 0.0f) * Vector3.forward;

        //Bring camera out by distance given. this can be modified for different camera behaviours.
        offsetFromPlayer *= distFromPlayer;

        //position for camera to be is in the back of worldPivotPos(Player's pivot point) by offsetFromPlayer
        m_GoalPos = offsetFromPlayer + worldPivotPos;

        //Keep local variable of old Camera Position to be changed and applied.
        Vector3 newCameraPosition = m_Camera.transform.position;

        //Change only x values to move towards m_GoalPos.
        newCameraPosition = MathUtils.SlerpToHoriz(
            CameraHorizPosEaseSpeed,
            newCameraPosition,
            m_GoalPos,
            worldPivotPos,
            Time.deltaTime);

        //lerp to GoalPos y position.
        newCameraPosition.y = MathUtils.LerpTo(
            CameraVertPosEaseSpeed,
            newCameraPosition.y,
            m_GoalPos.y,
            Time.deltaTime);

        //Set Camera's current position to new position now having input applied.
        m_Camera.transform.position = newCameraPosition;

        //Makes camera move infront of obstacle so you can always see the player.
        HandleObstacles();

        //Get Camera Rotation to look towards new player position. LookPos(Player's position set in ThirdPersonCamera.SetPlayer(m_player)).
        m_Camera.LookPos = MathUtils.LerpTo(
          LookPosEaseSpeed,
          m_Camera.LookPos,
          worldPivotPos,
          Time.deltaTime);

        //Get Vector3 that's between playerPos and Camera Position.
        Vector3 lookDir = m_Camera.LookPos - m_Camera.transform.position;

        //Set the Camera's current rotation to the new found rotation lerped on that frame.
        m_Camera.transform.rotation = Quaternion.LookRotation(lookDir);
    }

    Vector3 m_GoalPos;

    float m_YawInput;
    float m_PitchInput;

    float m_TimeTillAutoRotate;
    bool m_AllowAutoRotate;
}
