using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class AncientPowerSurgeHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Power Surge Helmet");
			base.Tooltip.SetDefault("15% increased minion damage\n5% increased magic damage\n+4 minion capacity\n12% reduced mana usage\n+100 max mana");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 22;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions += 4;
			player.magicDamage *= 1.05f;
			player.minionDamage *= 1.15f;
			player.manaCost *= 0.88f;
			player.statManaMax2 += 100;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<AncientPowerSurgeBody>() && legs.type == ModContent.ItemType<AncientPowerSurgeLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Dealing damage has a 10% chance to fire out homing energy orbs at enemies\n8% increased magic and summon damage when you're not in a power surge\n6% increased magic critical strike chance.\nTaking damage builds up energy within the armour\nReaching a charge of 300 will unleash a Power Surge for 7 seconds\n--During Power Surge--\n25% increased magic and summon damage\nYour magic weapons don't consume mana\nYour immunity frames are extended";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).powerSurgeSet = true;
			if (!player.HasBuff(ModContent.BuffType<PowerSurgeBuff>()))
			{
				player.magicDamage += 0.08f;
				player.minionDamage += 0.08f;
			}
			player.magicCrit += 6;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 12);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			string text = "Charge: " + player.GetModPlayer<RedePlayer>().powerSurgeCharge + "/300";
			TooltipLine line = new TooltipLine(base.mod, "text1", text)
			{
				overrideColor = new Color?(Color.Goldenrod)
			};
			tooltips.Insert(2, line);
		}
	}
}
