using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class DeathsClaw : ModItem
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				DeathsClaw.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.item.glowMask = DeathsClaw.customGlowMask;
			base.DisplayName.SetDefault("Death's Claw");
			base.Tooltip.SetDefault("'A burning scythe created in the underworld...'\nSlain enemies will explode into fireballs, the amount of fireballs depending on the size of the enemy\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 17;
			base.item.melee = true;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 11;
			base.item.useAnimation = 11;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.glowMask = DeathsClaw.customGlowMask;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 6, 0f, 0f, 0, default(Color), 1f);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (target.life <= 0)
			{
				int targetHitBox = target.width + target.height;
				if (targetHitBox < 350)
				{
					for (int i = 0; i < targetHitBox / 25; i++)
					{
						int proj = Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), 15, base.item.damage, 3f, Main.myPlayer, 0f, 0f);
						Main.projectile[proj].hostile = false;
						Main.projectile[proj].friendly = true;
					}
				}
				else
				{
					for (int j = 0; j < 15; j++)
					{
						int proj2 = Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), 15, base.item.damage, 3f, Main.myPlayer, 0f, 0f);
						Main.projectile[proj2].hostile = false;
						Main.projectile[proj2].friendly = true;
					}
				}
			}
			target.AddBuff(24, 600, false);
		}

		public static short customGlowMask;
	}
}
