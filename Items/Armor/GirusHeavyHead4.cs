using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GirusHeavyHead4 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Overseer Mask");
			base.Tooltip.SetDefault("6% increased minion damage\nIncreases your max number of minions by 1");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage *= 1.06f;
			player.maxMinions++;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("GirusHeavyBody4") && legs.type == base.mod.ItemType("GirusHeavyLeggings4");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Enemies are more likely to target you, +1 max minions and 8% damage reduction.\nSummons a tiny Attack Hovercopter to shoot at nearby enemies";
			player.AddBuff(11, 2, true);
			player.maxMinions++;
			player.endurance += 0.08f;
			player.aggro += 10;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(base.mod.BuffType("CorruptedCopterBuff")) == -1)
				{
					player.AddBuff(base.mod.BuffType("CorruptedCopterBuff"), 3600, true);
				}
				if (player.ownedProjectileCounts[base.mod.ProjectileType("CorruptedCopter")] < 1)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, base.mod.ProjectileType("CorruptedCopter"), 20, 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomiteHead", 1);
			modRecipe.AddIngredient(null, "VlitchScale", 10);
			modRecipe.AddIngredient(null, "GirusChip", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
