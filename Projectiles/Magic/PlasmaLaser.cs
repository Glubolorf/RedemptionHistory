using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Magic
{
	public class PlasmaLaser : ModProjectile
	{
		public float AITimer
		{
			get
			{
				return base.projectile.localAI[0];
			}
			set
			{
				base.projectile.localAI[0] = value;
			}
		}

		public float Frame
		{
			get
			{
				return base.projectile.localAI[1];
			}
			set
			{
				base.projectile.localAI[1] = value;
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plasma Beam");
		}

		public override void SetDefaults()
		{
			base.projectile.width = this.LaserWidth;
			base.projectile.height = this.LaserWidth;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.timeLeft = 3600;
			base.projectile.usesIDStaticNPCImmunity = true;
			base.projectile.idStaticNPCHitCooldown = 3;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			Projectile gun = Main.projectile[(int)base.projectile.ai[0]];
			Vector2 ShootPos = player.Center + RedeHelper.PolarVector(18f, Utils.ToRotation(Main.MouseWorld - player.Center));
			base.projectile.Center = ShootPos;
			base.projectile.rotation = Utils.ToRotation(Main.MouseWorld - player.Center);
			if (!gun.active)
			{
				base.projectile.Kill();
			}
			if (this.AITimer == 0f)
			{
				this.LaserScale = 0.1f;
				if (!Main.dedServ)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BallFire").WithVolume(0.9f).WithPitchVariance(0f), (int)base.projectile.position.X, (int)base.projectile.position.Y);
				}
			}
			if (this.AITimer <= 10f)
			{
				this.LaserScale += 0.09f;
			}
			else if (!player.channel || base.projectile.timeLeft < 10 || !gun.active)
			{
				if (base.projectile.timeLeft > 10)
				{
					base.projectile.timeLeft = 10;
				}
				this.LaserScale -= 0.1f;
			}
			if (this.StopsOnTiles)
			{
				this.EndpointTileCollision();
			}
			else
			{
				this.LaserLength = (float)this.MaxLaserLength;
			}
			base.projectile.frameCounter++;
			float num;
			if (base.projectile.frameCounter >= this.LaserFrameDelay)
			{
				base.projectile.frameCounter = 0;
				num = this.Frame;
				this.Frame = num + 1f;
				if (this.Frame >= (float)this.maxLaserFrames)
				{
					this.Frame = 0f;
				}
			}
			num = this.AITimer + 1f;
			this.AITimer = num;
		}

		private void EndpointTileCollision()
		{
			this.LaserLength = 10f;
			while (this.LaserLength < (float)this.MaxLaserLength)
			{
				Vector2 start = base.projectile.Center + Utils.RotatedBy(Vector2.UnitX, (double)base.projectile.rotation, default(Vector2)) * this.LaserLength;
				if (!Collision.CanHitLine(base.projectile.Center, 1, 1, start, 1, 1))
				{
					this.LaserLength -= (float)this.LaserSegmentLength;
					return;
				}
				this.LaserLength += (float)this.LaserSegmentLength;
			}
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = 2;
			Vector2 unit = Utils.RotatedBy(new Vector2(1.5f, 0f), (double)base.projectile.rotation, default(Vector2));
			Utils.PlotTileLine(base.projectile.Center, base.projectile.Center + unit * this.LaserLength, (float)(base.projectile.width + 16) * base.projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}

		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 1)
		{
			float r = Utils.ToRotation(unit) + rotation;
			for (float i = (float)transDist; i <= maxDist * (1f / this.LaserScale); i += (float)this.LaserSegmentLength)
			{
				Color c = Color.White;
				Vector2 origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition + new Vector2(0f, base.projectile.gfxOffY), new Rectangle?(new Rectangle((int)((float)this.LaserWidth * this.Frame), this.LaserEndSegmentLength, this.LaserWidth, this.LaserSegmentLength)), c, r, new Vector2((float)(this.LaserWidth / 2), (float)(this.LaserSegmentLength / 2)), scale, SpriteEffects.None, 0f);
			}
			spriteBatch.Draw(texture, start + unit * (float)(transDist - this.LaserEndSegmentLength) - Main.screenPosition + new Vector2(0f, base.projectile.gfxOffY), new Rectangle?(new Rectangle((int)((float)this.LaserWidth * this.Frame), 0, this.LaserWidth, this.LaserEndSegmentLength)), Color.White, r, new Vector2((float)(this.LaserWidth / 2), (float)(this.LaserSegmentLength / 2)), scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, start + maxDist * (1f / scale) * unit - Main.screenPosition + new Vector2(0f, base.projectile.gfxOffY), new Rectangle?(new Rectangle((int)((float)this.LaserWidth * this.Frame), this.LaserSegmentLength + this.LaserEndSegmentLength, this.LaserWidth, this.LaserEndSegmentLength)), Color.White, r, new Vector2((float)(this.LaserWidth / 2), (float)(this.LaserSegmentLength / 2)), scale, SpriteEffects.None, 0f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			this.DrawLaser(spriteBatch, Main.projectileTexture[base.projectile.type], base.projectile.Center + Utils.RotatedBy(new Vector2((float)base.projectile.width, 0f), (double)base.projectile.rotation, default(Vector2)) * this.LaserScale, Utils.RotatedBy(new Vector2(1f, 0f), (double)base.projectile.rotation, default(Vector2)) * this.LaserScale, -1.57f, this.LaserScale, this.LaserLength, Color.White, 10);
			return false;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Player player = Main.player[base.projectile.owner];
			Vector2 unit = Utils.RotatedBy(new Vector2(1.5f, 0f), (double)base.projectile.rotation, default(Vector2));
			float point = 0f;
			if (Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), base.projectile.Center, base.projectile.Center + unit * this.LaserLength, 48f * this.LaserScale, ref point))
			{
				return new bool?(true);
			}
			return new bool?(false);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(this.LaserLength);
			writer.Write(this.LaserScale);
			writer.Write(this.LaserSegmentLength);
			writer.Write(this.LaserEndSegmentLength);
			writer.Write(this.LaserWidth);
			writer.Write(this.MaxLaserLength);
			writer.Write(this.StopsOnTiles);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			this.LaserLength = reader.ReadSingle();
			this.LaserScale = reader.ReadSingle();
			this.LaserSegmentLength = reader.ReadInt32();
			this.LaserEndSegmentLength = reader.ReadInt32();
			this.LaserWidth = reader.ReadInt32();
			this.MaxLaserLength = reader.ReadInt32();
			this.StopsOnTiles = reader.ReadBoolean();
		}

		public float LaserLength;

		public float LaserScale;

		public int LaserSegmentLength = 30;

		public int LaserWidth = 26;

		public int LaserEndSegmentLength = 22;

		private const float FirstSegmentDrawDist = 10f;

		public int MaxLaserLength = 2000;

		public int maxLaserFrames = 2;

		public int LaserFrameDelay = 5;

		public bool StopsOnTiles = true;
	}
}
