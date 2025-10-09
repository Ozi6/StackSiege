using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 3f;

    private float initialCameraY;
    private float initialTargetY;
    private float initialX;
    private float initialZ;

    void Start()
    {
        initialCameraY = transform.position.y;
        initialTargetY = target.position.y;
        initialX = transform.position.x;
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        float yOffset = target.position.y - initialTargetY;
        float targetCameraY = initialCameraY + yOffset;
        Vector3 targetPos = new Vector3(initialX, targetCameraY, initialZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
