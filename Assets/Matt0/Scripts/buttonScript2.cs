using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class buttonScript2 : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{

    public static bool Button2 = false;
    public void ButtonPush()
    {
        Button2 = true;
        ChangeWeaponsKey.button2(Button2);
    }
	public void OnPointerEnter(PointerEventData eventData )
	{
		buttonScript.aaa = true;
	}
	public void OnPointerExit( PointerEventData eventData )
	{
		buttonScript.aaa = false;
	}
}
