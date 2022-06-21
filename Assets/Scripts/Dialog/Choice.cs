using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice {

    public Choice(string text) {

        this.text = text;
        this.next = null;
        this.ignore = false;
    }

    public Choice(string text, string next) {

        this.text = text;
        this.next = next;
        this.ignore = false;
    }

    public Choice(string text, bool ignore) {

        this.text = text;
        this.next = "";
        this.ignore = ignore;
    }

    public Choice(string text, string next, bool ignore) {

        this.text = text;
        this.next = next;
        this.ignore = ignore;
    }

    public string text { get; }
    public string next { get; }
    public bool ignore { get; }
}
