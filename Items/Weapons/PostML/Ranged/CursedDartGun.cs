using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class CursedDartGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Dart Pistol");
		}

		public override void SetDefaults()
		{
			base.item.damage = 450;
			base.item.ranged = true;
			base.item.width = 48;
			base.item.height = 30;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3.5f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = SoundID.Item98;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 10f;
			base.item.useAmmo = AmmoID.Dart;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().thornCrown)
			{
				flat += 50f;
			}
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CursedThorns", 15);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
