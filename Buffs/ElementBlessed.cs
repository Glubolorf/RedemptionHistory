using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class ElementBlessed : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Blessed");
			base.Description.SetDefault("\"You are blessed with Icar's Divine Protection...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += 15;
			player.manaRegen += 15;
			player.statDefense += 12;
			player.magicDamage *= 1.1f;
			player.meleeDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.minionDamage *= 1.1f;
			player.thrownDamage *= 1.1f;
		}
	}
}
