using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Druid.Seedbag
{
	public class Plant7 : DruidPlant
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Icar's Flower");
			Main.projFrames[base.projectile.type] = 6;
			ProjectileID.Sets.DontAttachHideToAlpha[base.projectile.type] = true;
		}

		public override void SetSafeDefaults()
		{
			base.projectile.width = 60;
			base.projectile.height = 44;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.tileCollide = true;
			base.projectile.ignoreWater = true;
			base.projectile.timeLeft = 200;
			base.projectile.hide = true;
		}

		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}

		protected override void PlantAI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				if (this.IsOnNativeTerrain)
				{
					Projectile projectile2 = base.projectile;
					num = projectile2.frame + 1;
					projectile2.frame = num;
					if (num >= 6)
					{
						base.projectile.frame = 5;
					}
				}
				else
				{
					Projectile projectile3 = base.projectile;
					num = projectile3.frame + 1;
					projectile3.frame = num;
					if (num >= 5)
					{
						base.projectile.frame = 4;
					}
				}
			}
			Point point = Utils.ToTileCoordinates(base.projectile.position);
			if (Main.tile[point.X, point.Y - 1].type != 0)
			{
				base.projectile.ai[0] = 0f;
			}
			else
			{
				base.projectile.ai[0] = 1f;
			}
			if (base.projectile.ai[0] == 1f)
			{
				this.cloudCounter++;
				if (this.cloudCounter > 15)
				{
					this.cloudFrame++;
					this.cloudCounter = 0;
				}
				if (this.cloudFrame >= 2)
				{
					this.cloudFrame = 0;
				}
				Projectile projectile4 = base.projectile;
				projectile4.velocity.Y = projectile4.velocity.Y * 0f;
			}
			else
			{
				Projectile projectile5 = base.projectile;
				projectile5.velocity.Y = projectile5.velocity.Y - 2f;
			}
			base.projectile.localAI[0] += 1f;
			if (RedeHelper.ClosestNPC(ref this.target, 900f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 15f == 0f && base.projectile.frame >= 4)
			{
				Projectile.NewProjectile(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<IcarsFlowerPetal>(), base.projectile.damage, base.projectile.knockBack, player.whoAmI, 0f, 0f);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			Texture2D cloudAni = base.mod.GetTexture("Projectiles/Druid/Seedbag/PlantCloud");
			int spriteDirection = base.projectile.spriteDirection;
			Vector2 drawCenter2 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y);
			int num215 = texture.Height / 6;
			int y7 = num215 * base.projectile.frame;
			Main.spriteBatch.Draw(texture, drawCenter2 - Main.screenPosition, new Rectangle?(new Rectangle(0, y7, texture.Width, num215)), drawColor, base.projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num215 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			if (base.projectile.ai[0] == 1f)
			{
				Vector2 drawCenter3 = new Vector2(base.projectile.Center.X, base.projectile.Center.Y - 32f);
				int num216 = cloudAni.Height / 2;
				int y8 = num216 * this.cloudFrame;
				Main.spriteBatch.Draw(cloudAni, drawCenter3 - Main.screenPosition, new Rectangle?(new Rectangle(0, y8, cloudAni.Width, num216)), drawColor, base.projectile.rotation, new Vector2((float)cloudAni.Width / 2f, (float)num216 / 2f), base.projectile.scale, (base.projectile.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CheckNativeTerrain()
		{
			Point rootLocation = Utils.ToTileCoordinates(base.projectile.Top);
			bool yes = false;
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					foreach (int type in base.projectile.GetGlobalProjectile<DruidProjectile>().NativeTerrainIDs)
					{
						if ((int)Main.tile[rootLocation.X + x, rootLocation.Y + y].type == type)
						{
							yes = true;
							break;
						}
					}
				}
			}
			return yes;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 6; i++)
			{
				Dust.NewDust(base.projectile.position + base.projectile.velocity, base.projectile.width, base.projectile.height, 2, base.projectile.velocity.X * 0.5f, base.projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		private NPC target;

		public int cloudFrame;

		public int cloudCounter;
	}
}
