using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.WorldStave;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MechanizedWorldStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mechanized World Stave");
			base.Tooltip.SetDefault("Summon a Mechanical Abomination at your cursor point\nThe abomination emits a nano-field that increases player's attack and speed when near\nCan only place one at a time");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<WorldTree3>();
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
			modRecipe.AddIngredient(1225, 12);
			modRecipe.AddIngredient(549, 4);
			modRecipe.AddIngredient(548, 4);
			modRecipe.AddIngredient(547, 4);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
