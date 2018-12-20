﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    LevelManager LM;
    // Use this for initialization
    private Vector3 ini;
    private Vector2 forceVector;
    [Range(1.0f, 300.0f)]
    [Tooltip("ball Speed. Value between 1 and 10.")]
    [Header("Bola")]
    public float Fuerza = 5f;
    public GameObject balls;
    Vector3 pos;
	void Start () {
        ini = this.gameObject.transform.position;
        pos = new Vector3(0, 0, 0);

    }

    
   
    public void Init(LevelManager aux)
    {
        LM = aux;

    }
    // Update is called once per frame
    void Update () {

        Vector3 fin;
        if (Input.GetMouseButtonUp(0))
        {
            //Lllama al levelManager para un nuevo disparo
            LM.NewShot();
            pos= LM.GetPosition();

            fin = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y,0));
            //el screentoworldpoint, te transforma las coordenadas del raton(1920x1080) a un punto de la escena
            forceVector = new Vector2(fin.x - ini.x,fin.y - ini.y);
             
            //borramos la bola que se mantenia para mantener el camino
            GameObject bolaF = GameManager.GM.GetPrimera();
            if (bolaF != null) Destroy(bolaF);

            //Borramos los bloques destruidos

            SpawnBalls((uint)GameManager.GM.nBolas, forceVector);
          
            
                //hacer un for con el numero actual de bolas que hay en el nivel, y llamar con el invoke o con una
            //coroutine al metodo instance balls para instanciar una bola y darle la fuerza.
        }
	}

    void SpawnBalls(uint numballs,Vector2 dir) // Lo llamará posiblemente otro objeto no desde aquí dentro
    {
        StartCoroutine(instanceBalls(numballs,dir));
    }
    IEnumerator instanceBalls(uint numBalls, Vector2 dir){
   ;
       
        for (int i = 0; i < numBalls; i++)
        {
            GameObject aux = Instantiate(balls, pos, Quaternion.identity);
            aux.GetComponent<BallLogic>().setForce(forceVector.normalized * Fuerza);
          
            yield return new WaitForFixedUpdate();// //TODO mas que un fixedUpdate
        }
     

        //instanciar el gameObject balls declarado arriba
        //añadir la fuerza

        //las pelotas estan en una layer de fisica para que no se colisionen entre sí

    }

    

}