using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform follow;
    public Vector3 offset;
    public Vector3 rotationOffset;
    public float defaultMovementSpeed;
    public float speedMultiplier;

    private Quaternion refRotSmoothDamp;
    private Vector3 startingPos;
    private Vector3 startingEuler;
    private Vector3 refVelocity;
    private Vector3 targetPos;

    private void Awake()
    {
        this.startingPos = this.transform.localPosition;
        this.startingEuler = this.transform.localEulerAngles;
    }

    public void LateUpdate()
    {
        this.Follow();
    }

    private void Follow()
    {
        this.targetPos = new Vector3(this.follow.transform.position.x, 0, this.follow.transform.position.z) + this.offset + this.startingPos;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, this.targetPos
            , ref this.refVelocity, this.defaultMovementSpeed, 500000, Time.unscaledDeltaTime * this.speedMultiplier);
        this.transform.rotation = QuaternionUtil.SmoothDamp(this.transform.rotation, Quaternion.Euler(this.startingEuler + this.rotationOffset), ref this.refRotSmoothDamp, 1f);
    }
}
