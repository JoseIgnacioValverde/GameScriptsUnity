using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using UnityEngine.UIElements;

[Serializable]
public class DialogNodeData
{
    public string Guid;
    public string DialogueText;
    public Vector2 Position;
}

