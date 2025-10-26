using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSpriteChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image targetImage;     
    public Sprite normalSprite;    
    public Sprite hoverSprite;     

    void Start()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        if (targetImage != null && normalSprite != null)
            targetImage.sprite = normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetImage != null && hoverSprite != null)
            targetImage.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetImage != null && normalSprite != null)
            targetImage.sprite = normalSprite;
    }
}
