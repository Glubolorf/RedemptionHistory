using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class StarSerpentBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Baby Star Serpent");
			base.Description.SetDefault("Summons a star serpent to fight for you");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("StarSerpentMinionHead")] > 0)
			{
				modPlayer.StarSerpentMinion = true;
			}
			if (!modPlayer.StarSerpentMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
