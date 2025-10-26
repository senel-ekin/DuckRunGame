using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SkyTransitionMenu : MonoBehaviour
{
    public Camera cam;
    public float startY = 5f;  
    public float endY = -5f;
    public float moveSpeed = 0.8f;
    public float waitTime = 2f;
    public Color topColor = new Color(0.3f, 0.6f, 1f);
    public Color bottomColor = new Color(0.7f, 0.9f, 1f);


    public GameObject[] buttons;
    public float buttonDelay = 0.5f;
    public float fadeDuration = 0.5f;

    public GameObject skipButton;
    private bool isSkipping = false;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        cam.transform.position = new Vector3(0, startY, -10);
        cam.backgroundColor = topColor;

        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);

            CanvasGroup cg = btn.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = btn.AddComponent<CanvasGroup>();

            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        if (skipButton != null)
        {
            skipButton.SetActive(false);
            UnityEngine.UI.Button skipBtnComponent = skipButton.GetComponent<UnityEngine.UI.Button>();
            if (skipBtnComponent != null)
            {
                skipBtnComponent.onClick.RemoveAllListeners();
                skipBtnComponent.onClick.AddListener(SkipIntro);
            }
        }

        StartCoroutine(CameraIntro());
    }

    IEnumerator CameraIntro()
    {
        yield return new WaitForSeconds(waitTime);

        if (skipButton != null)
        {
            skipButton.SetActive(true);
        }

        while (cam.transform.position.y > endY && !isSkipping)
        {
            cam.transform.position = Vector3.MoveTowards(
                cam.transform.position,
                new Vector3(0, endY, cam.transform.position.z),
                moveSpeed * Time.deltaTime
            );

            float t = (startY - cam.transform.position.y) / (startY - endY);
            
            cam.backgroundColor = Color.Lerp(topColor, bottomColor, t);

            yield return null;
        }

        cam.transform.position = new Vector3(0, endY, cam.transform.position.z);
        cam.backgroundColor = bottomColor;


        if (skipButton != null)
            skipButton.SetActive(false);

        StartCoroutine(ShowButtonsSequentially());
    }

    void SkipIntro()
    {
        isSkipping = true;
    }

    IEnumerator ShowButtonsSequentially()
    {
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(true);

            CanvasGroup cg = btn.GetComponent<CanvasGroup>();
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                cg.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            cg.alpha = 1;
            UnityEngine.UI.Button buttonComponent = btn.GetComponent<UnityEngine.UI.Button>();
            if (buttonComponent != null)
            {
                buttonComponent.interactable = true;
                cg.interactable = true;
                cg.blocksRaycasts = true;

                if (btn.name == "StartButton")
                    buttonComponent.onClick.AddListener(StartGame);

                if (btn.name == "ExitButton")
                    buttonComponent.onClick.AddListener(ExitGame);
            }
                

            yield return new WaitForSeconds(buttonDelay);
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("Level01");
    }

    void ExitGame()
    {
        Debug.Log("Oyun kapatýlýyor..."); // Unity editörde test için
        Application.Quit();
    }
}

