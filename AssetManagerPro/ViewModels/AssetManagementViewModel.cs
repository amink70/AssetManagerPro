using System.Collections.ObjectModel;
using AssetManagerPro.Models;
using AssetManagerPro.Repositories;

namespace AssetManagerPro.ViewModels;

public class AssetManagementViewModel
{
    private readonly AssetRepository _repository = new();

    public ObservableCollection<AssetDisplay> Assets { get; } = new();

    public AssetManagementViewModel()
    {
        LoadAssets();
    }

    public void LoadAssets()
    {
        Assets.Clear();

        foreach (var asset in _repository.GetAll())
        {
            Assets.Add(asset);
        }
    }
    public void Search(string text)
    {
        Assets.Clear();

        List<AssetDisplay> list;

        if (string.IsNullOrWhiteSpace(text))
        {
            list = _repository.GetAll();
        }
        else
        {
            list = _repository.Search(text);
        }

        foreach (var asset in list)
        {
            Assets.Add(asset);
        }
    }





}