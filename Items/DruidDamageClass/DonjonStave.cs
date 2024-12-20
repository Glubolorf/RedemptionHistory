using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class DonjonStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Donjon Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nRight-clicking will summon a Skeletal Guardian [c/94c2ff:(Requires 200 Mana)]\n[c/71ee8d:-Guardian Info-]\n[c/a0db98:Type:] Guardian\n[c/98dbc3:Special Ability:] Swift-Swing/Ironbone Aura\n[c/98c1db:Effects:] Staves swing a lot faster, Defence Enhancement");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 23;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 54, 30);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
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
				base.item.buffType = base.mod.BuffType("NatureGuardian10Buff");
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).longerGuardians)
				{
					base.item.buffTime = 1200;
				}
				else
				{
					base.item.buffTime = 600;
				}
				base.item.shoot = base.mod.ProjectileType("NatureGuardian10");
				base.item.shootSpeed = 0f;
				return !player.HasBuff(base.mod.BuffType("GuardianCooldownDebuff")) && player.statManaMax2 >= 200;
			}
			base.item.mana = 0;
			base.item.buffType = 0;
			base.item.buffTime = 0;
			base.item.shoot = 0;
			base.item.shootSpeed = 0f;
			return true;
		}

		public override void UseStyle(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 2700, true);
					return;
				}
				player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 3600, true);
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(4) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 15, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
