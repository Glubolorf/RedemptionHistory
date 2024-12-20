using System;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class GirusCloakBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Cloaked");
			base.Description.SetDefault("\"You are hidden from Girus.\"");
		}
	}
}
