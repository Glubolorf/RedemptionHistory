using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class Mk2MicrobotBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Mk-2 Microbot");
			base.Description.SetDefault("\"Boop beep\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Mk2Microbot>()] > 0)
			{
				modPlayer.mk2MicrobotMinion = true;
			}
			if (!modPlayer.mk2MicrobotMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
