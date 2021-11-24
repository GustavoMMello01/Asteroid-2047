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
    float momentumx = 0;
    float momentumy = 0;
    float velo = 0.05f;
    List<Asteroid> asteroids = new List<Asteroid>();
    List<Projectile> projectiles = new List<Projectile>();
    public Text ScoreText;
    public Text LifeText;
    private float timerAsteroid;
    private float timerAsteroidMs = 0.5f;
    private float timerProjectile;
    private float timer;
    private float score;
    private int life = 3;
    private int Life { get => life;
        set {
            life = value;
            timerAsteroidMs = 2;
            if (life == 0)
            {
                score = 0;
                life = 3;
                menu.SetActive(true);
                inGame.SetActive(false);
            }
            LifeText.text = life.ToString();
        }
    }

    public GameObject menu;
    public GameObject inGame;

    private void OnPostRender() {
        drawShip();
        foreach (var asteroid in asteroids)
        {
            asteroid.Render(mat);
        }
        foreach (var projectile in projectiles)
        {
            projectile.Render();
        }

    }

    void Start()
    {  
        
    }


    private void Update() {

        if (!inGame.activeSelf)
        {
            return;
        }

        sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float velox = 0;
        float veloy = 0;
        if(Input.GetKey(KeyCode.LeftArrow)) {
            velox -= velo;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            velox += velo;
        }
        if(Input.GetKey(KeyCode.DownArrow)) {
            veloy -= velo;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            veloy += velo;
        }

        momentumx = System.Math.Min(velox * 2 * Time.deltaTime + momentumx * ( 1 - 0.75f * Time.deltaTime), velo);
        momentumy = System.Math.Min(veloy * 2 * Time.deltaTime + momentumy * (1 - 0.75f * Time.deltaTime), velo);
        bx += momentumx;
        by += momentumy;

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

        foreach (var projectile in projectiles)
        {
            projectile.Update();
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
            Life -= 1;
            asteroids.Clear();
        }

        asteroids.RemoveAll(a => {
            foreach (var projectile in projectiles)
            {
                if (projectile.isHit(a))
                {
                    return true;
                }
            }
            return a.isOutOfScreen();
           });

        projectiles.RemoveAll(p => p.isOutOfScreen());

        addProjectile();
        addAsteroid();
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

    void addProjectile()
    {
        timerProjectile += Time.deltaTime;

        if(timerProjectile > 0.4f && Input.GetKey(KeyCode.Space))
        {
            timerProjectile = 0;
            projectiles.Add(new Projectile(bx, by, sb));
        }
    }


    void addAsteroid()
    {
        timerAsteroid += Time.deltaTime;

        if (timerAsteroid > timerAsteroidMs)
        {
            spawnAsteroid();
            timerAsteroidMs = Random.Range(0.005f, 0.4f );
            timerAsteroid = 0;
        }
    }



}
