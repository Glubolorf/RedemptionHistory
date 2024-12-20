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
			player.lifeRegen += 4;
			player.manaRegen += 4;
			player.statDefense += 12;
			player.allDamage *= 1.1f;
		}
	}
}
