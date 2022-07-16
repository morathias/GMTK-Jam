using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static FollowCamera Instance
    {
        get;
        private set;
    }

    public Camera camera;
    public Transform follow;
    public Vector3 offset;
    public Vector3 rotationOffset;
    public float defaultMovementSpeed;
    public float speedMultiplier;
    public bool pause;

    private Quaternion refRotSmoothDamp;
    private Vector3 startingPos;
    private Vector3 startingEuler;
    private Vector3 refVelocity;
    private Vector3 targetPos;

    private void Awake()
    {
        Instance = this;

        this.startingPos = this.transform.localPosition;
        this.startingEuler = this.transform.localEulerAngles;
    }

    public void LateUpdate()
    {
        if (!this.pause)
        {
            this.Follow();
        }
    }

    private void Follow()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosMouse = this.camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Vector3.Distance(this.transform.position, this.follow.position)));
        Vector3 toFollowPos = new Vector3(this.follow.transform.position.x, this.follow.transform.position.y, this.follow.transform.position.z);
        Vector3 finalPos = (toFollowPos + worldPosMouse * 0.5f) / 2f;
        this.targetPos = new Vector3(finalPos.x, 0, finalPos.z) + this.offset + this.startingPos;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, this.targetPos
            , ref this.refVelocity, this.defaultMovementSpeed, 500000, Time.unscaledDeltaTime * this.speedMultiplier);
        this.transform.rotation = QuaternionUtil.SmoothDamp(this.transform.rotation, Quaternion.Euler(this.startingEuler + this.rotationOffset), ref this.refRotSmoothDamp, 1f);
    }
}
