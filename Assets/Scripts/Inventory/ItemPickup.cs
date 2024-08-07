using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour, IPointerClickHandler {
    public Item Item;

    private void Awake() {
        if (ListaItems.Instance.ItensColetados.Contains(Item)) {
            Destroy(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        eventData.Use();
        DialogueTrigger dialogueTrigger = gameObject.GetComponent<DialogueTrigger>();
        ItemInventory itemInventory = new ItemInventory(Item, dialogueTrigger);
        InventoryManager.Instance.Add(Item, dialogueTrigger);
        ListaItems.Instance.ItensColetados.Add(Item);
        if (ListaItems.Instance.listaItenslargados.Contains(Item)) {
            ListaItems.Instance.listaItenslargados.Remove(Item);
        }
        SoundController.GetInstance().PlaySound(Item.nameSoundPickup);
        if (!gameObject.CompareTag("ItemDropped")) {
            if (dialogueTrigger != null) {
                if (Item.id != 2 && Item.id != 8)    //Se n�o for rem�dio nem presunto
                    dialogueTrigger.TriggerInteractionDialogue(true);
            }
        }
        if (Item.id == 15)
            ListaItems.Instance.pegouchaveFinal = true;
        Destroy(gameObject);
    }

}
