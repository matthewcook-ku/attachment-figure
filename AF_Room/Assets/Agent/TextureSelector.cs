using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureSelector : MonoBehaviour
{
    public Texture2D[] Textures;
    [Tooltip("GameObject with attached renderer to control. If none is provided the current GameObject is used.")]
    public Renderer MeshRenderer;

    public int CurrentTextureIndex { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // if no renderer is proivided, look for one on this game object.
        if(MeshRenderer == null)
            MeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (MeshRenderer == null)
            MeshRenderer = GetComponent<MeshRenderer>();
        if (MeshRenderer == null)
            Debug.LogError("TextureSelector: no renderer provided!");

        if (Textures.Length == 0)
        {
            Debug.LogError("TextureSelector: texture set is empty!");
            MeshRenderer.material.SetTexture("_MainTex", Texture2D.redTexture);
        }

        SetTexture(0);
    }

    public void SetTexture(int index)
    {
        if(index >= Textures.Length)
        {
            Debug.LogError("TextureSelector: texture index out of range!");
            return;
        }
        CurrentTextureIndex = index;
        MeshRenderer.material.SetTexture("_MainTex", Textures[CurrentTextureIndex]);
    }

    public int CycleNextTexture()
    {
        int index = (CurrentTextureIndex + 1) % Textures.Length;
        SetTexture(index);
        return index;
    }
    public int CyclePrevTexture()
    {
        int index = ((CurrentTextureIndex - 1) + Textures.Length) % Textures.Length;
        SetTexture(index);
        return index;
    }
}
