using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private bool isPlaced = false;
    [SerializeField] GameObject placeablePrefab;
    [SerializeField] GameObject placeHolder;
    
    [SerializeField] private List<Validater> validaters;
    
    Dictionary<Renderer,Material> RendererMaterials = new Dictionary<Renderer, Material>();
    
    public void SetPlaceHolderPosition(Vector3 position)
    {
        transform.position = position;
    }
    public bool IsValidPlacement(Vector3 position)
    {
        foreach (var validater in validaters)
        {
            if (!validater.IsValid())
            {
                return false;
            }
        }

        return true;
    }
    
    public bool TryPlace()
    {
        if (IsValidPlacement(transform.position))
        {
            Place();
            return true;
        }

        return false;
    }

    private void Place()
    {
        Instantiate(placeablePrefab, placeHolder.transform.position, placeHolder.transform.rotation);
    }

    public void SetPlaceHolderColor(Color color)
    {
        foreach (var renderer in RendererMaterials.Keys)
        {
            renderer.material.color = color;
        }
        
    }

    private void Awake()
    {
        validaters = GetComponents<Validater>().ToList();
        //Cashe all renderers materials
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            if (!RendererMaterials.ContainsKey(renderer))
            {
                RendererMaterials.Add(renderer, renderer.material);
            }
        }

    }
}