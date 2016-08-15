using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class buttonScript3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool donut_flg = false;
    private bool touch_imagenow = false;

    public void ButtonPush()
    {

        if (donut_flg == true)
        {
            if (touch_imagenow == true)
            {
                HP_Restore_Counter.button_flg = true;
                touch_imagenow = false;
            }
        }
       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touch_imagenow = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            donut_flg = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        donut_flg = false;
    }

}
