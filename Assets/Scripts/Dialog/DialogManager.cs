using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text contentText;
    public GameObject selectBox;

    private int textTimeSpeed = VariableManager.textTimeSpeed;
    readonly private List<Dialog> dialogs = new List<Dialog>();
    private Queue<char> dialogQueue = new Queue<char>();
    private int dialogIndex = -1;
    private float timer = 0;
    private Dialog dialog;

    private enum InputType {

        None,
        DialogName,
        DialogContent,
        Select
    }

    void Awake() {

        InputType dialogType = InputType.None;
        TextAsset txt = Resources.Load(VariableManager.currentStory) as TextAsset;
        foreach (string line in txt.text.Split("\r\n")) {
            switch (dialogType) {
                case InputType.None:
                    foreach (char ch in line)
                    if (line == "[dialog]")
                        dialogType = InputType.DialogName;
                    else if (line == "[select]")
                        dialogType = InputType.Select;
                    break;
                case InputType.DialogName:
                    dialogs.Add(new Dialog());
                    if (line == "0")
                        dialogs.Last().name = VariableManager.playerName;
                    else
                        dialogs.Last().name = line;
                    dialogType = InputType.DialogContent;
                    break;
                case InputType.DialogContent:
                    if (line.Length == 0)
                        dialogType = InputType.None;
                    else dialogs.Last().content += line + "\n";
                    break;
                case InputType.Select:
                    if (line.Length == 0)
                        dialogType = InputType.None;
                    else dialogs.Last().choice.Add(line);
                    break;
            }
        }
    }

    void Start() {

        DialogUpdate();
    }

    void Update() {

        if (dialogQueue.Count == 0) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                DialogUpdate();
            }
        }
        else {
            timer += Time.deltaTime;
            
            if ((int)(timer * 1000) >= textTimeSpeed) {
                timer = 0;
                contentText.text += dialogQueue.Dequeue();
                if (dialogQueue.Count == 0) {
                    FindObjectOfType<SoundManager>().Stop("Typing");
                    if (dialog.choice.Count > 0) {
                        FindObjectOfType<SoundManager>().Play("ShowSelect");
                        selectBox.GetComponent<SelectBoxManager>().SetChoice(dialog.choice);
                        selectBox.SetActive(true);
                        FindObjectOfType<NavigationAnimator>().animationType =
                            NavigationAnimator.AnimationType.Seleting;
                    }
                    else {
                        FindObjectOfType<NavigationAnimator>().animationType =
                            NavigationAnimator.AnimationType.End;
                    }
                }
            }
        }
    }

    private bool DialogUpdate() {

        if (Next()) {
            dialog = GetDialog();
            nameText.text = dialog.name;
            // contentText.text = dialog.content;
            FindObjectOfType<NavigationAnimator>().animationType =
                NavigationAnimator.AnimationType.Typing;
            FindObjectOfType<SoundManager>().Play("Typing");
            timer = 0;
            contentText.text = "";
            dialogQueue = new Queue<char>(dialog.content);
            return true;
        }
        else return false;
    }

    private bool Next() {

        dialogIndex++;
        return dialogIndex < dialogs.Count;
    }

    private Dialog GetDialog() {

        return dialogs[dialogIndex];
    }
}
