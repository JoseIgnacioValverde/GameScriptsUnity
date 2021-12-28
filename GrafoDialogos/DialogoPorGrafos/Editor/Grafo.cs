using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class Grafo : EditorWindow
{
    private VistaGrafo graphView;
    private string nombreArchivo= "Nueva narrativa";

    [MenuItem("Graphs/Conversation Graph")]
    public static void OpenConversationGraphWindow()
    {
        var window = GetWindow<Grafo>();
        window.titleContent = new GUIContent(text: "Conversation Graph");
    }
    private void OnEnable()
    {
        graphView = new VistaGrafo
        {
            name = "Conversation Graph"
        };

        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);

        GenerateToolBar();
        GenerateMinimap();

    }
    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    //barra de herramientas para crear más nodos
    private void GenerateToolBar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField(label: "Nombre del archivo:");
        fileNameTextField.SetValueWithoutNotify(nombreArchivo);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => nombreArchivo = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(true)) { text = "Guardar" });
        toolbar.Add(child: new Button(clickEvent: () => RequestDataOperation(false)) { text = "Cargar" });

        var nodeCreateButton = new Button(clickEvent: () =>
        {
            graphView.CreateNode("Nodo de dialogo");
        });
        nodeCreateButton.text = "Crear Nodo";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }
    private void RequestDataOperation(bool save)//para cargar y guardar
    {
        if (string.IsNullOrEmpty(nombreArchivo))
        {
            EditorUtility.DisplayDialog(title: "Nombre de archivo inválido", message: "Por favor introduce un nombre de archivo válido", ok:"Entendido");
            return;
        }
        var saveUtility = GraphSaveUtility.GetInstance(graphView);
        if (save)
            saveUtility.SaveGraph(nombreArchivo);
        else
        {
            saveUtility.LoadGraph(nombreArchivo);
        }
    }
    private void GenerateMinimap()
    {
        var miniMap = new MiniMap { anchored = true };
        miniMap.SetPosition(newPos: new Rect(x: 10, y: 30, width: 200, height: 140));
        graphView.Add(miniMap);
    }
}
