using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class XenomiteHatchlingBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Hatchling");
			base.Description.SetDefault("\"Probably the cutest infected thing you'll ever see\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<XenomiteHatchling>()] > 0)
			{
				modPlayer.xenoHatchlingMinion = true;
			}
			if (!modPlayer.xenoHatchlingMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
