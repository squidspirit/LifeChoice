using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectBoxManager : MonoBehaviour {

    public Text[] choiceTexts;
    public Text cursorText;

    private List<Choice> choices;
    private int cursorBlinkTimeSpeed = VariableManager.cursorBlinkTimeSpeed;
    private float timer = 0;

    void Start() {
        
    }

    void Update() {

        timer += Time.deltaTime;
        if ((int)(timer * 1000) >= cursorBlinkTimeSpeed) {
            timer = 0;
            cursorText.gameObject.SetActive(true);
        }
        else if ((int)(timer * 1000) >= cursorBlinkTimeSpeed / 2) {
            cursorText.gameObject.SetActive(false);
        }
    }

    public void MoveCursor(GameObject sender) {

        timer = 0;
        cursorText.gameObject.SetActive(true);
        FindObjectOfType<SoundManager>().Play("Select");
        Vector3 oriPos = cursorText.transform.position;
        cursorText.transform.position = new Vector3(
            sender.transform.position.x - 60f * Screen.width / 1600f,
            sender.transform.position.y, oriPos.z
        );
    }

    public void ClickChoice(GameObject sender) {

        for (int i = 0; i < choiceTexts.Length; i ++) {
            if (choiceTexts[i].name == sender.name) {
                FindObjectOfType<SoundManager>().Play("Enter");
                if (!choices[i].ignore) {
                    if (choices[i].next == null || choices[i].next.Length == 0)
                        VariableManager.AppendCurrentStory(i);
                    else VariableManager.SetCurrentStory(choices[i].next);
                    StartCoroutine(WaitAndSwitchScene());
                }
                break;
            }
        }
    }

    public void SetChoice(List<Choice> choices) {

        this.choices = choices;
        foreach (Text choiceText in choiceTexts) {
            choiceText.gameObject.SetActive(false);
        }
        for (int i = 0; i < choices.Count; i ++) {
            choiceTexts[i].text = choices[i].text;
            choiceTexts[i].gameObject.SetActive(true);
        }
    }

    private IEnumerator WaitAndSwitchScene() {

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
