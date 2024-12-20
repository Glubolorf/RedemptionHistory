using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Summon
{
	public class XeniumDrone : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Autoturret");
			base.Tooltip.SetDefault("Summons a friendly Xenium Autoturret to fight for you\nFires bullets from your inventory\n80% chance not to consume ammo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 75;
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
			base.item.shoot = ModContent.ProjectileType<XeniumTurretMinion>();
			base.item.shootSpeed = 8f;
			base.item.buffType = ModContent.BuffType<XeniumTurretBuff>();
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
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
