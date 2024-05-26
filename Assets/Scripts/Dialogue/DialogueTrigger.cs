using UnityEngine;

public class DialogueTrigger : MonoBehaviour {     //Este script apenas � respons�vel por triggar um di�logo quando alguma a��o acontecer durante o jogo

    public TextAsset dialogueJSON;
    [SerializeField] private float textDialogueSpeed = 0.05f;
    [SerializeField] private float fontSize = 20f;

    public void TriggerDialogue() {
        if (!DialogueController.GetInstance().dialogueActive)
            DialogueController.GetInstance().StartDialogue(dialogueJSON, textDialogueSpeed, fontSize);
    }
}