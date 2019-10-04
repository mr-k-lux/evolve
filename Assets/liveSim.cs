using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class liveSim : MonoBehaviour {
    int nutrition, oxidation, decarbonisation;
   public int glucose, oxygen, co2, proteins;

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
        InvokeRepeating("SecondUpdate", 0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        gl.text = "glucose: " + glucose;
        ox.text = "oxygen: " + oxygen;
        co.text = "co2: " + co2;
        pr.text = "proteins: " + proteins;
	}

    void SecondUpdate()
    {
        glucose += nutrition;
        oxygen += oxidation;
        co2 -= decarbonisation;
	    if (co2 < 0)
            co2 = 0;
        if(glucose>50 && oxygen>50 && co2 < 60)
        {
            glucose -= 15;
            oxygen -= 20;
            co2 += 35;
            proteins++;   
        }
        else
        {
            glucose -= 10;
            oxygen -= 10;
            co2 += 20;
        }
        if(glucose<=0 || oxygen<=0 || co2 >= 90)
        {
            Destroy(gameObject);
        }
        if (proteins == 10)
        {

            GameObject a = Instantiate(cell);
            liveSim dgt = a.GetComponent<liveSim>();

            proteins = 0;
            glucose = (glucose - 20) / 2;
            oxygen = (oxygen - 20) / 2;
            co2 = (co2 + 20) / 2;
            dgt.glucose = (glucose - 20) / 2;
            dgt.oxygen = (oxygen - 20) / 2;
            dgt.co2 = (co2 + 20) / 2;
        }
    }
}
