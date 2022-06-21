using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog {

    private bool _end = false;
    readonly private List<Choice> _choices = new List<Choice>();

    public string name { get; set; }
    public string content { get; set; }
    public bool end { get => _end; set => _end = value; }
    public List<Choice> choices { get => _choices; }

}
