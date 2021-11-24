using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile 
{
    float by = 0;
    float bx = 0;
    float maxy;

    public Projectile(float x, float y, Vector2 sb)
    {
        bx = x;
        by = y;
        maxy = sb.y + 1;
    }

   
    public void Update()
    {
        by += 0.05f;
    }

    public void Render()
    {
        GL.PushMatrix();
        GL.Color(Color.white);
        GL.Begin(GL.LINES);

        GL.Vertex3(bx, by - 0.25f, 0);
        GL.Vertex3(bx, by + 0.25f, 0);

        GL.End();
        GL.PopMatrix();
    }

    public bool isOutOfScreen()
    {
        return by > maxy;
    }

    public bool isHit(Asteroid asteroid)
    {
        return asteroid.hasBeenHit(bx,by);
    }


}
