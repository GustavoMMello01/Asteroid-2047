using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GLDraw : MonoBehaviour
{
    public Material mat;
    public Vector2 sb;
    public float by = 0;
    float bx = 0;
    float velo = 0.05f;
    List<Asteroid> asteroids = new List<Asteroid>();
    public Text ScoreText;

    private float timer;
    private float score;



    
    
    private void OnPostRender() {
        drawShip();
        foreach (var asteroid in asteroids)
        {
            asteroid.render();
        }
    }

    void Start()
    {  
        
    }


    private void Update() {

        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));


        if(Input.GetKey(KeyCode.LeftArrow)) {
            bx -= velo;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            bx += velo;
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            by -= velo;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            by += velo;
        }

        if (Input.GetKey(KeyCode.Space)) {
            spawnAsteroid();
        }

        sb -= new Vector2(0.5f, 0.5f);


        if (by > sb.y){
            by = sb.y;
        }
        if (by < -sb.y + 0.5f){
            by = -sb.y + 0.5f;
        }
        if (bx > sb.x){
            bx = sb.x;
        }
        if (bx < -sb.x){
            bx = -sb.x;
        }

        var hit = false;

        foreach (var asteroid in asteroids)
        {
            asteroid.Update();
            if (asteroid.isHit(bx, by)){
                hit = true;
            }
        }

        if (hit){
            asteroids.Clear();
        }


        Move();
        addScore();

    }

    void Move() {
        

    }

    void spawnAsteroid(){

        var asteroid = new Asteroid(sb);
        asteroid.Start();
        asteroids.Add(asteroid);

    }

    void drawShip() {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.Begin(GL.TRIANGLES);
        GL.Color(Color.white);

        GL.Vertex3(bx - 0.5f, by - 0.5f, 0);
        GL.Vertex3(bx + 0.5f, by - 0.5f, 0);
        GL.Vertex3(bx, by + 0.5f, 0);
        

        GL.End();
        GL.PopMatrix();

        
    }

    void addScore(){
        timer += Time.deltaTime;

        if (timer > 5f) {

            score += 5f;

            //We only need to update the text if the score changed.
            ScoreText.text = score.ToString();

            //Reset the timer to 0.
            timer = 0;
        }
    }
    
}
