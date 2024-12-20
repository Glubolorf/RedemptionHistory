using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class LavaCubeStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Magma Staff");
			base.Tooltip.SetDefault("Summons a Lava Cube to fight for you.");
		}

		public override void SetDefaults()
		{
			base.item.damage = 22;
			base.item.summon = true;
			base.item.mana = 10;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 35;
			base.item.useAnimation = 35;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 0, 45, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = ModContent.ProjectileType<LavaCubeMinion>();
			base.item.shootSpeed = 10f;
			base.item.buffType = ModContent.BuffType<LavaCubeBuff>();
			base.item.buffTime = 3600;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1309, 1);
			modRecipe.AddIngredient(ModContent.ItemType<DragonLeadBar>(), 7);
			modRecipe.AddIngredient(175, 7);
			modRecipe.AddTile(77);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
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
	}
}
