using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class PocketShieldProjector : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pocket-Shield Generator");
			base.Tooltip.SetDefault("Lethal damage will cause the player to summon a shield that can protect from a small amount of damage before breaking");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(3, 9));
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).ksShieldGenerator = true;
		}
	}
}
