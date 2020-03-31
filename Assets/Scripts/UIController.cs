using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject slider; 
    // Start is called before the first frame update
    void Start()
    {
        HideSlider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaiseClicked()
    {
        //if(slider.activeSelf == false)
        //{
        //    ShowSlider();
        //}
        //else
        //{
        //    HideSlider();
        //}
    }
    public void ShowSlider()
    {
        //slider.SetActive(true);
    }

    public void HideSlider()
    {
        //slider.SetActive(false);
    }
}
