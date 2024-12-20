using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class FrightBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Aura of Fright");
			base.Description.SetDefault("\"All damage and critical strike chance increased\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.pvpBuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.allDamage += 0.08f;
			player.magicCrit += 8;
			player.meleeCrit += 8;
			player.rangedCrit += 8;
			player.thrownCrit += 8;
		}
	}
}
