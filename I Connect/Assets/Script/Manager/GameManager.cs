using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance)
                return instance;
            else
                return instance = new GameObject("GameManager").AddComponent<GameManager>();
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        IsCanTouch = true;
        clearedStage.SetValue(PlayerPrefs.GetInt("clearedStage", 0));
    }

    public int StageLv { set; get; }

    private  BitsByte clearedStage;
    
    public void SetStageClear(int index,bool value)
    {
        clearedStage[index] = value;
        Debug.Log("CST : " + clearedStage.GetValue());
        PlayerPrefs.SetInt("clearedStage", clearedStage.GetValue());
    }
    public bool GetStageClear(int index)
    {
        return clearedStage[index];
    }




    public bool IsCanTouch { set; get; }
}