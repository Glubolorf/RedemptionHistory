using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class GodArmorHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kamite Helmet");
			base.Tooltip.SetDefault("'Blessed with Oysus' power, this helm holds overwhelming energy...'\nGreatly increased life and mana regen\nGreatly increased movement speed\nIncreased night vision\nShows the location of treasure and ore\nShows the location of NPCs\nShows the location of traps and hazards\n50% increased melee speed\n25% increased throwing velocity\n50% mana cost reduction\n+9 max minions\n+500 max life");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 30;
			base.item.value = Item.sellPrice(1, 75, 0, 0);
			base.item.rare = 9;
			base.item.defense = 82;
		}

		public override void UpdateEquip(Player player)
		{
			player.lifeRegen += 100;
			player.manaRegen += 100;
			player.statLifeMax2 += 500;
			player.nightVision = true;
			player.findTreasure = true;
			player.meleeSpeed *= 1.5f;
			player.thrownVelocity *= 1.25f;
			player.manaCost *= 0.5f;
			player.maxMinions += 9;
			player.dangerSense = true;
			player.detectCreature = true;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
			player.armorEffectDrawOutlines = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("GodArmor") && legs.type == base.mod.ItemType("GodArmorLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "'Oysus has empowered you'\nEmit an aura of light\n+50 armour penetration\nEnemies are more likely to target you\nImmune to knockback\nImmune to most debuffs";
			player.AddBuff(11, 2, true);
			player.armorPenetration += 50;
			player.aggro += 5;
			player.noKnockback = true;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff")] = true;
			player.buffImmune[base.mod.BuffType("XenomiteDebuff2")] = true;
			player.buffImmune[base.mod.BuffType("RadioactiveFalloutDebuff")] = true;
			player.buffImmune[base.mod.BuffType("GloomShroomDebuff")] = true;
			player.buffImmune[30] = true;
			player.buffImmune[20] = true;
			player.buffImmune[24] = true;
			player.buffImmune[70] = true;
			player.buffImmune[22] = true;
			player.buffImmune[80] = true;
			player.buffImmune[35] = true;
			player.buffImmune[23] = true;
			player.buffImmune[31] = true;
			player.buffImmune[32] = true;
			player.buffImmune[197] = true;
			player.buffImmune[33] = true;
			player.buffImmune[36] = true;
			player.buffImmune[195] = true;
			player.buffImmune[196] = true;
			player.buffImmune[39] = true;
			player.buffImmune[69] = true;
			player.buffImmune[46] = true;
			player.buffImmune[47] = true;
			player.buffImmune[156] = true;
			player.buffImmune[164] = true;
			player.buffImmune[144] = true;
			player.buffImmune[44] = true;
			player.buffImmune[153] = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(208, 255, 255));
			}
		}
	}
}
