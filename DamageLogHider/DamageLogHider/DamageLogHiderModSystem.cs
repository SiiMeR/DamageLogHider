using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;
using Vintagestory.Client.NoObf;

namespace DamageLogHider;

public class DamageLogHiderModSystem : ModSystem
{
    public static ICoreServerAPI serverApi;

    public override void StartServerSide(ICoreServerAPI api)
    {
        serverApi = api;
        var harmony = new Harmony(Mod.Info.ModID);

        var original = AccessTools.Method(typeof(EntityPlayer), nameof(EntityPlayer.OnHurt));
        var patched = AccessTools.Method(typeof(OnHurtPatch), nameof(OnHurtPatch.Prefix));
        
        harmony.Patch(original, prefix: new HarmonyMethod(patched));
    }

}