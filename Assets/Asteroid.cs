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

    public Asteroid(Vector2 sb){
        var random = new Random();
        bx = (float) random.NextDouble() * sb.x * 2 - sb.x;
        by = sb.y;

    }

    // Start is called before the first frame update
    public void Start()
    {
       var random = new Random();
        velox = (float) Math.Max(random.NextDouble(), 0.2) * 0.01f - 0.005f;
        veloy = (float) Math.Max(random.NextDouble(), 0.2) * -0.01f;
    }

    // Update is called once per frame
    public void Update()
    {
        by += veloy;
        bx += velox;
        ticks ++;
    }

    public void render(){

        var inc = (2 * Mathf.PI)/6;
        var offset = ticks * 0.01f;

        GL.PushMatrix();
        GL.Color(Color.white);
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

        return dist <= 2;
    }


}
