using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class EaglecrestMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Minion");
			base.Description.SetDefault("\"A little golem to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>(base.mod);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("EaglecrestMinion")] > 0)
			{
				modPlayer.eaglecrestMinion = true;
			}
			if (!modPlayer.eaglecrestMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
