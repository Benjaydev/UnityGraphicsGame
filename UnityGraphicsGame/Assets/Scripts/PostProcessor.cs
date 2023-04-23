using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessor : MonoBehaviour
{
    public List<Material> mats;

    private void Awake()
    {

        if (Camera.main != null && Camera.main.depthTextureMode != DepthTextureMode.Depth)
            Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    public void ApplyMaterial(Material mat)
    {
        mats.Add(mat);
    }
    public void RemoveMaterial(Material mat)
    {
        mats.Remove(mat);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(mats.Count == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }
        else if(mats.Count <= 1)
        {
            Graphics.Blit(source, destination, mats[0]);
            return;
        }


        // Create two temporary textures from the source which all materials will be applied to
        RenderTexture tempSource = RenderTexture.GetTemporary(source.width, source.height, source.depth, source.format);
        RenderTexture tempDestination = RenderTexture.GetTemporary(source.width, source.height, source.depth, source.format);

        Graphics.Blit(source, tempSource); //blit the source into the tempSrc;

        RenderTexture finalTexture = tempSource;
        for (int i = 0; i < mats.Count; i++)
        { 
            // Alternate between both temporary textures applying materials
            if ((float)i % 2.0f == 0.0f)
            {
                Graphics.Blit(tempSource, tempDestination, mats[i]);
                finalTexture = tempDestination;
            }
            else
            {
                Graphics.Blit(tempDestination, tempSource, mats[i]);
                finalTexture = tempSource;
            }
        }

        // Apply the final texture to the destination
        Graphics.Blit(finalTexture, destination);

        // Release temp textures
        RenderTexture.ReleaseTemporary(tempSource);
        RenderTexture.ReleaseTemporary(tempDestination);
    }
}

