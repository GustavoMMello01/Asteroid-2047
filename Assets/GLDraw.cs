using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLDraw : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    public float by = -9;
    float bx = 0;
    float velo = 0.05f;
    public float mGL = -2;
    public float mGR = 2;
    bool invert;
    
    
    private void OnPostRender() {
        drawShip();
    }

    private void Update() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            bx -= velo;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            bx += velo;
        }
        Move();
    }

    void Move() {
        

    }

    void drawShip() {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.white);

        GL.Vertex3(bx, by, 0);
        GL.Vertex3(bx+1, by, 0);
        GL.Vertex3(bx + 0.5f, by + 1, 0);
        

        GL.End();
        GL.PopMatrix();
    }
    
}
