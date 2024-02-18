using System.Collections.Generic;
using UnityEngine;

public abstract class SaveLoader<TParams, TService> : ISaveLoader
{
    public void Load(IGameRepository gameRepository)
    {

        if (gameRepository.TryGetData<TParams>(out TParams paramsData))
        {
            SetupParamsData(paramsData);
        }
        else
        {
            LoadDefault();
        }
    }

    public void Save(IGameRepository gameRepository)
    {
        TParams paramsData = ConvertDataToParams();
        gameRepository.SetData<TParams>(paramsData);
    }

    protected abstract void SetupParamsData(TParams paramsData);

    protected abstract void LoadDefault();

    protected abstract TParams ConvertDataToParams();
}