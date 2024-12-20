using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs.Debuffs
{
	public class HazardLaserDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Incinerated!");
			base.Description.SetDefault("\"You are being lasered!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.lifeRegen = -1500;
			Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 235, 0f, 0f, 100, default(Color), 2f);
		}
	}
}
