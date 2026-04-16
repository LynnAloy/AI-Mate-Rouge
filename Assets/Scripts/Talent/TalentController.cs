using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentController : Singleton<TalentController>
{
    [SerializeField] private List<TalentSO> talentList;

    public Action OnTalentListReady;
    public Action<TalentSO> OnRequestApplyTalent;
    private Queue<TalentSO> pending = new();

    //Variables from talentSO

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        TalentUIController.Instance.SetUpTalentButton();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestApplyTalent(TalentSO talent)
    {
        pending.Enqueue(talent);
        OnRequestApplyTalent?.Invoke(talent);
    }

    public TalentSO[] GetPending() => pending.ToArray();

    public void MarkHandled(TalentSO t)
    {
        var list = new List<TalentSO>(pending);
        list.Remove(t);
        pending = new Queue<TalentSO>(list);
    }



    public List<TalentSO> GetTalentList()
    {
        return talentList;
    }
}
