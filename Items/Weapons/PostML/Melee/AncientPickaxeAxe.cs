using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class AncientPickaxeAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Earthbreaker Pickaxe Axe");
			base.Tooltip.SetDefault("Right-clicking will throw the pickaxe axe, dealing ranged damage");
		}

		public override void SetDefaults()
		{
			base.item.damage = 600;
			base.item.melee = true;
			base.item.width = 68;
			base.item.height = 64;
			base.item.useTime = 6;
			base.item.useAnimation = 10;
			base.item.pick = 310;
			base.item.axe = 35;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 55, 0, 0);
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = 0;
			base.item.shootSpeed = 18f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.noUseGraphic = true;
				base.item.noMelee = true;
				base.item.useTime = 6;
				base.item.pick = 0;
				base.item.axe = 0;
				base.item.shoot = ModContent.ProjectileType<AncientPickaxeAxePro>();
				base.item.ranged = true;
			}
			else
			{
				base.item.noUseGraphic = false;
				base.item.noMelee = false;
				base.item.useTime = 6;
				base.item.pick = 310;
				base.item.axe = 35;
				base.item.shoot = 0;
				base.item.melee = true;
			}
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse == 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
