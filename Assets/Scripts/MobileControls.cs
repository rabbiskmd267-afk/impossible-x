
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileControls : MonoBehaviour,
    IPointerDownHandler,
    IPointerUpHandler,
    IDragHandler
{
    public PlayerController player;
    public RectTransform joystickArea;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (player == null || joystickArea == null)
            return;

        Vector2 local;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickArea,
            eventData.position,
            eventData.pressEventCamera,
            out local
        );

        Vector2 input = new Vector2(
            local.x / (joystickArea.rect.width / 2),
            local.y / (joystickArea.rect.height / 2)
        );

        player.SetMobileInput(
            Vector2.ClampMagnitude(input, 1f)
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (player != null)
            player.SetMobileInput(Vector2.zero);
    }
}
