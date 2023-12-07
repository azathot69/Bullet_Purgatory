using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField]
    private GameObject plane;

    private Renderer planeRenderer;

    private Color newPlaneColor;

    private float level1Color, level2Color;

    // Start is called before the first frame update
    void Start()
    {
        planeRenderer = plane.GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        newPlaneColor = new Color();
    }
}
