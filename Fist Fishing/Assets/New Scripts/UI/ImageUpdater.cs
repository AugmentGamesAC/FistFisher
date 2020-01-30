using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ImageUpdater : MonoBehaviour
{

    [SerializeField]
    protected ImageTracker m_tracker;
    //Switched to a RawImage for texture use
    protected RawImage m_image;
    
    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponentInChildren<RawImage>();
        m_tracker.OnStateChange += UpdateState;
    }

    protected void UpdateState(Texture value)
    {
        //Change Image sprite or material
        //Image.sprite was removed due to Unity switching images to VisualElements
        //Can't find a way to edit the image sprite or material due to it being a visual element need help
        //Could swap entire image with a seperate different image object
        //Image.image should work, but doesnt update during play or at all

        //RawImage texture swap works
        m_image.texture = value;

    }
}
