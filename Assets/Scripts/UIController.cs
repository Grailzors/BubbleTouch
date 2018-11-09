using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public MenuType.menuType type = MenuType.menuType.main;
    [Space]

    [Header("UI Components")]
    public Text[] text;
    public Image[] image;
    public Sprite toggleSprite1;
    public Sprite toggleSprite2;

    [Header("UI Controls")]
    public bool resetSprite = false;
    public bool isButton = false;
    public float fadeTime = 1f;

    private float fade;

    private void Start()
    {
        fade = 0f;
    }

    private void LateUpdate()
    {
        FadeControl();
        FadeUI();
        InteractableControl(isButton);
        ResetSprite();
    }

    void InteractableControl(bool isButton)
    {
        if (isButton)
        {
            if (type == UIManager.currentMenu)
            {
                foreach (Image i in image)
                {
                    i.raycastTarget = true;
                }

                GetComponent<Button>().interactable = true;
            }
            else
            {
                foreach (Image i in image)
                {
                    i.raycastTarget = false;
                }

                GetComponent<Button>().interactable = false;
            }
        }

    }

    void FadeControl()
    {
        if (type == UIManager.currentMenu)
        {
            fade += fadeTime * (Time.deltaTime / 5);
        }
        else
        {
            fade -= fadeTime * (Time.deltaTime / 5);
        }

        fade = Mathf.Clamp(fade, 0f, 1f);
    }

    void FadeUI()
    {
        if (text.Length > 0)
        {
            foreach (Text t in text)
            {
                t.color = new Color(t.color.r,
                    t.color.g,
                    t.color.b,
                    fade);
            }
        }

        if (image.Length > 0)
        {
            foreach (Image i in image)
            {
                i.color = new Color(i.color.r,
                    i.color.g,
                    i.color.b,
                    fade);
            }
        }
    }

    public void ToggleSprite(Sprite sprite)
    {
        if (GetComponent<Image>().sprite == toggleSprite2)
        {
            GetComponent<Image>().sprite = sprite;
        }
        else
        {
            GetComponent<Image>().sprite = toggleSprite2;
        }
    }

    void ResetSprite()
    {
        if (resetSprite && type != UIManager.currentMenu)
        {
            GetComponent<Image>().sprite = toggleSprite1;
        }
    }

}
