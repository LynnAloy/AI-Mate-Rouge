using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : Singleton<DamageNumberController>
{
    [SerializeField] private DamagerNumber damageNumber;
    [SerializeField] private Transform damageNumberCanvas;

    private List<DamagerNumber> damagerNumberPool = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDamageNumber(float damageAmount, Vector3 position)
    {
        int round = Mathf.RoundToInt(damageAmount);
        //DamagerNumber damageNumber = Instantiate(this.damageNumber, position, Quaternion.identity, damageNumberCanvas);
        DamagerNumber newDamageNumber = GetDamagerNumberFromPool();
        newDamageNumber.Setup(round);
        newDamageNumber.gameObject.SetActive(true);
        newDamageNumber.transform.position = position;
    }

    public DamagerNumber GetDamagerNumberFromPool()
    {
        DamagerNumber numberToOutput = null;
        if(damagerNumberPool.Count == 0)
        {
            numberToOutput = Instantiate(damageNumber, damageNumberCanvas);
        }
        else
        {
            numberToOutput = damagerNumberPool[0];
            damagerNumberPool.RemoveAt(0);
        }
        return numberToOutput;
    }

    public void AddToPool(DamagerNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);
        damagerNumberPool.Add(numberToPlace);
    }
}
