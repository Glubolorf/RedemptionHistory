using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LastRedemption : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				LastRedemption.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = LastRedemption.customGlowMask;
			base.DisplayName.SetDefault("Last Redemption");
			base.Tooltip.SetDefault("[c/ffea9b:'The darkened Night will lose its Sight,]\n[c/ffea9b:Leaving the Light command the Fright,]\n[c/ffea9b:And the Might to the Holy Knight.']\nRight-clicking will cast down Holy Comets\nOnly the worthy may wield this\n[c/ffc300:Legendary]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 1000;
			base.item.melee = true;
			base.item.width = 82;
			base.item.height = 82;
			base.item.useTime = 18;
			base.item.useAnimation = 18;
			base.item.useStyle = 1;
			base.item.knockBack = 8.5f;
			base.item.value = Item.sellPrice(20, 0, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("LastRPro1");
			base.item.shootSpeed = 10f;
			base.item.glowMask = LastRedemption.customGlowMask;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.UseSound = SoundID.Item71;
				base.item.shoot = base.mod.ProjectileType("LastRPro3");
				base.item.shootSpeed = 18f;
			}
			else
			{
				base.item.UseSound = SoundID.Item122;
				base.item.shoot = base.mod.ProjectileType("LastRPro1");
				base.item.shootSpeed = 10f;
			}
			return NPC.downedMoonlord && RedeWorld.downedVlitch1 && RedeWorld.downedVlitch2 && RedeWorld.downedVlitch3;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				int myPlayer = Main.myPlayer;
				float shootSpeed = base.item.shootSpeed;
				int num = damage;
				float num2 = knockBack;
				num2 = player.GetWeaponKnockback(base.item, num2);
				player.itemTime = base.item.useTime;
				Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
				Utils.RotatedBy(Vector2.UnitX, (double)player.fullRotation, default(Vector2));
				Main.MouseWorld - vector;
				float num3 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
				float num4 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
				if (player.gravDir == -1f)
				{
					num4 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - vector.Y;
				}
				float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
				if ((float.IsNaN(num3) && float.IsNaN(num4)) || (num3 == 0f && num4 == 0f))
				{
					num3 = (float)player.direction;
					num4 = 0f;
					num5 = shootSpeed;
				}
				else
				{
					num5 = shootSpeed / num5;
				}
				num3 *= num5;
				num4 *= num5;
				int num6 = 4;
				for (int i = 0; i < num6; i++)
				{
					vector..ctor(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * -(float)player.direction + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
					vector.X = (vector.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
					vector.Y -= (float)(100 * i);
					num3 = (float)Main.mouseX + Main.screenPosition.X - vector.X;
					num4 = (float)Main.mouseY + Main.screenPosition.Y - vector.Y;
					if (num4 < 0f)
					{
						num4 *= -1f;
					}
					if (num4 < 20f)
					{
						num4 = 20f;
					}
					num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
					num5 = shootSpeed / num5;
					num3 *= num5;
					num4 *= num5;
					float num7 = num3 + (float)Main.rand.Next(-50, 51) * 0.02f;
					float num8 = num4 + (float)Main.rand.Next(-50, 51) * 0.02f;
					int num9 = Projectile.NewProjectile(vector.X, vector.Y, num7, num8, base.mod.ProjectileType("LastRPro3"), num, num2, myPlayer, 0f, (float)Main.rand.Next(10));
					Main.projectile[num9].tileCollide = false;
					Main.projectile[num9].ranged = false;
					Main.projectile[num9].magic = true;
					Main.projectile[num9].timeLeft = 200;
				}
				return false;
			}
			return true;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("RedemptiveEmbraceBuff"), 4, true);
			if (player.ownedProjectileCounts[base.mod.ProjectileType("LastRPro4")] == 0)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, base.mod.ProjectileType("LastRPro4"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Color transparent = Color.Transparent;
			if (base.item.modItem != null && base.item.modItem.mod == ModLoader.GetMod("Redemption"))
			{
				TooltipLine tooltipLine = Enumerable.First<TooltipLine>(tooltips, (TooltipLine v) => v.Name.Equals("ItemName"));
				tooltipLine.overrideColor = new Color?(new Color(255, 195, 0));
			}
		}

		public static short customGlowMask;
	}
}
