using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Based on Board to Bits's Modular Textures
// https://youtube.com/playlist?list=PL5KbKbJ6Gf9-1VAsllNBn175nF4fqnBCF
//
// this approach builds a new texture, but this could also be done as a shader, see
// https://en.wikibooks.org/wiki/Cg_Programming/Unity/Layers_of_Textures

//[RequireComponent(typeof(MeshRenderer))]
public class MaterialLayers : MonoBehaviour
{
    Material material;
    public Texture2D[] textureArray;

    Texture2D tex;

    // Start is called before the first frame update
    void Start()
    {
        // look for either a renderer or a skinned mesh renderer, and collect the material
        MeshRenderer rend = GetComponent<MeshRenderer>();
        if(rend != null)
        {
            material = rend.material;
        }
        else
        {
            SkinnedMeshRenderer skinrend = GetComponent<SkinnedMeshRenderer>();
            if(skinrend != null)
            {
                material = skinrend.material;
            }
        }

        // make texture
        tex = CreateLayeredTexture(textureArray);

        // assign to material
        SetMaterialTexture(tex, material); 
    }

    Texture2D CreateLayeredTexture(Texture2D[] layers)
    {
        if (layers.Length == 0)
        {
            Debug.LogError("No image layer info in array!");
            return Texture2D.whiteTexture;
        }
        else if (layers.Length == 1)
        {
            Debug.Log("Only one image layer present, are you sure you need to make a texture?");
            return layers[0];
        }

        // create texture
        Texture2D newTexture = new Texture2D(layers[0].width, layers[0].height);

        // destination texture's pixels
        Color[] colorArray = new Color[newTexture.width * newTexture.height];

        // array of color extracted from source textures
        Color[][] srcArray = new Color[layers.Length][];

        // populate source
        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i].width != newTexture.width || layers[i].height != newTexture.height)
            {
                Debug.LogError("Layer dimensions of layer[" + i + "] do not match base layer.");
                return Texture2D.whiteTexture;
            }
            srcArray[i] = layers[i].GetPixels();
        }


        // set pixles
        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                int pixelIndex = x + (y * newTexture.width);
                for (int i = 0; i < layers.Length; i++)
                {
                    Color srcPixel = srcArray[i][pixelIndex];
                    if (srcPixel.a == 1)
                    {
                        colorArray[pixelIndex] = srcPixel;
                    }
                    else if (srcPixel.a > 0)
                    {
                        colorArray[pixelIndex] = NormalBlend(colorArray[pixelIndex], srcPixel);
                    }
                }
                
            }
        }
        newTexture.SetPixels(colorArray);
        newTexture.Apply(); // upload changes to graphics card

        // set texture settings
        newTexture.wrapMode = TextureWrapMode.Clamp;
        //newTexture.filterMode = FilterMode.Point;

        return newTexture;
    }


    void SetMaterialTexture(Texture2D texture, Material material)
    {
        // set texture to material for display
        material.SetTexture("_MainTex", texture);
    }

    Color NormalBlend(Color baseColor, Color blendColor)
    {
        // src and dest alphas need to add up to 1.0.
        // so we'll take the alpha from src, scale what's left by the dest
        // thus destAlpha + srcAlpha <= 1.0
        float blendColorAlpha = blendColor.a;
        float baseColorAlpha = (1.0f - blendColorAlpha) * baseColor.a;

        // for normal blending, just add them together.
        Color baseColorLayer = baseColor * baseColorAlpha; // bottom layer 
        Color blendColorLayer = blendColor * blendColorAlpha;
        return baseColorLayer + blendColorLayer;
    }

    Color MultiplyBlend(Color baseColor, Color blendColor)
    {
        if (baseColor == Color.white) // white becomes blend color
        {
            return blendColor;
        }
        else if(baseColor == Color.black) // black does not change
        {
            return baseColor;
        }

        float blendColorAlpha = blendColor.a;
        float baseColorAlpha = (1.0f - blendColorAlpha) * baseColor.a;

        return (baseColor * baseColorAlpha) * (blendColor * blendColorAlpha);
    }
}
