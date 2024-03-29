﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour
{
    public static Potions instance;
    int currentPotions = 3;
    int maxPotions = 10;
    public float healAmount = 1f;
    // public delegate void OnPotionChanged();
    // public OnPotionChanged onPotionChangedCallback;
    // Start is called before the first frame update
    public GameObject UIText;
    Text potionText;

    private void Awake()
    {
        instance = this;
    }
    public void Add()
    {
        currentPotions++;
        potionText.text = currentPotions.ToString();
    }

    public bool Remove()
    {
        if (currentPotions > 0)
        {
            currentPotions--;
            potionText.text = currentPotions.ToString();
            return true;
        }
        return false;
    }
    void Start()
    {
        potionText = UIText.GetComponent<Text>();
        potionText.text = currentPotions.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
