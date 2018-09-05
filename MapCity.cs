using UnityEngine;
using DG.Tweening;

public class MapCity : MonoBehaviour, IObjectClick
{
    public int ID;

    [SerializeField] float speed;
    [SerializeField] Transform circle;

    SpriteRenderer renderer;

    private void OnEnable()
    {
        TweenParams tParms = new TweenParams().SetLoops(-1).SetEase(Ease.Linear);

        circle.DOLocalRotate(new Vector3(30, 0, 360), speed, RotateMode.FastBeyond360).SetAs(tParms);

        renderer = circle.GetComponent<SpriteRenderer>();
    }

    public void UpdateCity()
    {
        var curCity = StaticData.Instance.Get.GetCity(ID);

        //City is captured
        if (PlayerData.Instance.citiesData.IsCapturedCity(ID))
        {
            renderer.color = Color.white;
        }
        //City is ready to capture
        else if (PlayerData.Instance.citiesData.IsCapturedCity(curCity.condition_city_id))
        {
            renderer.color = Color.red;
        }
        else
        {
            renderer.color = Color.clear;
        }
    }

    public void Pressed()
    {
        UIController.Instance.Get.ActiveWindow(WindowNames.CITY_CAPTURE);
        CityCapture.Instance.Get.UpdateWindow(ID);
        CamCapture.SetPos(transform);
    }
}
