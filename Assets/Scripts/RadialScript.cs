using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialScript : MonoBehaviour
{
    float specialRadial = 1f;
    float maxSpecialRadial;
    [SerializeField] Image radialUI = null;
    // Start is called before the first frame update
    void Start()
    {
        maxSpecialRadial = Playerscript.instance.specialCD;
    }

    // Update is called once per frame
    void Update()
    {
        radialUI.fillAmount = Playerscript.instance.currentSpecialCD / maxSpecialRadial;
    }
}
