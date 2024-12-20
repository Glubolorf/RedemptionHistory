using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class PowerSurgeBuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Power Surge");
			base.Description.SetDefault("\"Your armour surges with energy\"");
			Main.buffNoTimeDisplay[base.Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.magicDamage += 0.25f;
			player.minionDamage += 0.25f;
			player.longInvince = true;
			player.manaCost *= 0f;
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					int index = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 269, 0f, 0f, 100, default(Color), 2f);
					Main.dust[index].noGravity = true;
					Dust dust = Main.dust[index];
					dust.velocity.X = dust.velocity.X - player.velocity.X * 0.5f;
					dust.velocity.Y = dust.velocity.Y - player.velocity.Y * 0.5f;
				}
			}
		}
	}
}
