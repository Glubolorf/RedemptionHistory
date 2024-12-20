using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class HeavyRadiationDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Heavy Radiation");
			base.Description.SetDefault("\"Stats greatly decreased due to radioactivity\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
			player.lifeRegen = -4;
			player.allDamage *= 0.75f;
			player.statDefense -= 18;
			player.moveSpeed *= 0.5f;
		}
	}
}
