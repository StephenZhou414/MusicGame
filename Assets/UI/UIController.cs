using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    private VisualElement root;
    private List<Button> btns = new List<Button>();
    private Button hiddenBtn;
    // Start is called before the first frame update
    void Start()
    {
        ToggleShowUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Button op1Btn = root.Q<Button>("op1");
        Button op2Btn = root.Q<Button>("op2");
        Button op3Btn = root.Q<Button>("op3");
        Button op4Btn = root.Q<Button>("op4");
        Button op5Btn = root.Q<Button>("op5");
        btns.Add(op1Btn); btns.Add(op2Btn); btns.Add(op3Btn); btns.Add(op4Btn); btns.Add(op5Btn);

        op1Btn.clicked += () => PutLevelBlock(0);
        op2Btn.clicked += () => PutLevelBlock(1);
        op3Btn.clicked += () => PutLevelBlock(2);
        op4Btn.clicked += () => PutLevelBlock(3);
        op5Btn.clicked += () => PutLevelBlock(4);

        //hide the first one
        hiddenBtn = btns[0];
        hiddenBtn.style.display = DisplayStyle.None;
    }


    private void PutLevelBlock(int opt)
    {
        Level.current.PlaceNextBlock(opt);
        ToggleShowUI();
    }

    public void ToggleShowUI()
    {
        if(root.style.display == DisplayStyle.None)
        {
            //show the last hidden btn
            hiddenBtn.style.display = DisplayStyle.Flex;
            //before showing again lets another at random
            var rn = new System.Random();
            int i = rn.Next(btns.Count);
            //hide one random btn
            hiddenBtn = btns[i];
            hiddenBtn.style.display = DisplayStyle.None;
        }
        root.style.display = root.style.display == DisplayStyle.None ? DisplayStyle.Flex : DisplayStyle.None;

    }


}
