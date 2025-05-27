using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private GameObject showHideButton;
    private Image showHideButtonImage;
    [SerializeField] private Sprite hideButtonSprite;
    [SerializeField] private Sprite showButtonSprite;
    [SerializeField] private RectTransform mainRectTransform;
    [SerializeField] private float showHideDuration;
    [SerializeField] private bool interactableButton;
    [SerializeField] private float showWidth;
    [SerializeField] private float hideWidth;
    [SerializeField] private float showPositionX;
    [SerializeField] private float hidePositionX;
    [SerializeField] private bool isShown = true;
    [SerializeField] private GameObject scrollGameObject;
    [SerializeField] private GameObject titleGameObject;

    void Start() 
    {
        mainRectTransform = GetComponent<RectTransform>();
        mainRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, showWidth);
        mainRectTransform.anchoredPosition = new Vector2(showPositionX, 0);
        interactableButton = true;
        showHideButtonImage = showHideButton.GetComponent<Image>();
        showHideButtonImage.sprite = hideButtonSprite;
        
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClickShowHideButton()
    {
        if (isShown==true)
        {
            HideMenuOnClick();
        }
        else
        {
            ShowMenuOnClick();
        }
    }
    
    
    private void ShowMenuOnClick()
    {
        if (interactableButton)
        {
            interactableButton = false;
            float actualWidth = mainRectTransform.rect.width;
            float actualPositionX = mainRectTransform.anchoredPosition.x;
            Sequence showMenu = DOTween.Sequence();
            showMenu.Join(DOTween.To(
                () => actualPositionX,
                x =>
                {
                    actualPositionX = x;
                    mainRectTransform.anchoredPosition = new Vector2(x, 0);
                },
                showPositionX,
                showHideDuration
            ).SetEase(Ease.Linear));
            showMenu.Join(DOTween.To(
                () => actualWidth, 
                x =>
                {
                    actualWidth = x;
                    mainRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
                },
                showWidth,
                showHideDuration
            ).SetEase(Ease.Linear));
            
            showMenu.OnComplete(() =>
            {
                interactableButton = true;
                showHideButtonImage.sprite = hideButtonSprite;
                isShown = true;
            });
        }
    }

    private void HideMenuOnClick()
    {
        if (interactableButton)
        {
            interactableButton = false;
            float actualWidth = mainRectTransform.rect.width;
            float actualPositionX = mainRectTransform.anchoredPosition.x;
            Sequence showMenu = DOTween.Sequence();
            showMenu.Join(DOTween.To(
                () => actualPositionX,
                x =>
                {
                    actualPositionX = x;
                    mainRectTransform.anchoredPosition = new Vector2(x, 0);
                },
                hidePositionX,
                showHideDuration
            ).SetEase(Ease.Linear));
            showMenu.Join(DOTween.To(
                () => actualWidth, 
                x =>
                {
                    actualWidth = x;
                    mainRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
                },
                hideWidth,
                showHideDuration
            ).SetEase(Ease.Linear));
            
            showMenu.OnComplete(() =>
            {
                interactableButton = true;
                showHideButtonImage.sprite = showButtonSprite;
                isShown = false;
            });
        }
    }
}
