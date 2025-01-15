using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image selectImage;

    public UnityEvent OnSelect;
    public UnityEvent OnUnSelect;
    private void Start()
    {
        SetUnfocus();
    }
    public override void SetFocus()
    {
        base.SetFocus();

        if(selectImage != null)
            selectImage.enabled = true;

        OnSelect?.Invoke();
    }

    public override void SetUnfocus()
    {
        base.SetUnfocus();
        if (selectImage != null)
            selectImage.enabled = false;
        OnUnSelect?.Invoke();
    }

}
