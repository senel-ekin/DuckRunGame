using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public Transform[] grounds;  // t�m ground par�alar� (4 tane)
    public float scrollSpeed = 5f;
    public float groundWidth = 19.2f; // Sprite geni�li�i * scale
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
            // Sola kayd�r
            ground.position += Vector3.left * scrollSpeed * Time.deltaTime;

            // Ekran�n solunu ge�tiyse
            if (ground.position.x + groundWidth < screenLeft)
            {
                // Listenin en sa��ndaki ground�u bul
                float maxX = float.MinValue;
                foreach (Transform g in grounds)
                {
                    if (g.position.x > maxX)
                        maxX = g.position.x;
                }

                // Bu ground�u en sa�daki ground�un arkas�na ta��
                ground.position = new Vector3(maxX + groundWidth, ground.position.y, ground.position.z);
            }
        }
    }
}


