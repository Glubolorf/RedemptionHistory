using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class NeedlePro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Needle");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 12;
			base.projectile.height = 12;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = true;
			base.projectile.penetrate = 3;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			if (base.projectile.ai[0] == 1f)
			{
				int num = (int)base.projectile.ai[1];
				if (num >= 0 && num < 200 && Main.npc[num].active)
				{
					if (Main.npc[num].behindTiles)
					{
						drawCacheProjsBehindNPCsAndTiles.Add(index);
						return;
					}
					drawCacheProjsBehindNPCs.Add(index);
					return;
				}
			}
			drawCacheProjsBehindProjectiles.Add(index);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = (height = 10);
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return new bool?(projHitbox.Intersects(targetHitbox));
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			Vector2 vector = base.projectile.position;
			Vector2 vector2 = Utils.ToRotationVector2(base.projectile.rotation - MathHelper.ToRadians(90f));
			vector += vector2 * 16f;
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustDirect(vector, base.projectile.width, base.projectile.height, 226, 0f, 0f, 0, default(Color), 1f);
				dust.position = (dust.position + base.projectile.Center) / 2f;
				dust.velocity += vector2 * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				vector -= vector2 * 8f;
			}
		}

		public bool isStickingToTarget
		{
			get
			{
				return base.projectile.ai[0] == 1f;
			}
			set
			{
				base.projectile.ai[0] = (value ? 1f : 0f);
			}
		}

		public float targetWhoAmI
		{
			get
			{
				return base.projectile.ai[1];
			}
			set
			{
				base.projectile.ai[1] = value;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			this.isStickingToTarget = true;
			this.targetWhoAmI = (float)target.whoAmI;
			base.projectile.velocity = (target.Center - base.projectile.Center) * 0.75f;
			base.projectile.netUpdate = true;
			target.AddBuff(base.mod.BuffType<NeedleDebuff>(), 900, false);
			base.projectile.damage = 0;
			int num = 20;
			Point[] array = new Point[num];
			int num2 = 0;
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (i != base.projectile.whoAmI && projectile.active && projectile.owner == Main.myPlayer && projectile.type == base.projectile.type && projectile.ai[0] == 1f && projectile.ai[1] == (float)target.whoAmI)
				{
					array[num2++] = new Point(i, projectile.timeLeft);
					if (num2 >= array.Length)
					{
						break;
					}
				}
			}
			if (num2 >= array.Length)
			{
				int num3 = 0;
				for (int j = 1; j < array.Length; j++)
				{
					if (array[j].Y < array[num3].Y)
					{
						num3 = j;
					}
				}
				Main.projectile[array[num3].X].Kill();
			}
			Player player = Main.player[base.projectile.owner];
			int crit2 = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref crit2);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref crit2);
			if (crit2 >= 100 || Main.rand.Next(1, 101) <= crit2)
			{
				crit = true;
			}
		}

		public override void AI()
		{
			if (!this.isStickingToTarget)
			{
				this.targetWhoAmI += 1f;
				if (this.targetWhoAmI >= 45f)
				{
					float num = 0.98f;
					float num2 = 0.35f;
					this.targetWhoAmI = 45f;
					base.projectile.velocity.X = base.projectile.velocity.X * num;
					base.projectile.velocity.Y = base.projectile.velocity.Y + num2;
				}
				base.projectile.rotation = Utils.ToRotation(base.projectile.velocity) + MathHelper.ToRadians(90f);
			}
			if (this.isStickingToTarget)
			{
				base.projectile.ignoreWater = true;
				base.projectile.tileCollide = false;
				int num3 = 15;
				bool flag = false;
				base.projectile.localAI[0] += 1f;
				bool flag2 = base.projectile.localAI[0] % 30f == 0f;
				int num4 = (int)this.targetWhoAmI;
				if (base.projectile.localAI[0] >= (float)(60 * num3) || num4 < 0 || num4 >= 200)
				{
					flag = true;
				}
				else if (Main.npc[num4].active && !Main.npc[num4].dontTakeDamage)
				{
					base.projectile.Center = Main.npc[num4].Center - base.projectile.velocity * 2f;
					base.projectile.gfxOffY = Main.npc[num4].gfxOffY;
					if (flag2)
					{
						Main.npc[num4].HitEffect(0, 1.0);
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					base.projectile.Kill();
				}
			}
		}

		private const float maxTicks = 45f;
	}
}
