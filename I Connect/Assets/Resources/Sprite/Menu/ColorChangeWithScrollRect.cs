using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeWithScrollRect : MonoBehaviour
{
    [SerializeField]
    private ScrollRect myRect;

    [SerializeField]
    private Image myImage;

    [SerializeField]
    private Vector3 ColorDefault;

    private void Awake()
    {
        
    }
    public void OnValueChanged(Vector2 value)
    {
        float H = value.x;
        myImage.color = Color.HSVToRGB(H, ColorDefault.y, ColorDefault.z);
    }
}
