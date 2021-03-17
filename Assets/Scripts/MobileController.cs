using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image joystickBG;
    [SerializeField]
    private Image joystick;
    private Vector2 inputVector;

    private void Start()
    {
        joystickBG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;//возврат джойстика в центр
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);//получение координат касания джойстика
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.x);//получение координат касания джойстика
            //print(pos);

            inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);//Уствновка точных координат из касания
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public float Horizontal()
    {
         return inputVector.x;
        
    }

    public float Vertical()
    {
        return inputVector.y;

    }
}
