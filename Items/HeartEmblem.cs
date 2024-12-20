using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class HeartEmblem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Heart Insignia");
			base.Tooltip.SetDefault("You respawn with 75% of maximum health and a great regen boost after death");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 28;
			base.item.value = 50000;
			base.item.rare = 3;
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.heartEmblem = true;
		}
	}
}
