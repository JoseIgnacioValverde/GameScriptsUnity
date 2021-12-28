using UnityEngine;
using UnityEngine.EventSystems;

public class MatchMogus : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{
    static MatchMogus hoverItem;
    public string itemName;
    public GameObject linePrefab;
    private GameObject line;
    public AmogusGame amog;
    public Canvas canvas;
    // Start is called before the first frame update
    public void Awake(){
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnPointerDown(PointerEventData eventData){
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
        UpdateLine(eventData.position);
    }
    public void OnDrag(PointerEventData eventData){
        UpdateLine(eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      hoverItem=this;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!this.Equals(hoverItem) && itemName.Equals(hoverItem.itemName)){
            amog.AddPoint();
            UpdateLine(hoverItem.transform.position);
            Destroy(hoverItem);
            Destroy(this);
            
        }
        else{
            Destroy(line);
        }
    }
    void UpdateLine(Vector3 position){
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        Debug.Log(canvas.scaleFactor);
        line.transform.localScale = new Vector3(direction.magnitude/canvas.scaleFactor, 1, 1);
    }
}
