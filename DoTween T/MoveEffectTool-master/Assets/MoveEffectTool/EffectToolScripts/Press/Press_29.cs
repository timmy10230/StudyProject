using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MoveEffectTool
{
    public class Press_29 : BasePress
    {
        protected override void Effect()
        {
            base.Effect();
            transform.DOPunchPosition(new Vector3(-40, 0, 0), 0.4f, 4, 0.5f);
            transform.DOPunchRotation(new Vector3(0, 0, 30), 0.4f, 4, 0.5f);
        }
    }
}
