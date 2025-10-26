using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static bool gameStarted = false;

    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                gameStarted = true;
            }
        }
    }
}

