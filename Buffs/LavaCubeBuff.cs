using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class LavaCubeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lava Cube");
			base.Description.SetDefault("\"A molten cube to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<LavaCubeMinion>()] > 0)
			{
				modPlayer.lavaCubeMinion = true;
			}
			if (!modPlayer.lavaCubeMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
