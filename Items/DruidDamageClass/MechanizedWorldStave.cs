using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MechanizedWorldStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mechanized World Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nSummon a Mechanical Abomination at your cursor point\nThe abomination emits a nano-field that increases player's attack and speed when near\nAlso deals damage to enemies\nCan only place one at a time");
			Item.staff[base.item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 27;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 5;
			base.item.mana = 100;
			base.item.crit = 4;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/WorldTree1");
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("WorldTree3");
			base.item.shootSpeed = 0f;
			base.item.buffType = base.mod.BuffType("WorldStaveCooldownDebuff");
			base.item.buffTime = 1000;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(base.mod.BuffType("WorldStaveCooldownDebuff"));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
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
