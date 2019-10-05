using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class liveSim : MonoBehaviour {
    int nutrition, oxidation, decarbonisation;
    public int glucose, oxygen, co2, proteins;
   
    public int atf;

    public GameObject cell;
    public TextMesh gl;
    public TextMesh ox;
    public TextMesh co;
    public TextMesh pr;
    // Use this for initialization
    void Start () {
        nutrition = 12;
        oxidation = 20;
        decarbonisation = 30;

        glucose = 40;
        oxygen = 40;
        co2 = 0;
        proteins = 0;
        atf = 0;
        InvokeRepeating("SecondUpdate", 0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        gl.text = "glucose: " + glucose;
        ox.text = "oxygen: " + oxygen;
        co.text = "co2: " + co2;
        pr.text = "proteins: " + proteins;
	}

    void SecondUpdate() //life cycle
    {
        glucose += nutrition;
        oxygen += oxidation;
        co2 -= decarbonisation;
        if (co2 < 0) co2 = 0;
        if (oxygen > 100) oxygen = 100;
        if (glucose > 100) glucose = 100;
        constMove();

        if (glucose>50 && oxygen>50 && co2<60 && atf>30) //gain of breeding proteins
        {
            glucose -= 15;
            oxygen -= 20;
            atf -= 15;
            co2 += 40;
            proteins++;   
        }
        else //breathe
        {
            breathe();
        }
        if(glucose<=0 || oxygen<=0 || co2 >= 90) //death
        {
            Destroy(gameObject);
        }
        if (proteins == 10) //breeding itself
        {

            GameObject a = Instantiate(cell);
            liveSim dgt = a.GetComponent<liveSim>();

            proteins = 0;
            glucose = (glucose - 20) / 2;
            oxygen = (oxygen - 20) / 2;
            co2 = (co2 + 20) / 2;
            atf = (atf + 20) / 2;
            dgt.glucose = (glucose - 20) / 2;
            dgt.oxygen = (oxygen - 20) / 2;
            dgt.co2 = (co2 + 20) / 2;
            dgt.atf = (atf + 20) / 2;
        }
    }

    void breathe()
    {
         glucose -= 10;
         oxygen -= 10;
         co2 += 20;
        if (glucose > 0 && oxygen > 0) atf += 10;
    }
    void constMove()
    {
        atf -= 4;
        //some moving action like move=true; and while it's true it'll move
    }
}
