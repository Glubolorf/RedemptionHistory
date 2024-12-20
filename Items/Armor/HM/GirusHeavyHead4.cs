using System;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
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
			base.Tooltip.SetDefault("6% increased minion damage\nIncreases your max number of minions by 2");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 40, 0, 0);
			base.item.rare = 10;
			base.item.defense = 10;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage *= 1.06f;
			player.maxMinions += 2;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<GirusHeavyBody>() && legs.type == ModContent.ItemType<GirusHeavyLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Enemies are more likely to target you, +3 max minions and 8% damage reduction.\nSummons a tiny Attack Hovercopter to shoot at nearby enemies";
			player.AddBuff(11, 2, true);
			player.maxMinions += 2;
			player.endurance += 0.08f;
			player.aggro += 10;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).corruptedCopter = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(ModContent.BuffType<CorruptedCopterBuff>()) == -1)
				{
					player.AddBuff(ModContent.BuffType<CorruptedCopterBuff>(), 3600, true);
				}
				if (player.ownedProjectileCounts[ModContent.ProjectileType<CorruptedCopter>()] < 1)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<CorruptedCopter>(), 20, 2f, Main.myPlayer, 0f, 0f);
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
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
