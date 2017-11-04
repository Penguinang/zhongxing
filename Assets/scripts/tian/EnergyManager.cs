using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {

    private SpriteRenderer EnergyBar;
    

    public float energy;
    public int energy_update;


    public Image Energy;
    public Image EnergyUpdate;
    public Text Amout;
    public bool launchable;
    public float ShipEnergy;


    private void Update()
    {

        // Energy.fillAmount -= Time.deltaTime;
        Energy.fillAmount = energy/100;
        
        if(EnergyUpdate.fillAmount>Energy.fillAmount)
        {
            EnergyUpdate.fillAmount -= Time.deltaTime/5;
        }
        else if (EnergyUpdate.fillAmount < Energy.fillAmount)
        {
            EnergyUpdate.fillAmount += Time.deltaTime / 5;
        }
        if(energy-ShipEnergy>=0)
        {
            launchable = true;
        }
        else
        {
            launchable = false;
        }
        



    }
    private void FixedUpdate()
    {
        if (energy_update > energy)
        {
            energy_update -= 1; 
        }
        Amout.GetComponent<Text>().text = energy_update + "%";
    }
    public void IncreaseEnergy(float e)
    {
        this.energy += e;
    }
    public void DecreaseEnergy(float e)
    {
        this.energy -= e;
    }

}
