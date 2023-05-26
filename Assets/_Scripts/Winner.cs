using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string message = GridManager._winningTeam;
        Canvas myCanvas = FindObjectOfType<Canvas>();
        
        // Add a Text component to the panel
        GameObject myText = new GameObject();
        myText.transform.SetParent(myCanvas.transform);
        myText.name = "Text";
        Text textComponent = myText.AddComponent<Text>();
        textComponent.text = message + " is the winner";
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 60;
        textComponent.color = Color.blue;
        if (message == "Red")
        {
            textComponent.color = Color.red;
        }
        textComponent.alignment = TextAnchor.MiddleCenter;
        // Set the RectTransform of the Text component
        RectTransform rectTransformText = textComponent.GetComponent<RectTransform>();
        rectTransformText.anchorMin = new Vector2(.25f,.40f);
        rectTransformText.anchorMax = new Vector2(.75f,.75f);
        rectTransformText.offsetMin = Vector2.zero;
        rectTransformText.offsetMax = Vector2.zero;
        rectTransformText.sizeDelta = Vector2.zero;
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
