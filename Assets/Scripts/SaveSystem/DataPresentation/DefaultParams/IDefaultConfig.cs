using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefaultConfig<TParams>
{
    public TParams GetParamsObject();
}
