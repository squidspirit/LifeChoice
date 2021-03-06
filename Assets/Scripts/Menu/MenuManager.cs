using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

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
            sender.transform.position.x - 80f * Screen.width / 1600f,
            sender.transform.position.y, oriPos.z
        );
    }

    public void SetNewCursor(Text target) {

        cursorText = target;
    }

    public void StartGame(Text name) {

        FindObjectOfType<SoundManager>().Play("Enter");
        if (name.text.Length == 0)
            name.text = "?L?W??";
        VariableManager.playerName = name.text;
        StartCoroutine(WaitAndSwitchScene());
    }

    public void QuitGame() {

        Application.Quit();
    }

    private IEnumerator WaitAndSwitchScene() {

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
