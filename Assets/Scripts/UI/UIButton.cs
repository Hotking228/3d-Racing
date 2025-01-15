using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected bool Interactible;

    private bool focus = false;
    public bool Focus => focus;


    public event UnityAction<UIButton> PointerEnter;
    public event UnityAction<UIButton> PointerExit;
    public event UnityAction<UIButton> PointerClick;


    public UnityEvent OnClick;
    public virtual void SetFocus()
    {
        if (!Interactible)
        {
            return;
        }

        focus = true;
    }
    public virtual void SetUnfocus()
    {
        if(!Interactible) return;

        focus = false;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactible) return;

        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!Interactible) return;

        PointerExit?.Invoke(this);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!Interactible) return;

        PointerClick?.Invoke(this);
        OnClick?.Invoke();
    }
}
