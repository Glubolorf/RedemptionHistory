using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class StatuetteOfFaith : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Statuette of Faith");
			base.Tooltip.SetDefault("'Have a little faith'\nTemporarily creates a large Celestine Dreamsong aura around the location of use\nPlayers in the aura will see better in the Soulless Caverns");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.useStyle = 4;
			base.item.useTurn = true;
			base.item.noUseGraphic = false;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 22;
			base.item.height = 44;
			base.item.maxStack = 5;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
			base.item.shoot = ModContent.ProjectileType<StatuetteOfFaithPro>();
			base.item.shootSpeed = 0f;
		}
	}
}
