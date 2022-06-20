using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public int cursorBlinkTimeSpeed = 1000;
    public GameObject cursorText;

    private float timer = 0;

    void Start() {

    }

    void Update() {

        timer += Time.deltaTime;
        if ((int)(timer * 1000) >= cursorBlinkTimeSpeed) {
            timer = 0;
            cursorText.SetActive(true);
        }
        else if ((int)(timer * 1000) >= cursorBlinkTimeSpeed / 2) {
            cursorText.SetActive(false);
        }
    }

    public void MoveCursor(GameObject sender) {

        timer = 0;
        cursorText.SetActive(true);
        FindObjectOfType<SoundManager>().Play("Select");
        Vector3 oriPos = cursorText.transform.position;
        cursorText.transform.position =
            new Vector3(sender.transform.position.x - 105, sender.transform.position.y, oriPos.z);
    }

    public void SetNewCursor(GameObject target) {

        cursorText = target;
    }

    public void StartGame() {

        SceneManager.LoadScene(1);
    }

    public void QuitGame() {

        Application.Quit();
    }
}
