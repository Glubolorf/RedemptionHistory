using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class StaticStunDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Static Shock!");
			base.Description.SetDefault("\"You are stunned by electricity!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.wingTime = 0f;
			player.velocity.X = player.velocity.X + (float)Main.rand.Next(-4, 5);
			player.velocity.Y = player.velocity.Y + (float)Main.rand.Next(-4, 5);
			player.position = player.oldPosition;
		}
	}
}
