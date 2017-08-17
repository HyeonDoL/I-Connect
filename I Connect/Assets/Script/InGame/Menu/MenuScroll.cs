using UnityEngine;
using UnityEngine.EventSystems;

public class MenuScroll : MonoBehaviour, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform menuTrans;

    [SerializeField]
    private RectTransform openTrans;

    [SerializeField]
    private RectTransform closeTrans;

    [SerializeField]
    private MenuControl menuControl;

    private float splitPointX;

    private void Awake()
    {
        splitPointX = (openTrans.anchoredPosition.x - closeTrans.anchoredPosition.x) / 3 + closeTrans.anchoredPosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float positionX = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                closeTrans.position.x, 
                                                openTrans.position.x);

        menuTrans.position = new Vector2(positionX, menuTrans.position.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float mousePositionX = (Camera.main.ScreenToViewportPoint(Input.mousePosition) * Screen.width).x;

        float positionX = Mathf.Clamp(mousePositionX, closeTrans.anchoredPosition.x, openTrans.anchoredPosition.x);

        if (positionX > splitPointX)
            menuControl.Open();

        else
            menuControl.Close();
    }
}