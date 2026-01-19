using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBankController : BankController
{
    protected override void Awake() {
        base.Awake();
        _button.enabled = false;
    }
}
