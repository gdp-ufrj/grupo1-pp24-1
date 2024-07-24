using UnityEngine;

public class CaixaRevista : MonoBehaviour {
    public Item Item;
    public bool coletado;
    private void Awake() {
        if (ListaItems.Instance.ItensColetados.Contains(Item)) {
            Destroy(gameObject);
        }
    }

    void Pickup() {
        DialogueTrigger dialogueTrigger = gameObject.GetComponent<DialogueTrigger>();
        ItemInventory itemInventory = new ItemInventory(Item, dialogueTrigger);
        InventoryManager.Instance.Add(Item, dialogueTrigger);
        ListaItems.Instance.ItensColetados.Add(Item);
        if (ListaItems.Instance.listaItenslargados.Contains(itemInventory)) {
            ListaItems.Instance.listaItenslargados.Remove(itemInventory);
        }
        SoundController.GetInstance().PlaySound(Item.nameSoundPickup);
        if (!gameObject.CompareTag("ItemDropped")) {
            if (dialogueTrigger != null) {
                if (Item.id != 2 && Item.id != 8)    //Se n�o for rem�dio nem presunto
                    dialogueTrigger.TriggerInteractionDialogue(true);
            }
        }
        Destroy(gameObject);
    }

    void OnMouseDown() {
        if(!GameController.GetInstance().blockActionsDialogue)
            Pickup();
    }

}