using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class XeniumCap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Cap");
			base.Tooltip.SetDefault("25% increased minion damage\n+1 max minions\n+50 max life");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 9, 50, 0);
			base.item.rare = 11;
			base.item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage *= 1.25f;
			player.maxMinions++;
			player.statLifeMax2 += 50;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<XeniumBody>() && legs.type == ModContent.ItemType<XeniumLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grants immunity to the Infection, Radioactive Fallout, and infected waters\n+3 max minions\nIncreased life regeneration the more minions you've summoned, capped at 10";
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			if (player.numMinions > 10)
			{
				player.lifeRegen += 10;
			}
			else
			{
				player.lifeRegen += player.numMinions;
			}
			player.maxMinions += 3;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<XenomiteDebuff2>()] = true;
			player.buffImmune[ModContent.BuffType<RadioactiveFalloutDebuff>()] = true;
			player.buffImmune[ModContent.BuffType<HeavyRadiationDebuff>()] = true;
			redePlayer.labWaterImmune = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 12);
			modRecipe.AddIngredient(null, "ArtificalMuscle", 2);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
