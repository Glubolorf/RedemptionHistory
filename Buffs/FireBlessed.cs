using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class FireBlessed : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Blessed");
			base.Description.SetDefault("You are blessed with Icar's Divine Protection...");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += 10;
			player.manaRegen += 5;
			player.magicDamage *= 1.05f;
			player.meleeDamage *= 1.05f;
			player.rangedDamage *= 1.05f;
			player.minionDamage *= 1.05f;
			player.thrownDamage *= 1.05f;
		}
	}
}
