using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager
{
    // Start is called before the first frame update
    string State { get; set; }
    void Initialize();
     
}
