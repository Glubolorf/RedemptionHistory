using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public abstract class DruidStave : DruidDamageItem
	{
		public override bool CloneNewInstances
		{
			get
			{
				return true;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine tt = Enumerable.FirstOrDefault<TooltipLine>(tooltips, (TooltipLine x) => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null)
			{
				string[] array = tt.text.Split(new char[]
				{
					' '
				});
				string damageValue = Enumerable.First<string>(array);
				string damageWord = Enumerable.Last<string>(array);
				tt.text = damageValue + " druidic " + damageWord;
			}
			int tooltipLocation = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("ItemName"));
			int tooltipLocation2 = tooltips.FindIndex((TooltipLine TooltipLine) => TooltipLine.Name.Equals("Tooltip0"));
			if (tooltipLocation != -1 && !RedeConfigClient.Instance.NoDruidClassTag)
			{
				tooltips.Insert(tooltipLocation + 1, new TooltipLine(base.mod, "IsDruid", "[c/91dc16:---Druid Class---]"));
			}
			if (tooltipLocation2 != -1 && this.guardianBuffID != -1 && this.guardianProjectileID != -1)
			{
				tooltips.Insert(tooltipLocation + 2, new TooltipLine(base.mod, "GuardianIntro", string.Concat(new object[]
				{
					"Right-clicking will summon a ",
					this.guardianName,
					"  [c/bee7c9:(",
					Math.Round((double)((float)this.guardianTime * (Main.LocalPlayer.GetModPlayer<RedePlayer>().longerGuardians ? 1.5f : 1f) * base.item.GetGlobalItem<RedeItem>().prefixLifetimeModifier / 60f), 1),
					" Second Duration)]"
				})));
				if (!RedeConfigClient.Instance.NoGuardianInfo)
				{
					tooltips.Insert(tooltipLocation2 + 2, new TooltipLine(base.mod, "GuardianInfo", "[c/71ee8d: -Guardian Info -]"));
					tooltips.Insert(tooltipLocation2 + 3, new TooltipLine(base.mod, "GuardianType", "[c/a0db98:Type:] " + this.guardianType));
					tooltips.Insert(tooltipLocation2 + 4, new TooltipLine(base.mod, "SpecialAbilities", "[c/98dbc3:Special Ability:] " + this.guardianAbility));
					tooltips.Insert(tooltipLocation2 + 5, new TooltipLine(base.mod, "Effects", "[c/98c1db:Effects:] " + this.guardianEffects));
				}
			}
		}

		public override void SecondarySetDefaults()
		{
			base.item.useTurn = false;
			base.item.noMelee = true;
			base.item.useStyle = 6;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return this.guardianBuffID != -1 && this.guardianProjectileID != -1;
		}

		public override bool CanUseItem(Player player)
		{
			if (this.defaultShoot != -1)
			{
				if (this.guardianBuffID != -1 && this.guardianProjectileID != -1 && player.altFunctionUse == 2)
				{
					base.item.mana = 1;
					base.item.buffType = this.guardianBuffID;
					base.item.buffTime = (int)((float)this.guardianTime * (Main.LocalPlayer.GetModPlayer<RedePlayer>().longerGuardians ? 1.5f : 1f) * base.item.GetGlobalItem<RedeItem>().prefixLifetimeModifier);
					base.item.shoot = this.guardianProjectileID;
					if (!player.HasBuff(base.mod.BuffType("GuardianCooldownDebuff")))
					{
						if (Main.LocalPlayer.GetModPlayer<RedePlayer>().guardianCooldownReduce)
						{
							player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 2700, true);
						}
						else
						{
							player.AddBuff(base.mod.BuffType("GuardianCooldownDebuff"), 3600, true);
						}
						return true;
					}
					return false;
				}
				else
				{
					base.item.mana = 0;
					base.item.buffType = 0;
					base.item.buffTime = 0;
					base.item.shoot = this.defaultShoot;
				}
			}
			return true;
		}

		public override void UseStyle(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				player.itemRotation = 0.7853982f * (float)player.direction;
				player.bodyFrame.Y = player.bodyFrame.Height * 5;
			}
			else
			{
				player.bodyFrame.Y = player.bodyFrame.Height * 3;
				player.itemRotation = -1f * this.staveHoldRotation * (float)player.direction;
			}
			Vector2 vector24 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
			if (player.direction != 1)
			{
				vector24.X = (float)player.bodyFrame.Width - vector24.X;
			}
			if (player.gravDir != 1f)
			{
				vector24.Y = (float)player.bodyFrame.Height - vector24.Y;
			}
			vector24 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
			player.itemLocation = player.position + vector24;
			if (player.altFunctionUse == 2)
			{
				player.itemLocation += Vector2.UnitY * -4f;
				return;
			}
			float trueRotation = 1.5707964f - player.itemRotation + 3.1415927f;
			player.itemLocation += new Vector2((float)Math.Cos((double)trueRotation), (float)Math.Sin((double)trueRotation)) * this.staveHoldOffset.Y;
			player.itemLocation += new Vector2((float)Math.Cos((double)(trueRotation + 1.5707964f)), (float)Math.Sin((double)(trueRotation + 1.5707964f))) * this.staveHoldOffset.X * (float)player.direction;
		}

		public override float MeleeSpeedMultiplier(Player player)
		{
			return player.GetModPlayer<RedePlayer>().staveSpeed;
		}

		public override float UseTimeMultiplier(Player player)
		{
			return player.GetModPlayer<RedePlayer>().staveSpeed;
		}

		protected virtual void ModifyVelocity(ref float speedX, ref float speedY)
		{
		}

		protected virtual bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float trueRotation = 1.5707964f - player.itemRotation + 3.1415927f;
			position = player.itemLocation + new Vector2((float)Math.Cos((double)trueRotation), (float)Math.Sin((double)trueRotation)) * this.staveLength;
			float scalarSpeed = new Vector2(speedX, speedY).Length();
			Vector2 speed = Utils.SafeNormalize(Main.MouseWorld - position, -Vector2.UnitY) * scalarSpeed;
			speedX = speed.X;
			speedY = speed.Y;
			this.ModifyVelocity(ref speedX, ref speedY);
			if (this.singleShotStave)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveStreamShot && Main.rand.Next(5) == 0)
				{
					Projectile.NewProjectile(position.X, position.Y, speedX * 1.25f, speedY * 1.25f, type, damage, knockBack, player.whoAmI, 0f, 0f);
					Projectile.NewProjectile(position.X, position.Y, speedX * 0.75f, speedY * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveTripleShot)
				{
					float numberProjectiles = 3f;
					float rotation = MathHelper.ToRadians(15f);
					position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
					int i = 0;
					while ((float)i < numberProjectiles)
					{
						Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.8f;
						Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
						i++;
					}
					return false;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveScatterShot && Main.rand.Next(5) == 0)
				{
					int numberProjectiles2 = 2 + Main.rand.Next(2);
					for (int j = 0; j < numberProjectiles2; j++)
					{
						Vector2 perturbedSpeed2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
						float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
						perturbedSpeed2 *= scale;
						Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					}
					return false;
				}
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveQuadShot)
				{
					float numberProjectiles3 = 5f;
					float rotation2 = MathHelper.ToRadians(15f);
					position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
					int k = 0;
					while ((float)k < numberProjectiles3)
					{
						Vector2 perturbedSpeed3 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation2, rotation2, (float)k / (numberProjectiles3 - 1f)), default(Vector2)) * 0.8f;
						Projectile.NewProjectile(position.X, position.Y, perturbedSpeed3.X, perturbedSpeed3.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
						k++;
					}
					return false;
				}
			}
			return this.SpecialShootPattern(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
		}

		protected int defaultShoot = -1;

		protected int guardianTime = 1500;

		protected int guardianBuffID = -1;

		protected int guardianProjectileID = -1;

		protected bool singleShotStave = true;

		protected Vector2 staveHoldOffset = Vector2.Zero;

		protected float staveHoldRotation = 0.3926991f;

		protected float staveLength;

		protected string guardianName = "Guardian";

		protected string guardianType = "Basic";

		protected string guardianAbility = "None";

		protected string guardianEffects = "None";
	}
}
