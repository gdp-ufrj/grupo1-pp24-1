using UnityEngine;
using UnityEngine.EventSystems;

//Esta classe estar� presente nos objetos que ativar�o uma mudan�a de cen�rio
public class InteractionScenario : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerClick(PointerEventData eventData) {
        //Debug.Log("clicou!");
        GameController.GetInstance().changeScenario((int)GameController.LookDirection.OTHER, transform.gameObject.name);
        //throw new System.NotImplementedException();
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
