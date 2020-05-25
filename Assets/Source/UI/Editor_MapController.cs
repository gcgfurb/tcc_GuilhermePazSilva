﻿using Assets.Source;
using UnityEngine;

public class Editor_MapController : MonoBehaviour
{
    public GameObject tilePrefab;

    public void GenerateCurrentMap()
    {
        var width = Global.currentMap.width;
        var height = Global.currentMap.height;

        GenerateLayer(Layers.Terrain.ToString(), width, height, 3);
        GenerateLayer(Layers.Construction.ToString(), width, height, 2);
        GenerateLayer(Layers.Above.ToString(), width, height, 1);
        GenerateLayer(Layers.Entities.ToString(), width, height, 0, false);

        Camera.main.transform.position = new Vector3(transform.position.x + width / 2, transform.position.y + height / 2, Camera.main.transform.position.z);
    }

    void GenerateLayer(string name, int width, int height, int depth, bool populate = true)
    {
        var layer = new GameObject(name);
        layer.transform.parent = transform;
        layer.transform.localPosition = new Vector3(0, 0, depth);

        if (populate)
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var obj = Instantiate(tilePrefab, layer.transform, true);
                    obj.transform.localPosition = new Vector3(x, y, 0);
                    obj.GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }
    }

    public Material lineMaterial;
    public void OnRenderObject()
    {
        lineMaterial.SetPass(0);
        
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix * Matrix4x4.Translate(new Vector3(-0.5f, -0.5f)));

        var width = Global.currentMap.width;
        var height = Global.currentMap.height;

        // grid
        GL.Begin(GL.LINES);
        GL.Color(new Color(1, 1, 1, 0.5f));

        for (var y = 0; y < height; y++)
        {
            GL.Vertex3(0, y, 0);
            GL.Vertex3(width, y, 0);
        }

        for (var x = 0; x < width; x++)
        {
            GL.Vertex3(x, 0, 0);
            GL.Vertex3(x, height, 0);
        }

        GL.End();

        // borders
        GL.Begin(GL.QUADS);
        GL.Color(Color.white);

        var lineThickness = 0.1f;
        
        // left
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, height, 0);
        GL.Vertex3(lineThickness, height, 0);
        GL.Vertex3(lineThickness, 0, 0);

        // up
        GL.Vertex3(0, height, 0);
        GL.Vertex3(width, height, 0);
        GL.Vertex3(width, height - lineThickness, 0);
        GL.Vertex3(0, height - lineThickness, 0);

        // right
        GL.Vertex3(width - lineThickness, height, 0);
        GL.Vertex3(width, height, 0);
        GL.Vertex3(width, 0, 0);
        GL.Vertex3(width - lineThickness, 0, 0);

        // down
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, lineThickness, 0);
        GL.Vertex3(width, lineThickness, 0);
        GL.Vertex3(width, 0, 0);

        GL.End();
        GL.PopMatrix();
    }
}
