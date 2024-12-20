using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class KSShieldCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Pocket Shield Cooldown");
			base.Description.SetDefault("The shield won't protect you from lethal damage");
			Main.debuff[base.Type] = true;
			Main.pvpBuff[base.Type] = true;
			Main.buffNoSave[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
