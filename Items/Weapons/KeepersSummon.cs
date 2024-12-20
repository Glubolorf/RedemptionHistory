using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class KeepersSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Keeper's Staff");
			base.Tooltip.SetDefault("Summons a temporary Dark Soul to fight for you.\nAfter 5 seconds, it will explode into tiny homing Dark Souls\nIf all minion slots are used up, the staff will summon 2-4 tiny Dark Souls instead");
		}

		public override void SetDefaults()
		{
			base.item.damage = 15;
			base.item.summon = true;
			base.item.mana = 6;
			base.item.width = 54;
			base.item.height = 58;
			base.item.useTime = 36;
			base.item.useAnimation = 36;
			base.item.useStyle = 1;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item44;
			base.item.shoot = base.mod.ProjectileType("DarkSoulMinion");
			base.item.shootSpeed = 8f;
			base.item.buffType = base.mod.BuffType("DarkSoulBuff");
			base.item.buffTime = 300;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(16);
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
