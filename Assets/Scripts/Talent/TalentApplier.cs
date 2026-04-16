using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentApplier : MonoBehaviour
{
    private void Start()
    {
        if (TalentController.Instance != null)
        {
            TalentController.Instance.OnRequestApplyTalent += OnRequest;
            foreach(var talent in TalentController.Instance.GetPending())
            {
                ApplyTalent(talent);
                TalentController.Instance.MarkHandled(talent);
            }
        }
    }


    private void OnDisable()
    {
        if (TalentController.Instance != null)
        {
            TalentController.Instance.OnRequestApplyTalent -= OnRequest;
        }
    }

    private void OnRequest(TalentSO talent)
    {
        ApplyTalent(talent);
        TalentController.Instance.MarkHandled(talent);
    }

    private void ApplyTalent(TalentSO talentSO)
    {
        if (talentSO.PlayerMaxHealthUpRatio != 1)
        {
            PlayerHealthController.Instance.SetMaxHealthExternal(talentSO.PlayerMaxHealthUpRatio);
        }
        if (talentSO.PlayerMoveSpeedUpRatio != 1)
        {
            PlayerController.Instance.SetPlayerMoveSpeedExternal(talentSO.PlayerMoveSpeedUpRatio);
        }
        if (talentSO.IsSEE)
        {
            PlayerController.Instance.SetIsSEEExternal(true);
        }
    }
}
