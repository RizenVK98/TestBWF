using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

    private Image joystickBG;
    [SerializeField]
    private Image joystick;
    private Vector2 InputVector;//получения координат джойстика

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
        InputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero; //возврат джойстика в центр
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, ped.position, ped.pressEventCamera, out pos)) //сравнение между углом отклонения между центром объекта касания и местом касания
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x); //получение координат касания на джойстик по х
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y); //получение координат касания на джойстик по у
           // print(pos);

            InputVector = new Vector2(pos.x*1.9f, pos.y*1.9f); //установка точных координат из касания 
           InputVector = (InputVector.magnitude > 1f) ? InputVector.normalized : InputVector;

           joystick.rectTransform.anchoredPosition = new Vector2(InputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), InputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public float Horizontal()
    {
        if (InputVector.x != 0)
        {
            return InputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if (InputVector.y != 0)
        {
            return InputVector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}
