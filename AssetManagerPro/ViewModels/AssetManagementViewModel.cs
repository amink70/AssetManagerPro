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
}