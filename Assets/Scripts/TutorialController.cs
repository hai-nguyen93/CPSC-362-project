using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> slides;
    public int currentSlide = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentSlide = 0;
        foreach (GameObject g in slides)
        {
            g.SetActive(false);
        }
        slides[currentSlide].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Next()
    {
        if (currentSlide < slides.Count - 1)
        {
            slides[currentSlide].SetActive(false);
            ++currentSlide;
            slides[currentSlide].SetActive(true);
        }
    }

    public void Back()
    {
        if (currentSlide > 0)
        {
            slides[currentSlide].SetActive(false);
            --currentSlide;
            slides[currentSlide].SetActive(true);
        }
    }

    public void Title()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
