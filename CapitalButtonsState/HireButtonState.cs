using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireButtonState : CapitalButtonStateBase
{
    public void OnEnable()
    {
        SetActive(PlayerData.Instance.unitsData.UnitsCount != 10);
		label.text = LocalizationController.Instance.GetText(IsActive ? "L_HeroesHire" : "L_AllHired");
        FitBGForText();
    }
}
