using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text contentText;
    public Text gameOverText;
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
        Select,
        End
    }

    public void Awake() {

        string nextStory = "";
        bool ignoreNext = false;
        InputType dialogType = InputType.None;
        TextAsset txt = Resources.Load(VariableManager.currentStory) as TextAsset;
        foreach (string line in (txt.text + "\r\n").Split("\r\n")) {
            switch (dialogType) {
                case InputType.None:
                    if (line.StartsWith("[dialog]"))
                        dialogType = InputType.DialogName;
                    else if (line.StartsWith("[select]"))
                        dialogType = InputType.Select;
                    else if (line.StartsWith("[end]"))
                        dialogType = InputType.End;
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
                    else {
                        if (line.StartsWith("[ignore]"))
                            ignoreNext = true;
                        else if (line.StartsWith("[:")) {
                            ignoreNext = false;
                            for (int i = 2; i < line.Length; i++) {
                                if (line[i] == ']') break;
                                else nextStory += line[i];
                            }
                        }
                        else dialogs.Last().choices.Add(new Choice(line, nextStory, ignoreNext));
                    }
                    break;
                case InputType.End:
                    dialogs.Last().end = true;
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
                    if (dialog.choices.Count > 0) {
                        FindObjectOfType<SoundManager>().Play("ShowSelect");
                        selectBox.GetComponent<SelectBoxManager>().SetChoice(dialog.choices);
                        selectBox.SetActive(true);
                        FindObjectOfType<NavigationAnimator>().animationType =
                            NavigationAnimator.AnimationType.Seleting;
                    }
                    else if (dialog.end) {
                        FindObjectOfType<SoundManager>().Play("GameOver");
                        gameOverText.gameObject.SetActive(true);
                        FindObjectOfType<NavigationAnimator>().animationType =
                            NavigationAnimator.AnimationType.EndGame;
                    }
                    else {
                        FindObjectOfType<NavigationAnimator>().animationType =
                            NavigationAnimator.AnimationType.EndText;
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

        dialogIndex ++;
        return dialogIndex < dialogs.Count;
    }

    private Dialog GetDialog() {

        return dialogs[dialogIndex];
    }
}
