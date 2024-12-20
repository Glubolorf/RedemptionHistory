using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.WorldStave;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class StarboundWorldStave1 : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starbound World Stave");
			base.Tooltip.SetDefault("'Despite its origin, it feels familiar'\nSummon a tree formed out of outergalactic crystals\nThe tree emits an aura that increases player's jump height and defence when near\nCan only place one at a time");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 46;
			base.item.height = 44;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.value = Item.buyPrice(0, 4, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<WorldTree5>();
			base.item.shootSpeed = 0f;
			base.item.potion = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<WorldStaveCooldownDebuff>());
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(ModContent.BuffType<WorldStaveCooldownDebuff>(), 1200, true);
			position = Main.MouseWorld;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(117, 20);
			modRecipe.AddIngredient(502, 20);
			modRecipe.AddIngredient(null, "ForestCore", 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
