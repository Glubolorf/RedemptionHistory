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
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Medusa won't like this...'\nWhile holding this, you are immune to Petrification\nRight-clicking will summon a Marble King Piece  [c/bee7c9:(30 Second Duration)]\n[c/71ee8d:-Guardian Info-]\n[c/a0db98:Type:] Other\n[c/98dbc3:Special Ability:] Marble Aura\n[c/98c1db:Effects:] Defence Enhancement+, Life & Mana Enhancement, Petrification Immunity");
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
			if (player.altFunctionUse == 2 && player.itemAnimation == 0)
			{
				base.item.mana = 1;
				base.item.buffType = base.mod.BuffType("NatureGuardian13Buff");
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().longerGuardians)
				{
					base.item.buffTime = 2400;
				}
				else
				{
					base.item.buffTime = 1800;
				}
				base.item.shoot = base.mod.ProjectileType("NatureGuardian13");
				return !player.HasBuff(base.mod.BuffType("GuardianCooldownDebuff"));
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = 0;
			return true;
		}

		public override void UseStyle(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
				{
					player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 2700, true);
					return;
				}
				player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 3600, true);
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().rapidStave)
				{
					return 1.35f;
				}
				return 1f;
			}
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
