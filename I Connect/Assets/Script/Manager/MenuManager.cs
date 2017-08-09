using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    public static MenuManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("MenuManager").AddComponent<MenuManager>();
        }
    }

    private void Awake()
    {
        instance = this;
    }
}