using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortMenu : MonoBehaviour
{
    public enum MenuElement
    {
        Connector,
        CrossCable,
        Cutter,
        Direct,
        Label,
        Router,
        Setting,
        Switch
    }

    [SerializeField]
    private Transform[] elementTrans;

    [SerializeField]
    private Transform[] orderTrans;

    private MenuElement[] sortArray;

    private InGameData inGameData;

    private void Awake()
    {
        inGameData = InGameManager.Instance.GetInGameData(GameManager.Instance.StageLv - 1);


    }
}