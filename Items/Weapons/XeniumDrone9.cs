using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XeniumDrone9 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Autoturret (Meteor)");
			base.Tooltip.SetDefault("Summons a friendly Xenium Autoturret to fight for you\nTakes up 2 minion slots");
		}

		public override void SetDefaults()
		{
			base.item.damage = 49;
			base.item.summon = true;
			base.item.mana = 20;
			base.item.width = 30;
			base.item.height = 28;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4.5f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("XeniumTurretMinion9");
			base.item.shootSpeed = 8f;
			base.item.buffType = base.mod.BuffType("XeniumTurretBuff9");
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
			modRecipe.AddIngredient(null, "XeniumDrone", 1);
			modRecipe.AddIngredient(234, 500);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
