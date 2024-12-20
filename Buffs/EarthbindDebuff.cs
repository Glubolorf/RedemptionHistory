using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Buffs
{
	public class EarthbindDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			base.DisplayName.SetDefault("Earthbind");
			base.Description.SetDefault("\"You are being forced back to the earth!\"");
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.debuff[base.Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.velocity.Y = player.velocity.Y + 5f;
			if (player.velocity.Y > 80f)
			{
				player.velocity.Y = 80f;
			}
			Point point = Utils.ToTileCoordinates(player.Bottom);
			if (Main.tile[point.X, point.Y + 1].type != 0 || Main.tile[point.X, point.Y].type != 0)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}
