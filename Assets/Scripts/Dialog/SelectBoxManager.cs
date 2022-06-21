using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBoxManager : MonoBehaviour {

    public Text[] choiceTexts;
    public Text cursorText;

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

    public void SetChoice(List<string> choices) {

        foreach (Text choiceText in choiceTexts) {
            choiceText.gameObject.SetActive(false);
        }
        for (int i = 0; i < choices.Count; i ++) {
            choiceTexts[i].text = choices[i];
            choiceTexts[i].gameObject.SetActive(true);
        }
    }
}
