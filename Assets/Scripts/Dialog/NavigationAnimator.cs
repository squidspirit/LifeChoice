using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationAnimator : MonoBehaviour {

    public enum AnimationType {

        None,
        Typing,
        Seleting,
        EndText,
        EndGame
    }

    public Text navigationText;
    public int animationTime;
    public AnimationType animationType;

    private float timer = 0;

    void Update() {

        switch (animationType) {
            case AnimationType.None:
                navigationText.text = "";
                break;
            case AnimationType.Typing:
                timer += Time.deltaTime;
                if ((int)(timer * 1000) >= animationTime) {
                    timer = 0;
                    navigationText.text = "...";
                }
                else if ((int)(timer * 1000) >= animationTime / 4 * 3) {
                    navigationText.text = "..";
                }
                else if ((int)(timer * 1000) >= animationTime / 2) {
                    navigationText.text = ".";
                }
                else if ((int)(timer * 1000) >= animationTime / 4) {
                    navigationText.text = "";
                }
                break;
            case AnimationType.Seleting:
                timer += Time.deltaTime;
                if ((int)(timer * 1000) >= animationTime) {
                    timer = 0;
                    navigationText.text = "???";
                }
                else if ((int)(timer * 1000) >= animationTime / 4 * 3) {
                    navigationText.text = "??";
                }
                else if ((int)(timer * 1000) >= animationTime / 2) {
                    navigationText.text = "?";
                }
                else if ((int)(timer * 1000) >= animationTime / 4) {
                    navigationText.text = "";
                }
                break;
            case AnimationType.EndText:
                navigationText.text = "??";
                break;
            case AnimationType.EndGame:
                navigationText.text = "X";
                break;
        }
    }
}
