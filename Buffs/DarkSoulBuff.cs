using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class DarkSoulBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Dark Soul");
			base.Description.SetDefault("A Dark Soul to fight for you");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.ownedProjectileCounts[base.mod.ProjectileType("DarkSoulMinion")] > 0)
			{
				redePlayer.darkSoulMinion = true;
			}
			if (!redePlayer.darkSoulMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
