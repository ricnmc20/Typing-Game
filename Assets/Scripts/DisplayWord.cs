﻿using TMPro;
using UnityEngine;

public class DisplayWord : MonoBehaviour
{
    private string word = "Sample-text";
    private TMP_Text text;
    private SlicedMesh backgroundMesh;
    private int typedIndex = 0;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.autoSizeTextContainer = true;
        text.text = word;

        backgroundMesh = GetComponentInChildren<SlicedMesh>();
        backgroundMesh.Width = text.GetPreferredValues().x + 0.4f;
        backgroundMesh.Height = text.GetPreferredValues().y + 0.2f;
        backgroundMesh.Margin = 0.25f;
        backgroundMesh.transform.Translate(new Vector3(-text.GetPreferredValues().x / 2 - 0.2f, -text.GetPreferredValues().y / 2 - 0.1f, 0));
    }

    void Update()
    {
        foreach (char character in Input.inputString)
        {
            Type(character);
        }
    }

    public void Type(char character)
    {
        if (char.ToUpperInvariant(word[typedIndex]).Equals(char.ToUpperInvariant(character)))
        {
            typedIndex++;
        }
        else
        {
            Debug.Log("Wrong character " + character);
        }

        text.text = "<color=#f0f>" + word.Substring(0, typedIndex) + "</color>" + word.Substring(typedIndex);
    }
}
