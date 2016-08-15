using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
public class buttonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public static bool aaa;
    public static bool Button1=false;
    public void ButtonPush()
    {
        Button1 = true;
        ChangeWeaponsKey.button1(Button1);
    }
	public void OnPointerEnter(PointerEventData eventData )
	{
		aaa = true;
	}
	public void OnPointerExit( PointerEventData eventData )
	{
		aaa = false;
	}
}