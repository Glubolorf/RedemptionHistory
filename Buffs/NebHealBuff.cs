using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class NebHealBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Vigorous Aura");
			base.Description.SetDefault("\"A strange aura is healing you...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen += 5;
		}
	}
}
