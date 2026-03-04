using System;
using System.Collections;
using System.Data;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public interface ITargetable {
    Transform transform { get; }
    bool getCurrentlyMatched();
}