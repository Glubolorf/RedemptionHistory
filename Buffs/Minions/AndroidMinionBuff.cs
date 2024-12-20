using System;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class AndroidMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Android Minion");
			base.Description.SetDefault("\"A little android to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<AndroidMinionPro>()] > 0)
			{
				modPlayer.androidMinion = true;
			}
			if (!modPlayer.androidMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
