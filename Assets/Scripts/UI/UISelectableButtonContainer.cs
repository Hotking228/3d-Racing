using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectableButtonContainer : MonoBehaviour
{
    [SerializeField] private Transform buttonsContainer;

    public bool Interactible = true;


    public void SetInteractible(bool interactible)
    {
        Interactible = interactible;
    }


    private UISelectableButton[] buttons;


    private int selectButtonIndex = 0;

    private void Start()
    {
        buttons = buttonsContainer.GetComponentsInChildren<UISelectableButton>();

        if (buttons == null)
        {
            Debug.LogError("Button container is empty");
        }

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].PointerEnter += OnPointerEnter;
            }
        if (!Interactible) return;

        buttons[selectButtonIndex].SetFocus();
    }
    private void OnDestroy()
    {
        for (int i = 0; i < buttons?.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
        }
    }
    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SelectButton(UIButton button)
    {
        if (!Interactible) return;
        buttons[selectButtonIndex].SetUnfocus();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (button == buttons[i])
            {
                selectButtonIndex = i;
                button.SetFocus();
                break;
            }
            

        }
    }

    public void SelectNext()
    {

    }


    public void SelectPrevious()
    {

    }
}
