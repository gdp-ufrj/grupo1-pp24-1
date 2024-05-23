using UnityEngine;
using UnityEngine.EventSystems;

//Esta classe estar� presente nos objetos que ativar�o di�logos
public class InteractionDialogue : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerClick(PointerEventData eventData) {
        gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void OnPointerDown(PointerEventData eventData) {
        //throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData) {
        //throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData) {
        //throw new System.NotImplementedException();
    }
}
