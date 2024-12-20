using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class ElegantStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Elegant Marble Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Medusa won't like this...'\nWhile holding this, you are immune to Petrification\nRight-clicking will summon a Marble King Piece [c/94c2ff:(Requires 200 Mana)]\n[c/71ee8d:-Guardian Info-]\n[c/a0db98:Type:] Defensive/Healer\n[c/98dbc3:Special Ability:] Marble Aura\n[c/98c1db:Buffs:] +8 defence, increased mana regen, +20 max life & mana, Petrification immunity (Marble Aura)");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 21;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.useStyle = 1;
			base.item.crit = 16;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 40, 30);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.mana = 200;
				base.item.buffType = base.mod.BuffType("NatureGuardian13Buff");
				base.item.buffTime = 36000;
				base.item.shoot = base.mod.ProjectileType("NatureGuardian13");
				base.item.shootSpeed = 0f;
				return !player.HasBuff(base.mod.BuffType("NatureGuardian2Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardianBuff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian3Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian4Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian5Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian6Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian7Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian8Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian9Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian10Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian11Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian12Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian13Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian14Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian15Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian16Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian17Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian18Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian19Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian20Buff")) && !player.HasBuff(base.mod.BuffType("NatureGuardian21Buff"));
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = 0;
			base.item.shootSpeed = 0f;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				base.item.useTime = 20;
				base.item.useAnimation = 20;
			}
			else
			{
				base.item.useTime = 24;
				base.item.useAnimation = 24;
			}
			return true;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (target.type == 480)
			{
				damage *= 200;
			}
		}

		public override void HoldItem(Player player)
		{
			player.buffImmune[156] = true;
		}
	}
}
