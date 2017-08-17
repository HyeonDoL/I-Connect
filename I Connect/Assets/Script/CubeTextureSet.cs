using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTextureSet : MonoBehaviour
{

    void Awake()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector2[] UVs = new Vector2[mesh.vertices.Length];
        // Front
        UVs[0] = new Vector2(0.0f, 0.0f);
        UVs[1] = new Vector2(0.333f, 0.0f);
        UVs[2] = new Vector2(0.0f, 0.333f);
        UVs[3] = new Vector2(0.333f, 0.333f);
        // Top
        UVs[9] = new Vector2(0.334f, 0.334f);  //16 17 18 19
        UVs[8] = new Vector2(0.334f, 0.666f);
        UVs[4] = new Vector2(0.666f, 0.666f);
        UVs[5] = new Vector2(0.666f, 0.334f);
        // Back
        UVs[6] = new Vector2(1.0f, 0.0f);
        UVs[7] = new Vector2(0.667f, 0.0f);
        UVs[10] = new Vector2(1.0f, 0.333f);
        UVs[11] = new Vector2(0.667f, 0.333f);
        // Bottom
        UVs[12] = new Vector2(0.0f, 0.334f);
        UVs[13] = new Vector2(0.0f, 0.666f);
        UVs[14] = new Vector2(0.333f, 0.666f);
        UVs[15] = new Vector2(0.333f, 0.334f);
        // Left
        UVs[17] = new Vector2(0.334f, 0.333f);
        UVs[18] = new Vector2(0.666f, 0.333f);
        UVs[16] = new Vector2(0.334f, 0.0f);
        UVs[19] = new Vector2(0.666f, 0.0f);
        // Right        
        UVs[20] = new Vector2(0.667f, 0.334f);
        UVs[21] = new Vector2(0.667f, 0.666f);
        UVs[22] = new Vector2(1.0f, 0.666f);
        UVs[23] = new Vector2(1.0f, 0.334f);
        mesh.uv = UVs;
    }
}