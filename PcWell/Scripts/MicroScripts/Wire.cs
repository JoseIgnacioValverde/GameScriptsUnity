using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    public LayerMask layerMask;
    new Camera camera;
    Vector3 startPoint;
    Vector3 startPosition;

    public void Start(){
        startPoint = transform.parent.position;
        startPosition = transform.position;
        camera = Camera.main;
    }
    private void OnMouseDrag(){
        Vector3 newPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.2f, layerMask);
        foreach(Collider2D collider in colliders){
            if(collider.gameObject != gameObject){
                UpdateWire(collider.transform.position);

                if(transform.parent.name.Equals(collider.transform.parent.name)){
                    //Main.Instance.SwitchCharge(1);

                    collider.GetComponent<Wire>();
                }
                return;
            }
        }
        UpdateWire(newPosition);
    }
    void UpdateWire(Vector3 newPosition){
        transform.position = newPosition;

        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, newPosition)/Mathf.Abs(transform.lossyScale.x);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
