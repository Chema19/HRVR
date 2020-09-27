using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseEntity<T> 
{
    public T Data;
    public Boolean Error;
    public String Message;
}
