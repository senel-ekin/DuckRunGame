using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ördek
    public float initialY;
    public float smoothSpeed = 0.125f; // Takip hýzý
    public float offsetX; // Kameranýn ördeðe göre x farký


    void Start()
    {
        if (target == null) return;

        initialY = transform.position.y;
        offsetX = transform.position.x - target.position.x;
    }


    void LateUpdate()
    {
        if (!GamePlayManager.gameStarted || target == null) return; //Son Eklenen Kod

        float targetX = target.position.x + offsetX;
        Vector3 desiredPosition = new Vector3(targetX, initialY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
