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
			base.Description.SetDefault("\"You are blessed with Icar's Divine Protection...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += 2;
			player.manaRegen += 2;
			player.allDamage *= 1.05f;
		}
	}
}
