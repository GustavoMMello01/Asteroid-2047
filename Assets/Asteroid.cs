using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class Asteroid
{
    float by = 0;
    float bx = 0;
    float velox = 0.005f;
    float veloy = 0.005f;
    float radius = 0.5f;
    int ticks = 0;
    float maxy;
    float variance;
    float color;

    public Asteroid(Vector2 sb){
        var random = new Random();
        radius = (float)random.NextDouble() * 0.7f + 0.2f;
        bx = (float) random.NextDouble() * sb.x * 2 - sb.x;
        maxy = sb.y + 2 * radius;
        by = maxy;
        variance = (float) random.NextDouble() - 0.5f;
        color = (float) random.NextDouble() * 0.5f + 0.5f;
    }

    public void Start()
    {
       var random = new Random();
        velox = (float) Math.Max(random.NextDouble(), 0.4) * 0.01f - 0.005f;
        veloy = (float) Math.Max(random.NextDouble(), 0.4) * -0.01f;
    }

    public void Update()
    {
        by += veloy;
        bx += velox;
        ticks ++;
    }

    public void Render(Material mat){

        var inc = (2 * Mathf.PI)/(int)Math.Max(Math.Min(radius * 10, 8), 2);
        var offset = ticks * 0.04f * variance;

        GL.PushMatrix();
        mat.SetPass(0);
        GL.Color(new Color(0, color, color));
        GL.Begin(GL.LINES);

        for(float t=0.0f; t < (2 * Mathf.PI); t += inc)
        {
            GL.Vertex3(Mathf.Cos(t + offset)*radius + bx,Mathf.Sin(t + offset) * radius + by, 0);
            GL.Vertex3(Mathf.Cos(t + inc + offset)*radius + bx,Mathf.Sin(t + inc + offset) * radius + by, 0);
            
        }

        GL.End();
        GL.PopMatrix();

    }

    public bool isHit(float x, float y){
        var dist = Math.Pow(x - bx, 2) + Math.Pow(y - by, 2);

        return dist <= radius;
    }

    public bool isOutOfScreen()
    {
        return by < -maxy;
    }

    public bool hasBeenHit(float x, float y)
    {
        return by + radius > y - 0.25f && by - radius  < y + 0.25f && bx + radius > x && bx - radius < x;
    }

}
