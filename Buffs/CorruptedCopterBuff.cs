using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class CorruptedCopterBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Attack Minicopter");
			base.Description.SetDefault("\"A Girus Attack Minicopter to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("CorruptedCopter")] > 0)
			{
				modPlayer.corruptedCopter = true;
			}
			if (!modPlayer.corruptedCopter)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
