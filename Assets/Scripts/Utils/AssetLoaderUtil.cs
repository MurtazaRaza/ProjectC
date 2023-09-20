using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Utils
{
    public class AssetLoaderUtil
    {
        public static async Task<Sprite> LoadSpriteAsync(AssetReferenceSprite spriteReference)
        {
            AsyncOperationHandle<Sprite> handle = spriteReference.LoadAssetAsync();

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            else
            {
                Debug.LogError("Failed to load sprite: " + handle.OperationException);
                return null;
            }
        }
    }
}
