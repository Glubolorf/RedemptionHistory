using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Cooldowns
{
	public class ShadePortalCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Shade Portal Cooldown");
			base.Description.SetDefault("\"You cannot enter Shade Portals\"");
			Main.buffNoTimeDisplay[base.Type] = false;
			Main.debuff[base.Type] = true;
			this.canBeCleared = false;
		}
	}
}
