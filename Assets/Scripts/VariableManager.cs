using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager {

    private static string _currentStory = "Stories/001";
    
    public static int cursorBlinkTimeSpeed = 1000;
    public static int textTimeSpeed = 50;
    public static string playerName;

    public static string currentStory { get => _currentStory; }

    public static void AppendCurrentStory(int i) {
        _currentStory += i.ToString();
    }

    public static void AppendCurrentStory(string str) {
        _currentStory += str;
    }

    public static void SetCurrentStory(string str) {
        _currentStory = "Stories/" + str;
    }
}
