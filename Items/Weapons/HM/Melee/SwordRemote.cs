using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs.Cooldowns;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class SwordRemote : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Melee/" + base.GetType().Name + "_Glow");
				SwordRemote.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Cleaver Remote");
			base.Tooltip.SetDefault("'Size does matter'\nCalls upon the Vlitch Cleaver to unleash a devastating attack\nRight-Click to switch mode of attack\n10 second cooldown");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1;
			base.item.width = 28;
			base.item.height = 28;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.knockBack = 18f;
			base.item.useStyle = 4;
			base.item.crit = 4;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item44;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.noUseGraphic = false;
			base.item.shoot = ModContent.ProjectileType<RemoteCleaver>();
			base.item.glowMask = SwordRemote.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.UseSound = SoundID.Item1;
			}
			else
			{
				base.item.UseSound = SoundID.Item44;
			}
			return !player.HasBuff(ModContent.BuffType<SwordRemoteCooldown>());
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				this.attackMode++;
				if (this.attackMode >= 4)
				{
					this.attackMode = 0;
				}
				switch (this.attackMode)
				{
				case 0:
					CombatText.NewText(player.getRect(), Color.Red, "Swing Mode", true, false);
					break;
				case 1:
					CombatText.NewText(player.getRect(), Color.Red, "Stab Mode", true, false);
					break;
				case 2:
					CombatText.NewText(player.getRect(), Color.Red, "Sword Burst Mode", true, false);
					break;
				case 3:
					CombatText.NewText(player.getRect(), Color.Red, "Red Prism Mode", true, false);
					break;
				}
			}
			else
			{
				player.AddBuff(ModContent.BuffType<SwordRemoteCooldown>(), 600, true);
				switch (this.attackMode)
				{
				case 0:
					Projectile.NewProjectile(new Vector2((player.direction == 1) ? (player.Center.X + 50f) : (player.Center.X - 50f), player.Center.Y - 200f), Vector2.Zero, ModContent.ProjectileType<RemoteCleaver>(), 3000, 18f, Main.myPlayer, 0f, 0f);
					break;
				case 1:
					Projectile.NewProjectile(new Vector2((player.direction == 1) ? (player.Center.X + 50f) : (player.Center.X - 50f), player.Center.Y - 200f), Vector2.Zero, ModContent.ProjectileType<RemoteCleaver>(), 3000, 18f, Main.myPlayer, 1f, 0f);
					break;
				case 2:
					Projectile.NewProjectile(new Vector2((player.direction == 1) ? (player.Center.X + 50f) : (player.Center.X - 50f), player.Center.Y - 200f), Vector2.Zero, ModContent.ProjectileType<RemoteCleaver>(), 3000, 18f, Main.myPlayer, 2f, 0f);
					break;
				case 3:
					Projectile.NewProjectile(new Vector2((player.direction == 1) ? (player.Center.X + 50f) : (player.Center.X - 50f), player.Center.Y - 200f), Vector2.Zero, ModContent.ProjectileType<RemoteCleaver>(), 3000, 18f, Main.myPlayer, 3f, 0f);
					break;
				}
			}
			return false;
		}

		public static short customGlowMask;

		public int attackMode;
	}
}
