using System;
using Redemption.Items.Weapons.HM.Druid;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Minions
{
	public class SpiritWyvernBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Spirit Wyvern");
			base.Description.SetDefault("Summons a Spirit Wyvern to fight for you");
			Main.buffNoSave[base.Type] = true;
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SpiritWyvernHead>()] > 0)
			{
				modPlayer.spiritWyvern1 = true;
			}
			if (!modPlayer.spiritWyvern1)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
				return;
			}
			player.buffTime[buffIndex] = 1200;
		}
	}
}
