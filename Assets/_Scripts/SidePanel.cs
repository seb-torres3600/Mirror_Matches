using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Create a Canvas that holds a Text GameObject.

public class SidePanel : MonoBehaviour
{
    private GameObject myGO;
    private Canvas myCanvas;
    private GameObject myRawImage;
    private GameObject myText;
    private GameObject myButton;
    private Button buttonComponent;
    public Text textComponent;

    private GameObject logger_text;
    public  Text loggerComponent;
    private float screenWidth = Screen.width;
    private float screenHeight = Screen.height;
    private GameObject imageObject;
    public Image imageComponent;
    private RectTransform imageTransform;

    private RectTransform loggerTransformText;
    private float panel_width;
    private int side;
    private Sprite sprite;
    private Button attack_button;
    private Button move_button;
    private Button wait_button;
    
    // First piece of the side panels
    public SidePanel(float w, float h, int side)
    {   
        // get the width the panel should be
        panel_width = (float)Math.Round(4.0f/(float)w, 2) + .01f;
        // get reference to Canvas
        myCanvas = FindObjectOfType<Canvas>();
        // create inital side panel for appropritate side
        this.CreateSidePanels(side);
        this.side = side;
    }
    // Create the turn side panels
    public void CreateTurn(int pawn, float w, float h){
        string textString = "MAKE YOUR MOVES\n";
        // Set the text and other properties of the Text component
        textComponent.text = textString;
        imageTransform.anchorMin = new Vector2(0, .50f);
        imageTransform.anchorMax = new Vector2(1, .80f);
        // Create attack button, and add listener for when it is clicked
        this.attack_button = this.CreateButtons(.6f, "ATTACK");
        ButtonNotify amyScript = attack_button.GetComponent<ButtonNotify>();
        attack_button.onClick.AddListener(amyScript.Attack);
        // Create move button, add listener for when it is clicked
        this.move_button = this.CreateButtons(.5f, "MOVE");
        ButtonNotify mmyScript = move_button.GetComponent<ButtonNotify>();
        move_button.onClick.AddListener(mmyScript.Move);
        // Create wait button, add listener for when it is clicked
        this.wait_button = this.CreateButtons(.4f, "WAIT");
        ButtonNotify wmyScript = wait_button.GetComponent<ButtonNotify>();
        wait_button.onClick.AddListener(wmyScript.Wait);

        GameObject logger_text = new GameObject();
        logger_text.transform.SetParent(myRawImage.transform);
        logger_text.name = "Logger";
        loggerComponent = logger_text.AddComponent<Text>();
        loggerComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        loggerComponent.fontSize = 12;
        loggerComponent.color = Color.white;
        // loggerComponent.alignment = TextAnchor.UpperCenter;
        loggerComponent.text = "Log";
        loggerTransformText = loggerComponent.GetComponent<RectTransform>();
        loggerTransformText.anchorMin = new Vector2(0.02f, .38f);
        loggerTransformText.anchorMax = new Vector2(0.98f, .38f);
        loggerTransformText.offsetMin = Vector2.zero;
        loggerTransformText.offsetMax = Vector2.zero;
        loggerTransformText.sizeDelta = Vector2.zero;

    }

    // Create side panel
    private void CreateSidePanels(int side)
    {
        // Set string for blue or red side
        string blueString = "Blue Team\nBe Ready! \n";
        string redString = "Red Team\nBe Ready! \n";
        
        // add a raw image as a child of the Canvas
        myRawImage = new GameObject();
        myRawImage.transform.SetParent(myCanvas.transform);
        // Set image name and add rawimage component 
        myRawImage.name = "SidePanel One";
        RawImage rawImage = myRawImage.AddComponent<RawImage>();
        // Set the Raw Image's color to black
        rawImage.color = Color.black;

        //Set the raw image size to only occupy the side panels
        RectTransform srectTransform = rawImage.GetComponent<RectTransform>();
        if (side == 0)
        {
            srectTransform.anchorMin = new Vector2(1.0f - panel_width, 0f);
            srectTransform.anchorMax = Vector2.one;
        }
        else
        {
            srectTransform.anchorMin = Vector2.zero;
            srectTransform.anchorMax = new Vector2(panel_width, 1);
        }
        srectTransform.offsetMin = Vector2.zero;
        srectTransform.offsetMax = Vector2.zero;
        srectTransform.sizeDelta = Vector2.zero;

        // Add a Text component to the raw image
        myText = new GameObject();
        myText.transform.SetParent(myRawImage.transform);
        myText.name = "Sides";
        textComponent = myText.AddComponent<Text>();

        // Set the text and other properties of the Text component
        if (side == 1)
        {
            textComponent.text = redString;
        }
        else
        {
            textComponent.text = blueString;
        }
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 20;
        textComponent.color = Color.white;
        textComponent.alignment = TextAnchor.UpperCenter;

        // Set the RectTransform of the Text component
        RectTransform rectTransformText = textComponent.GetComponent<RectTransform>();
        rectTransformText.anchorMin = Vector2.zero;
        rectTransformText.anchorMax = Vector2.one;
        rectTransformText.offsetMin = Vector2.zero;
        rectTransformText.offsetMax = Vector2.zero;
        rectTransformText.sizeDelta = Vector2.zero;

        // Add Image as child of rawImage to add pawn picture
        imageObject = new GameObject("Image");
        imageObject.transform.SetParent(myRawImage.transform);
        imageComponent = imageObject.AddComponent<Image>();
        if (side == 1)
        {
            sprite = Resources.Load<Sprite>("redknife");
        }
        else
        {
            sprite = Resources.Load<Sprite>("blueknife");
        }
        imageComponent.sprite = sprite;

        // Set the Image's rect transform to fill the entire canvas
        imageTransform = imageComponent.GetComponent<RectTransform>();
        imageTransform.anchorMin = Vector2.zero;
        imageTransform.anchorMax = new Vector2(1, .80f);
        imageTransform.offsetMin = Vector2.zero;
        imageTransform.offsetMax = Vector2.zero;
        imageTransform.sizeDelta = Vector2.zero;
    }
    // Function to create buttons
    // needs anchormin and what action it is
    private Button CreateButtons(float amin, string action)
    {
        // Create new button as child of canvas
        GameObject myButton = new GameObject("Move Button");
        myButton.transform.SetParent(myRawImage.transform);
        myButton.name = "Move Button";
        myButton.AddComponent<Button>();
        Button buttonComponent = myButton.GetComponent<Button>();
        myButton.gameObject.AddComponent<ButtonNotify>();
        // Set button colors
        ColorBlock colors = buttonComponent.colors;
        colors.normalColor = Color.blue;
        if (side == 1)
        {
            colors.normalColor = Color.red;
        }
        buttonComponent.colors = colors;
        // Set Button Location
        myButton.AddComponent<RectTransform>();
        RectTransform rec = myButton.GetComponent<RectTransform>();
        rec.anchorMin = new Vector2(.125f, amin);
        rec.anchorMax = new Vector2(.875f, amin + 0.05f);
        rec.offsetMin = Vector2.zero;
        rec.offsetMax = Vector2.zero;
        rec.sizeDelta = Vector2.zero;
        // Create Image and add as child of button
        GameObject myImage = new GameObject("Image");
        myImage.transform.SetParent(myButton.transform);
        Image targetGraphic = myButton.AddComponent<Image>();
        buttonComponent.targetGraphic = targetGraphic;
        // Create Text object an add as child of button
        GameObject mytext = new GameObject("Text");
        mytext.transform.SetParent(myButton.transform);
        mytext.AddComponent<Text>();
        Text btextcomp = mytext.GetComponent<Text>();
        btextcomp.text = action;
        btextcomp.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        btextcomp.fontSize = 24;
        btextcomp.color = Color.white;
        btextcomp.alignment = TextAnchor.MiddleCenter;
        
        RectTransform trec = mytext.GetComponent<RectTransform>();
        trec.anchorMin = Vector2.zero;
        trec.anchorMax = Vector2.one;
        trec.offsetMin = Vector2.zero;
        trec.offsetMax = Vector2.zero;
        trec.sizeDelta = Vector2.zero;

        return buttonComponent;
    }

    // When it's not the players turn, remove the buttons    
    public void NotTurn()
    {
        string blueString = "Blue Team\nBe Ready! \n";
        string redString = "Red Team\nBe Ready! \n";
        

        // Set the text and other properties of the Text component
        if (side == 1)
        {
            textComponent.text = redString;
        }
        else
        {
            textComponent.text = blueString;
        }
        
        // Set the Image's rect transform to fill the entire canvas
        imageTransform = imageComponent.GetComponent<RectTransform>();
        imageTransform.anchorMin = new Vector2(0, .70f);
        imageTransform.anchorMax = new Vector2(1, .90f);
        imageTransform.offsetMin = Vector2.zero;
        imageTransform.offsetMax = Vector2.zero;
        imageTransform.sizeDelta = Vector2.zero;

        // Remove Buttons When its not their turn 
        // Ding this by just making them really small
        RectTransform aRT = attack_button.GetComponent<RectTransform>();
        aRT.anchorMin = Vector2.zero;
        aRT.anchorMax = Vector2.zero;
        
        RectTransform mRT = move_button.GetComponent<RectTransform>();
        mRT.anchorMin = Vector2.zero;
        mRT.anchorMax = Vector2.zero;
        
        RectTransform wRT = wait_button.GetComponent<RectTransform>();
        wRT.anchorMin = Vector2.zero;
        wRT.anchorMax = Vector2.zero;

    }
    // When its sides turn
    public void Turn()
    {
        // resize buttons 
        textComponent.text = "MAKE YOUR MOVES\n";
        imageTransform.anchorMin = new Vector2(0, .70f);
        imageTransform.anchorMax = new Vector2(1, .90f);

        // Remove Buttons When its not their turn 
        RectTransform aRT = attack_button.GetComponent<RectTransform>();
        aRT.anchorMin = new Vector2(.125f, .6f);
        aRT.anchorMax =  new Vector2(.875f, .65f);
        
        RectTransform mRT = move_button.GetComponent<RectTransform>();
        mRT.anchorMin = new Vector2(.125f, .5f);
        mRT.anchorMax = new Vector2(.875f, .55f);
        
        RectTransform wRT = wait_button.GetComponent<RectTransform>();
        wRT.anchorMin = new Vector2(.125f, .4f);
        wRT.anchorMax =  new Vector2(.875f, .45f);

        loggerTransformText.anchorMin = new Vector2(0.02f, 0f);
        loggerTransformText.anchorMax = new Vector2(0.98f, .38f);
    }
    
}