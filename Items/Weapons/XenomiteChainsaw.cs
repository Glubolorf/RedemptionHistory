using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XenomiteChainsaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Chainsaw");
		}

		public override void SetDefaults()
		{
			base.item.damage = 29;
			base.item.melee = true;
			base.item.width = 66;
			base.item.height = 26;
			base.item.useTime = 9;
			base.item.useAnimation = 15;
			base.item.channel = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.axe = 35;
			base.item.useStyle = 5;
			base.item.knockBack = 3f;
			base.item.value = 550000;
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item23;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("XenomiteChainsawPro");
			base.item.shootSpeed = 40f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 30);
			modRecipe.AddIngredient(null, "StarliteBar", 5);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
		}
	}
}
