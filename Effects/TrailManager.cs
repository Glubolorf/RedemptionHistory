using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Redemption.NPCs.Bosses.Neb;
using Redemption.NPCs.Bosses.Neb.Phase2;
using Redemption.NPCs.Bosses.TheKeeper;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Melee;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Effects
{
	public class TrailManager
	{
		public TrailManager(Mod mod)
		{
			this._trails = new List<Trail>();
			this._effect = mod.GetEffect("Effects/trailShaders");
			this._basicEffect = new BasicEffect(Main.graphics.GraphicsDevice)
			{
				VertexColorEnabled = true
			};
		}

		public void DoTrailCreation(Projectile projectile)
		{
			Mod mod = Redemption.Inst;
			if (projectile.type == ModContent.ProjectileType<BleedingSunPro1>())
			{
				this.CreateTrail(projectile, new GradientTrail(new Color(255, 230, 230), new Color(255, 98, 87)), new RoundCap(), new DefaultTrailPosition(), 50f, 250f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_2"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<CursedMoonPro1>())
			{
				this.CreateTrail(projectile, new GradientTrail(new Color(41, 34, 56), new Color(53, 155, 106)), new RoundCap(), new DefaultTrailPosition(), 50f, 250f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_2"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<CurvingStar2>() || projectile.type == ModContent.ProjectileType<StarFallPro2>())
			{
				this.CreateTrail(projectile, new RainbowTrail(5f, 0.002f, 1f, 0.75f), new RoundCap(), new DefaultTrailPosition(), 150f, 130f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_4"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<CurvingStar>() || projectile.type == ModContent.ProjectileType<StarFallPro>())
			{
				this.CreateTrail(projectile, new RainbowTrail(5f, 0.002f, 1f, 0.75f), new RoundCap(), new DefaultTrailPosition(), 50f, 100f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_4"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<DualcastBall>())
			{
				this.CreateTrail(projectile, new GradientTrail(new Color(255, 255, 255), new Color(241, 215, 108)), new RoundCap(), new ZigZagTrailPosition(6f), 80f, 250f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_4"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<UkkoThunderwave>() || projectile.type == ModContent.ProjectileType<UkkosLightning2>())
			{
				this.CreateTrail(projectile, new StandardColorTrail(Color.White), new RoundCap(), new ZigZagTrailPosition(6f), 80f, 250f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_4"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<WhiteNeedlePro>())
			{
				this.CreateTrail(projectile, new StandardColorTrail(Color.White), new RoundCap(), new DefaultTrailPosition(), 20f, 60f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_4"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<KeeperDreadCoil>())
			{
				this.CreateTrail(projectile, new GradientTrail(new Color(136, 123, 255), new Color(79, 15, 255)), new NoCap(), new DefaultTrailPosition(), 200f, 250f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_1"), 0.01f, 1f, 1f, 0f));
			}
			if (projectile.type == ModContent.ProjectileType<KeeperSoulCharge>())
			{
				this.CreateTrail(projectile, new StandardColorTrail(Color.GhostWhite), new RoundCap(), new ArrowGlowPosition(), 32f, 250f, null);
			}
			if (projectile.type == ModContent.ProjectileType<DancingLights>())
			{
				this.CreateTrail(projectile, new GradientTrail(new Color(255, 243, 162), new Color(226, 143, 70)), new RoundCap(), new DefaultTrailPosition(), 20f, 60f, new ImageShader(mod.GetTexture("ExtraTextures/Trails/Trail_4"), 0.01f, 1f, 1f, 0f));
			}
		}

		public void TryTrailKill(Projectile projectile)
		{
			Redemption inst = Redemption.Inst;
			if (projectile.type == ModContent.ProjectileType<BleedingSunPro1>() || projectile.type == ModContent.ProjectileType<CursedMoonPro1>() || projectile.type == ModContent.ProjectileType<CurvingStar2>() || projectile.type == ModContent.ProjectileType<CurvingStar>() || projectile.type == ModContent.ProjectileType<StarFallPro>() || projectile.type == ModContent.ProjectileType<StarFallPro2>() || projectile.type == ModContent.ProjectileType<DualcastBall>() || projectile.type == ModContent.ProjectileType<UkkoThunderwave>() || projectile.type == ModContent.ProjectileType<UkkosLightning2>() || projectile.type == ModContent.ProjectileType<WhiteNeedlePro>() || projectile.type == ModContent.ProjectileType<DancingLights>() || projectile.type == ModContent.ProjectileType<KeeperDreadCoil>() || projectile.type == ModContent.ProjectileType<KeeperSoulCharge>())
			{
				Redemption.TrailManager.TryEndTrail(projectile, Math.Max(15f, projectile.velocity.Length() * 3f));
			}
		}

		public void CreateTrail(Projectile projectile, ITrailColor trailType, ITrailCap trailCap, ITrailPosition trailPosition, float widthAtFront, float maxLength, ITrailShader shader = null)
		{
			Trail newTrail = new Trail(projectile, trailType, trailCap, trailPosition, shader ?? new DefaultShader(), widthAtFront, maxLength);
			newTrail.Update();
			this._trails.Add(newTrail);
		}

		public void UpdateTrails()
		{
			for (int i = 0; i < this._trails.Count; i++)
			{
				Trail trail = this._trails[i];
				trail.Update();
				if (trail.Dead)
				{
					this._trails.RemoveAt(i);
					i--;
				}
			}
		}

		public void DrawTrails(SpriteBatch spriteBatch)
		{
			foreach (Trail trail in this._trails)
			{
				trail.Draw(this._effect, this._basicEffect, spriteBatch.GraphicsDevice);
			}
		}

		public void TryEndTrail(Projectile projectile, float dissolveSpeed)
		{
			for (int i = 0; i < this._trails.Count; i++)
			{
				Trail trail = this._trails[i];
				if (trail.MyProjectile.whoAmI == projectile.whoAmI)
				{
					trail.StartDissolve(dissolveSpeed);
					return;
				}
			}
		}

		private List<Trail> _trails = new List<Trail>();

		private readonly Effect _effect;

		private readonly BasicEffect _basicEffect;
	}
}
