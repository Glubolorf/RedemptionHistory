using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class CombatChickenBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Combat Chicken");
			base.Description.SetDefault("\"You are the King Chicken now!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<CombatChicken>()] > 0)
			{
				modPlayer.chickenMinion = true;
			}
			if (!modPlayer.chickenMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
