using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    //not use

    //public enum MenuButtonType
    //{
    //    None,
    //    TouchToStart,
    //    Cancle,
    //    Start,
    //    Exit,
    //    Host,
    //    Join,
    //    Refresh
    //}
    //[SerializeField]
    //private MenuButtonType myType;

    //[SerializeField]
    //private GameObject[] Menus;

    //[SerializeField]
    //private GameObject Waiting;

    //private bool IsUsingItem;

    //public void ClickMenu()
    //{
    //    switch (myType)
    //    {
    //        case MenuButtonType.TouchToStart: DisableMenuAndTurnOn(1); break;
    //        case MenuButtonType.Cancle: DisableMenuAndTurnOn(0); NetworkManager.instance.OnClickCancle(); break;
    //        case MenuButtonType.Join: DisableMenuAndTurnOn(2); break;
    //        case MenuButtonType.Host: OnClickHost(); break;

    //        case MenuButtonType.Start:
    //            AutoFade.LoadLevel(GameManager.instance.isUsingItem ? "Local_item" : "Local", 1.5f, 1.5f, Color.black); GameManager.instance.Reset(); GameManager.instance.FadeMusic.Fade(1.5f); break;
    //        case MenuButtonType.Exit: AutoFade.LoadLevel("Title", 1.5f, 1.5f, Color.white); GameManager.instance.Reset(); break;

    //        default: break;
    //    }
    //}
    //private void DisableMenuAndTurnOn(int OnIndex)
    //{
    //    for (int i = 0; i < Menus.Length; ++i)
    //    {
    //        Menus[i].SetActive(false);
    //    }
    //    Menus[OnIndex].SetActive(true);
    //}

    //private void OnClickHost()
    //{
    //    if (Network.isServer || Network.isClient)
    //        return;
    //    NetworkManager.instance.OnClickStart("MatchPoint : " + GameManager.instance.GameRound + "\n" + "GameLevel : " + GameManager.instance.GameDifficulty);
    //    Waiting.SetActive(true);

    //}
    //public void SetUsingItem()
    //{
    //    IsUsingItem = !IsUsingItem;
    //    Debug.Log("Itme: " + IsUsingItem);
    //}

}//not use