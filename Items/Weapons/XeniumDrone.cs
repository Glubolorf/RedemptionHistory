using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XeniumDrone : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Deactivated Xenium Autoturret");
			base.Tooltip.SetDefault("Summons a friendly Xenium Autoturret to fight for you\nYou can craft this with different bullets\nTakes up 2 minion slots");
		}

		public override void SetDefaults()
		{
			base.item.damage = 47;
			base.item.summon = true;
			base.item.mana = 20;
			base.item.width = 30;
			base.item.height = 28;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("XeniumTurretMinion");
			base.item.shootSpeed = 8f;
			base.item.buffType = base.mod.BuffType("XeniumTurretBuff");
			base.item.buffTime = 3600;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}

		public override bool UseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 15);
			modRecipe.AddIngredient(null, "CarbonMyofibre", 20);
			modRecipe.AddIngredient(null, "AIChip", 1);
			modRecipe.AddIngredient(97, 500);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddRecipeGroup("Redemption:XeniumTurret", 1);
			modRecipe2.AddTile(null, "XenoTank1");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
