using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CapitalButtonStateBase : MonoBehaviour
{
    [SerializeField]protected TMPro.TextMeshPro label;
    [SerializeField]protected SpriteRenderer bgImage;
    [SerializeField] float offset;

    public bool IsActive { get; set; }

    public void SetActive(bool isActive)
    {
        bgImage.sprite = MainSceneStorage.GetCityBGSprite(isActive);
        IsActive = isActive;
    }

    protected void FitBGForText()
    {
        float width = label.GetPreferredValues(0, label.rectTransform.sizeDelta.y).x + offset;
        bgImage.size = new Vector2(width / 0.3f,  bgImage.size.y);
    }
}
