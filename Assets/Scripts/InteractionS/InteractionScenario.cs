using UnityEngine;
using UnityEngine.EventSystems;

//Esta classe estar� presente nos objetos que ativar�o uma mudan�a de cen�rio
public class InteractionScenario : MonoBehaviour, IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventData) {
        //Debug.Log("clicou armario!");
        GameController.GetInstance().changeScenario((int)GameController.LookDirection.OTHER, transform.gameObject.name);
        //throw new System.NotImplementedException();
    }
}
