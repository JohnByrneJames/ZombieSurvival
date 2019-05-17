using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour {

    public bool Gaussian;

    public Material Whites;
    public Material PostOutline;
    public Material GaussianBlur;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture TempRT = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 24);

        RenderTexture whiteRT = new RenderTexture(TempRT.width, TempRT.height, TempRT.depth);

        Graphics.Blit(source, whiteRT, Whites);

        if (Gaussian)
        {
            Graphics.Blit(whiteRT, TempRT, PostOutline);
            Graphics.Blit(TempRT, destination, GaussianBlur);
        }
        else
        {
            Graphics.Blit(whiteRT, destination, PostOutline);
        }
    }
}
