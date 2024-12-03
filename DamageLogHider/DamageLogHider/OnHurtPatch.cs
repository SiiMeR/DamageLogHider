using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace DamageLogHider;

public class OnHurtPatch
{
    [HarmonyPatch(typeof(EntityPlayer), nameof(EntityPlayer.OnHurt))]
    public static bool Prefix(EntityPlayer __instance, DamageSource damageSource, float damage)
    {
        if (damageSource?.Source == EnumDamageSource.Player)
        {
            var world = __instance.World;
            var heal = damageSource.Type == EnumDamageType.Heal;
            
            
            var msg = Lang.Get(heal ? "damagelog-heal-byplayer" : "damagelog-damage-byplayer", damage, string.Empty);

            (world.PlayerByUid(__instance.PlayerUID) as IServerPlayer).SendMessage(GlobalConstants.DamageLogChatGroup, msg, EnumChatType.Notification);

            return false;
        }

        return true;
    }
}