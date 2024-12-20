using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class BrokenBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Broken Blade");
			base.Tooltip.SetDefault("Hitting enemies with melee swings has a chance to summon a Phantom Cleaver above their heads\n50% increased melee swing damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 48;
			base.item.value = Item.sellPrice(0, 4, 0, 0);
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.brokenBlade = true;
			redePlayer.trueMeleeDamage += 0.5f;
		}
	}
}
