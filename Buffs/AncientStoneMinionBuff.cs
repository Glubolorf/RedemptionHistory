using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class AncientStoneMinionBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Ancient Pebble");
			base.Description.SetDefault("\"A magic pebble to fight for you!\"");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[base.mod.ProjectileType("AncientStoneMinion")] > 0)
			{
				modPlayer.ancientStoneMinion = true;
			}
			if (!modPlayer.ancientStoneMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 18000;
		}
	}
}
