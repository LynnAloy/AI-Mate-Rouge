using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamagerNumber : MonoBehaviour
{
    //References
    [SerializeField] private TMP_Text damageText;

    //Variables
    [SerializeField] private float lifeTime;
    [SerializeField] private float flowSpeed;

    private float lifeTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeTimeCounter > 0f)
        {
            lifeTimeCounter -= Time.deltaTime;

            if(lifeTimeCounter <= 0f)
            {
                //Destroy(gameObject);
                DamageNumberController.Instance.AddToPool(this);
            }
        }
        transform.position += flowSpeed * Time.deltaTime * Vector3.up;
    }

    public void Setup(int damageAmount)
    {
        lifeTimeCounter = lifeTime;
        damageText.text = damageAmount.ToString();
    }
}
