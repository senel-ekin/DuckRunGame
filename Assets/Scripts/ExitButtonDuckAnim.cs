using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ExitButtonDuckAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform duckImage;      
    public RectTransform doorImage;    

    public float startX = -41.2f;         
    public float endX = 44f;             
    public float duckY = -13.6f;         
    public float moveDuration = 1.5f;

    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = new Vector3(0.7f, 0.7f, 1f);

    private Coroutine moveRoutine;

    void Start()
    {
        
        if (duckImage != null)
            duckImage.gameObject.SetActive(false);

        if (doorImage != null)
            doorImage.gameObject.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (duckImage != null) duckImage.gameObject.SetActive(true);
        if (doorImage != null) doorImage.gameObject.SetActive(true);


        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        moveRoutine = StartCoroutine(DuckExit());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (moveRoutine != null)
            StopCoroutine(moveRoutine);
        
        if (duckImage != null) duckImage.gameObject.SetActive(false);
        if (doorImage != null) doorImage.gameObject.SetActive(false);
    }

    IEnumerator DuckExit()
    {
        duckImage.anchoredPosition = new Vector2(startX, duckY);
        duckImage.localScale = startScale;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            float newX = Mathf.Lerp(startX, endX, t);
            duckImage.anchoredPosition = new Vector2(newX, duckY);
            duckImage.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        duckImage.anchoredPosition = new Vector2(endX, duckY);
        duckImage.localScale = endScale;

    }
}
