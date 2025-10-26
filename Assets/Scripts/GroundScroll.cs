using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public Transform[] grounds;  // tüm ground parçalarý (4 tane)
    public float scrollSpeed = 5f;
    public float groundWidth = 19.2f; // Sprite geniþliði * scale
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!GamePlayManager.gameStarted) return; //Son Eklenen Kod
        float screenLeft = cam.transform.position.x - cam.orthographicSize * cam.aspect;

        foreach (Transform ground in grounds)
        {
            // Sola kaydýr
            ground.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // Ekranýn solunu geçtiyse
            if (ground.position.x + groundWidth < screenLeft)
            {
                // Listenin en saðýndaki ground’u bul
                float maxX = float.MinValue;
                foreach (Transform g in grounds)
                {
                    if (g.position.x > maxX)
                        maxX = g.position.x;
                }

                // Bu ground’u en saðdaki ground’un arkasýna taþý
                ground.position = new Vector3(maxX + groundWidth, ground.position.y, ground.position.z);
            }
        }
    }
}


