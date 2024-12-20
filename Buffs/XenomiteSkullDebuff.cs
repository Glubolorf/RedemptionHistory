using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class XenomiteSkullDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Lab Defensive System");
			base.Description.SetDefault("\"Waves of energy overwhelm your body...\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -999;
			player.moveSpeed *= 0.2f;
		}
	}
}
