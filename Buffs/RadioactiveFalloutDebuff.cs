using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class RadioactiveFalloutDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Radioactive Fallout");
			base.Description.SetDefault("\"Stats greatly decreased due to radioactivity\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -14;
			player.meleeDamage *= 0.75f;
			player.magicDamage *= 0.75f;
			player.minionDamage *= 0.75f;
			player.rangedDamage *= 0.75f;
			player.statDefense -= 12;
			player.moveSpeed *= 0.5f;
		}
	}
}
