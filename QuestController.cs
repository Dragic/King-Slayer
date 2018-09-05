using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class QuestController : MonoBehaviour
{
    public TextMeshProUGUI questPoints;
    public Image questPointsFill;
    public QuestItem[] questGroup;
    public Sprite[] qustSprites;

    private bool isExpanded = false;
    private float contentHeight;
    private Transform contentTransform;
    private ScrollRect scroll;

    private void Awake()
    {
        ServerController.OnQuestUpdate += UpdateQuests;

        contentHeight = transform.GetChild(1).GetComponent<RectTransform>().rect.height;
        contentTransform = transform.GetChild(1).GetChild(0).transform;
        scroll = transform.GetChild(1).GetComponent<ScrollRect>();
    }

    private void OnDestroy()
    {
        ServerController.OnQuestUpdate -= UpdateQuests;
    }

    private void OnEnable()
    {
        ServerController.Instance.Get.UpdateQuests();
        SoundManager.THIS.Get.PlayEffect(SoundEffect.Type.OpenQuest);
        //UpdateQuests();
    }

    public void UpdateQuests()
    {
        SetQuestPointsAmount();

        for (int i = 0; i < questGroup.Length; i++)
            questGroup[i].UpdateQuestGroup(i + 1);

    }

    private void SetQuestPointsAmount()
    {
        int qestPointsLimit = StaticData.Instance.Get.FindQuestPointsLimit();
        questPoints.text = MutableString.Start.Add(PlayerData.Instance.questPoints).Add(" / ").Add(qestPointsLimit).ToString();

        questPointsFill.fillAmount = (float)PlayerData.Instance.questPoints / qestPointsLimit;
    }

    /// <summary>
    /// When all of the quests in group is done
    /// </summary>
    public void QuestGroupDone(int i)
    {
        LogController.Log("Group done " + i);
        if (isExpanded)
            StartExpand(i);
    }

    public void StartExpand(int i)
    {
        float mult = isExpanded ? -1 : 1;
        float size = isExpanded ? 81 : contentHeight;
        SoundManager.THIS.Get.PlayEffect(isExpanded ? SoundEffect.Type.HideQuest : SoundEffect.Type.ShowQuest);

        float curY = contentTransform.localPosition.y + mult * 86 * i;

        StopAllCoroutines();
        StartCoroutine(questGroup[i].Expanding(size, mult));
        contentTransform.DOLocalMoveY(curY, .5f);

        TurnScroll();

        isExpanded = !isExpanded;
    }

    private void TurnScroll()
    {
        scroll.movementType = isExpanded ? ScrollRect.MovementType.Elastic : ScrollRect.MovementType.Unrestricted;
        scroll.enabled = isExpanded;
    }

    public void BuyQP()
    {
        ServerController.Instance.Get.BuyQuestPoints();
    }

}