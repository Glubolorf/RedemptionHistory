using System;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Magic
{
	public class AncientArchmageStaff2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Archmage's Staff");
			base.Tooltip.SetDefault("Casts Cursed Bolts\nWhen hitting an enemy, cursed flames will rain down on it");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 260;
			base.item.magic = true;
			base.item.mana = 10;
			base.item.width = 58;
			base.item.height = 58;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.UseSound = SoundID.Item20;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<CursedBoltPro1>();
			base.item.shootSpeed = 15f;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 10);
			modRecipe.AddIngredient(172, 35);
			modRecipe.AddIngredient(null, "AncientNovicesStaff", 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
