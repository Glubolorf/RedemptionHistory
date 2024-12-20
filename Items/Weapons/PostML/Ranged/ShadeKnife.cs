using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class ShadeKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Knife");
			base.Tooltip.SetDefault("Knives burst into candle light after a small distance");
		}

		public override void SetDefaults()
		{
			base.item.ranged = true;
			base.item.shootSpeed = 23f;
			base.item.damage = 510;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 7;
			base.item.useTime = 7;
			base.item.width = 34;
			base.item.height = 40;
			base.item.maxStack = 999;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 4, 0);
			base.item.shoot = ModContent.ProjectileType<ShadeKnifePro>();
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "VesselFrag", 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 100);
			modRecipe.AddRecipe();
		}
	}
}
