using Ink.Runtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {    //Esta classe ser� �nica para todo o projeto (singleton class)
    private static DialogueController instance;

    public TextAsset variablesJSON;    //Este � o arquivo JSON do ink que cont�m todas as vari�veis de di�logo
    public GameObject canvasDialogue, dialogueBox;
    public TextMeshProUGUI txtDialogue, txtNameCharacter;
    public GameObject[] choices;
    public DialogueVariablesController dialogueVariablesController { get; private set; }
    [SerializeField] [Range(5f, 10f)] private float showPanelDialogueTax;

    private TextMeshProUGUI[] choicesTxt;
    private Story dialogue;

    private bool endLine = false;   //Esta vari�vel � respons�vel por guardar se cada linha do di�logo j� terminou ou ainda n�o
    private float textDialogueSpeed;
    private int indexLine;

    public bool dialogueActive { get; private set; }   //Quero que esta vari�vel possa ser lida por outros scripts, mas n�o modificada

    public static DialogueController GetInstance() {
        return instance;
    }

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //Ao carregar pela primeira vez, precisamos carregar as vari�veis criadas no ink para o c�digo. Fa�o isso chamando o pr�prio construtor da classe DialogueVariablesController:
        dialogueVariablesController = new DialogueVariablesController(variablesJSON);
    }

    void Start() {
        //DialogueBoxContainer.SetActive(false);
        dialogueActive = false;

        choicesTxt = new TextMeshProUGUI[choices.Length];   //O array deve ter o mesmo tamanho do n�mero de escolhas
        int index = 0;
        foreach (GameObject choice in choices) {
            choicesTxt[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update() {
        if (dialogueActive) {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
                PassDialogue();
        }
        if (endLine) {
            endLine = false;
            //if (dialogue.currentChoices.Count > 0)
            //    ShowChoices();
        }
    }

    public IEnumerator ShowCanvasDialogue() {   //Este m�todo ser� respon�vel por mostrar o canvas de di�logo
        Image dialogueBoxRectTransform = dialogueBox.GetComponent<Image>();
        Color initialColor = new Color(0, 0, 0, 0);
        dialogueBoxRectTransform.color = initialColor;
        float targetValue = 0.5f;   //Este � o valor desejado para a opacidade da caixa de di�logo
        bool txtStarted = false;
        canvasDialogue.SetActive(true);

        while (Mathf.Abs(dialogueBoxRectTransform.color.a - targetValue) > 0.01f) {
            float lerpValue = Mathf.Lerp(dialogueBoxRectTransform.color.a, targetValue, showPanelDialogueTax * Time.deltaTime);
            Color newColor = new Color(0, 0, 0, lerpValue);
            dialogueBoxRectTransform.color = newColor;
            if (Mathf.Abs(dialogueBoxRectTransform.color.a - targetValue) > 0.008f && !txtStarted) {    //Para come�ar a mostrar as letras do di�logo um pouco antes de mostrar a caixa
                txtStarted = true;
                StartTextDialogue();
            }
            yield return null;
        }
    }

    public void StartDialogue(TextAsset dialogueJSON, float textSpeed, float fontSize) {
        dialogue = new Story(dialogueJSON.text);        //Carregando o di�logo a partir do arquivo JSON passado de par�metro
        textDialogueSpeed = textSpeed;
        txtDialogue.fontSize = fontSize;
        StartCoroutine(ShowCanvasDialogue());
    }

    public void StartTextDialogue() {
        dialogueActive = true;
        dialogueVariablesController.StartListening(dialogue);  //Para detectar as mudan�as de vari�veis no di�logo
        if (dialogue.canContinue) {
            dialogue.Continue();
            StartCoroutine(PrintDialogue());
        }
    }

    private void PassDialogue() {
        string fala = dialogue.currentText;

        if (indexLine < fala.Length - 1) {         //Se n�o estiver no final da fala
            StopAllCoroutines();
            indexLine = fala.Length - 1;
            endLine = true;
            txtDialogue.text = fala;
        }
        else {
            if (dialogue.currentChoices.Count == 0) {
                //SoundController.GetInstance().PlaySound("skip_dialogo", null);
                if (!dialogue.canContinue)     //Se estiver no final do di�logo
                    EndDialogue();
                else {
                    dialogue.Continue();
                    StartCoroutine(PrintDialogue());
                }
            }
        }
    }

    //Fun��o que printa cada linha do di�logo na caixa de di�logo
    private IEnumerator PrintDialogue() {
        //ChangeCharacterDialogue();
        string fala = dialogue.currentText;    //Pegando a fala atual do di�logo

        txtDialogue.text = "";
        for (int i = 0; i < fala.Length; i++) {    //Fazendo as letras aparecerem uma de cada vez
            txtDialogue.text += fala[i];
            indexLine = i;
            yield return new WaitForSeconds(textDialogueSpeed);
        }
        endLine = true;
    }

    private void EndDialogue() {   //M�todo chamado ao fim do di�logo
        txtDialogue.text = "";
        canvasDialogue.SetActive(false);
        //DialogueBoxContainer.SetActive(false);
        dialogueActive = false;
        dialogueVariablesController.StopListening(dialogue);  //Para parar de detectar as mudan�as de vari�veis no di�logo
        //GameController.checkVariablesDialogue(dialogueVariablesController.variablesValues);    //Fazendo as checagens de vari�veis importantes que podem ter mudado ap�s um di�logo
    }

    /*
    private void ShowChoices() {    //Fun��o para mostrar as escolhas do di�logo
        List<Choice> choicesList = dialogue.currentChoices;   //Recuperando as escolhas do di�logo

        int index = 0;
        foreach (Choice choice in choicesList) {
            choicesTxt[index].text = choice.text;
            choices[index].SetActive(true);
            index++;
        }
        for (int i = index; i < choices.Length; i++) {   //Escondendo as escolhas que n�o fazem parte do di�logo
            choices[i].SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex) {    //Fun��o para fazer uma escolha no di�logo
        dialogue.ChooseChoiceIndex(choiceIndex);
        foreach (GameObject choice in choices) {
            choice.SetActive(false);
        }

        if (!dialogue.canContinue)     //Se estiver no final do di�logo
            EndDialogue();
        else {
            dialogue.Continue();
            StartCoroutine(PrintDialogue());
        }
    }

    private void ChangeCharacterDialogue() {   //Fun��o para mudar o sprite do personagem do di�logo
        List<string> tagsDialogueLine = dialogue.currentTags;   //As tags s�o: nome do personagem e sprite do personagem
        string characterName = "", spriteCharacter = "";
        foreach (string tag in tagsDialogueLine) {
            if (tag.Split(":")[0].Trim() == "character")
                characterName = tag.Split(":")[1].Trim().ToUpper();
            else if (tag.Split(":")[0].Trim() == "state")
                spriteCharacter = tag.Split(":")[1].Trim();
        }
        if (spriteCharacter != "")
            ImgCharacterDialogue.GetComponent<Animator>().Play(spriteCharacter);
        if (characterName != "")
            txtNameCharacter.text = characterName;
    }
    */

    public Ink.Runtime.Object GetVariableState(string variableName) {    //Esta fun��o servir� para recuperar o estado de determinada vari�vel de di�logo
        Ink.Runtime.Object variableValue = null;
        dialogueVariablesController.variablesValues.TryGetValue(variableName, out variableValue);
        if (variableValue == null) {
            Debug.Log("N�o foi poss�vel recuperar o valor da vari�vel de di�logo informada.");
            return null;
        }
        return variableValue;
    }
}
