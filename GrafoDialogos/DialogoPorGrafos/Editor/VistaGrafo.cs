using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class VistaGrafo : GraphView
{
    //tamaños de las cajas de nuevos nodos.
    public readonly Vector2 defaultNodeSize = new Vector2(x: 150, y: 200);
    //constructor
    public VistaGrafo()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(GenerateEntryPointNode());
    }

    private Nodo GenerateEntryPointNode()
    {
        var node = new Nodo
        {
            title = "COMIENZO",
            GUID = System.Guid.NewGuid().ToString(),
            DialogueText = "ENTRYPOINT",
            EntryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Siguiente";
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(x: 100, y: 100, width: 100, height: 150));

        return node;
    }

    private Port GeneratePort(Nodo node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));//el typeof realmente no hace nada.
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateConverNode(nodeName));
    }

    public Nodo CreateConverNode(string nodeName)
    {
        var conversationNode = new Nodo
        {
            title = nodeName,
            DialogueText = nodeName,
            GUID = System.Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(conversationNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Entrada";
        conversationNode.inputContainer.Add(inputPort);

        var button = new Button(clickEvent: () => { AddChoicePort(conversationNode); });
        button.text = "Añadir camino";
        conversationNode.titleContainer.Add(button);

        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt => 
        {
            conversationNode.DialogueText = evt.newValue;
            conversationNode.title = evt.newValue;

        });
        textField.SetValueWithoutNotify(conversationNode.title);
        conversationNode.mainContainer.Add(textField);

        conversationNode.RefreshExpandedState();
        conversationNode.RefreshPorts();
        conversationNode.SetPosition(new Rect(position: Vector2.zero, defaultNodeSize));

        return conversationNode;
    }

    //lista de puertos compatibles para el nodo que corresponda
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach(funcCall: port =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }


    //crea nuevas opciones de diálogo
    public void AddChoicePort(Nodo conversationNode, string overriddenPortName = "")
    {
        var generatedPort = GeneratePort(conversationNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>(name: "type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = conversationNode.outputContainer.Query(name: "connector").ToList().Count;
        generatedPort.portName = $"Camino{outputPortCount}";

        var choicePortName = string.IsNullOrEmpty(overriddenPortName) ? $"Camino {outputPortCount + 1}" : overriddenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = choicePortName
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label("  "));
        generatedPort.contentContainer.Add(textField);
        var deleteButton = new Button(clickEvent: () => RemovePort(conversationNode, generatedPort))
        {
            text = "X"
        };
        generatedPort.contentContainer.Add(deleteButton);
        generatedPort.portName = choicePortName;
        conversationNode.outputContainer.Add(generatedPort);
        conversationNode.RefreshPorts();
        conversationNode.RefreshExpandedState();
    }
    private void RemovePort(Nodo nodo, Port generatedPort)
    {
        var targetEdge = edges.ToList()
            .Where(x => x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);
        if (targetEdge.Any())
        {
            var edge = targetEdge.First();
            edge.input.Disconnect(edge);
            RemoveElement(targetEdge.First());
        }

        nodo.outputContainer.Remove(generatedPort);
        nodo.RefreshPorts();
        nodo.RefreshExpandedState();
    }
}
