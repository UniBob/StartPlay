using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PFCUIListner : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] Slider protein;
    [SerializeField] Slider fat;
    [SerializeField] Slider carbon;
    [SerializeField] Slider kcal;

    PlayerPFCKeeper keeperPFC;
    public delegate void PFCUIUpdate(Vector3 pfc, float kcal);
    public static PFCUIUpdate UpdateUI;

    private void Start()
    {
        keeperPFC = FindAnyObjectByType<PlayerPFCKeeper>();

        Vector3 pfc = keeperPFC.GetPFC();
        protein.maxValue = pfc.x;
        fat.maxValue = pfc.y;
        carbon.maxValue = pfc.z;
        kcal.maxValue = keeperPFC.GetKcal();

        UpdateUI += UpdateUISliders;
        UpdateUISliders(Vector3.zero, 0f);
    }

    void UpdateUISliders(Vector3 pfc, float kcalValue)
    {
        protein.value = pfc.x;
        fat.value = pfc.y;
        carbon.value = pfc.z;
        kcal.value = kcalValue;
    }
}
