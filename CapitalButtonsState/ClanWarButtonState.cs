using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanWarButtonState : CapitalButtonStateBase
{
    void OnEnable()
    {
        SetActive(ClanController.IsUserHaveClan);

        label.text = LocalizationController.Instance.GetText(ClanController.IsUserHaveClan ? "L_ClanWar" : "L_YouHaventClan");
        FitBGForText();
    }
}
