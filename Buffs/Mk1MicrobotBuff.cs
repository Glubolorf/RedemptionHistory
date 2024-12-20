using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class Mk1MicrobotBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Mk-1 Microbot");
			base.Description.SetDefault("\"Beep boop\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Mk1Microbot>()] > 0)
			{
				modPlayer.mk1MicrobotMinion = true;
			}
			if (!modPlayer.mk1MicrobotMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
