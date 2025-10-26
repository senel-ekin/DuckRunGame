using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float speed = 0.5f;
    public float resetX = 12f;
    public float leftLimit = -12f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftLimit)
        {
            Vector3 newPos = transform.position;
            newPos.x = resetX;
            transform.position = newPos;
        }
    }
}