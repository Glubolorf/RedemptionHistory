using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class XenoEyeSBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Eye");
			base.Description.SetDefault("\"You have a Xenomite Eye to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("XenomiteEyeS")] > 0)
			{
				modPlayer.xenoMinion = true;
			}
			if (!modPlayer.xenoMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
