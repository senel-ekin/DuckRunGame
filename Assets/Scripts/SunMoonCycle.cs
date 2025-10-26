using UnityEngine;

public class SunMoonCycle : MonoBehaviour
{
    public Transform sun;
    public Transform moon;
    public Transform cameraTransform;

    public float cycleDuration = 60f;
    public float radius = 10f;
    public float heightOffset = 0f;

    public float firstVar = 0.52f;
    public float secondVar = 0.72f;
    public float thirdVar = 1f;
    public Color dayColor;

    public Camera mainCamera;

    private float angle = 0f;

    void Start()
    {
        dayColor = new Color(firstVar, secondVar, thirdVar);
    }
    void Update()
    {
        if (!GamePlayManager.gameStarted) return;

        angle += (360f / cycleDuration) * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        Vector3 center = new Vector3(cameraTransform.position.x, heightOffset, 0f);

        float sunRad = Mathf.Deg2Rad * angle;
        float moonRad = sunRad + Mathf.PI;

        sun.position = center + new Vector3(Mathf.Cos(sunRad), Mathf.Sin(sunRad), 0) * radius;
        moon.position = center + new Vector3(Mathf.Cos(moonRad), Mathf.Sin(moonRad), 0) * radius;

        sun.rotation = Quaternion.identity;
        moon.rotation = Quaternion.identity;

        if(sun.position.y > -4.14f)
{
            firstVar = Mathf.MoveTowards(firstVar, 0f, Time.deltaTime * 0.02f);
            secondVar = Mathf.MoveTowards(secondVar, 0.41f, Time.deltaTime * 0.01f);

            dayColor = new Color(firstVar, secondVar, 1f);
        }

        else if(moon.position.x > -4f && moon.position.y > -4.14f)
        {
            secondVar = Mathf.MoveTowards(secondVar, 0.05f, Time.deltaTime * 0.05f);
            thirdVar = Mathf.MoveTowards(thirdVar, 0.13f, Time.deltaTime * 0.06f);

            dayColor = new Color(firstVar, secondVar, thirdVar);
        }
        else if (moon.position.x > -6.2f && moon.position.x < -4f)
        {
            secondVar = Mathf.MoveTowards(secondVar, 0.41f, Time.deltaTime * 0.1f);
            thirdVar = Mathf.MoveTowards(thirdVar, 1f, Time.deltaTime * 0.2f);
            dayColor = new Color(firstVar, secondVar, thirdVar);
        }
        else if (moon.position.x > -7.3f && moon.position.x < -6.2f)
        {
            firstVar = Mathf.MoveTowards(firstVar, 0.52f, Time.deltaTime * 0.1f);
            secondVar = Mathf.MoveTowards(secondVar, 0.72f, Time.deltaTime * 0.1f);
            dayColor = new Color(firstVar, secondVar, thirdVar);
        }
        mainCamera.backgroundColor = dayColor;
    }
}
