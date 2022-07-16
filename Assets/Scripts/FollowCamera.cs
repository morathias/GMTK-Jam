using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public static FollowCamera Instance
    {
        get;
        private set;
    }

    public Plane plane;
    
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
        plane.SetNormalAndPosition(Vector3.up, follow.position);
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
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float distance);
        Vector3 worldPosMouse = ray.GetPoint(distance);
        Vector3 toFollowPos = follow.transform.position;
        Vector3 finalPos = (toFollowPos + (toFollowPos + (worldPosMouse - toFollowPos)  * 0.5f)) / 2f;
        this.targetPos = new Vector3(finalPos.x, 0, finalPos.z) + this.offset + this.startingPos;
        this.transform.position = Vector3.SmoothDamp(this.transform.position, this.targetPos
            , ref this.refVelocity, this.defaultMovementSpeed, 500000, Time.unscaledDeltaTime * this.speedMultiplier);
        this.transform.rotation = QuaternionUtil.SmoothDamp(this.transform.rotation, Quaternion.Euler(this.startingEuler + this.rotationOffset), ref this.refRotSmoothDamp, 1f);
    }
}
