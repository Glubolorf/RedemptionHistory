using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles
{
	public class CrowbarPro : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crowbar");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 72;
			base.projectile.height = 72;
			base.projectile.friendly = true;
			base.projectile.penetrate = -1;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
			base.projectile.melee = true;
			base.projectile.scale = 2f;
		}

		public override void AI()
		{
			base.projectile.soundDelay--;
			if (base.projectile.soundDelay <= 0)
			{
				Main.PlaySound(2, (int)base.projectile.Center.X, (int)base.projectile.Center.Y, 1, 1f, 0f);
				base.projectile.soundDelay = 45;
			}
			Player player = Main.player[base.projectile.owner];
			if (Main.myPlayer == base.projectile.owner && (!player.channel || player.noItems || player.CCed))
			{
				base.projectile.Kill();
			}
			base.projectile.Center = player.MountedCenter;
			Projectile projectile = base.projectile;
			projectile.position.X = projectile.position.X + (float)(player.width / 2 * player.direction);
			base.projectile.spriteDirection = player.direction;
			base.projectile.rotation += 0.4f * (float)player.direction;
			if (base.projectile.rotation > 6.2831855f)
			{
				base.projectile.rotation -= 6.2831855f;
			}
			else if (base.projectile.rotation < 0f)
			{
				base.projectile.rotation += 6.2831855f;
			}
			player.heldProj = base.projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = base.projectile.rotation;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.type == base.mod.NPCType("HazmatSkeleton") || target.type == base.mod.NPCType("HazmatZombie") || target.type == base.mod.NPCType("InfectedCaveBat") || target.type == base.mod.NPCType("InfectedDemonEye") || target.type == base.mod.NPCType("InfectedDiggerHead") || target.type == base.mod.NPCType("InfectedDiggerBody") || target.type == base.mod.NPCType("InfectedDiggerTail") || target.type == base.mod.NPCType("InfectedGiantBat") || target.type == base.mod.NPCType("InfectedGiantWormBody") || target.type == base.mod.NPCType("InfectedGiantWormHead") || target.type == base.mod.NPCType("InfectedGiantWormTail") || target.type == base.mod.NPCType("InfectedGiantWormTail") || target.type == base.mod.NPCType("InfectedZombie") || target.type == base.mod.NPCType("SludgyBoi") || target.type == base.mod.NPCType("XenoChomper") || target.type == base.mod.NPCType("XenomiteEye") || target.type == base.mod.NPCType("XenomiteGargantuan") || target.type == base.mod.NPCType("XenomiteGolem") || target.type == base.mod.NPCType("XenonRoller") || target.type == base.mod.NPCType("RadiumDiggerBody") || target.type == base.mod.NPCType("RadiumDiggerTail") || target.type == base.mod.NPCType("RadiumDiggerHead") || target.type == base.mod.NPCType("InfectedEye") || target.type == base.mod.NPCType("Blisterface") || target.type == base.mod.NPCType("Blisterling") || target.type == base.mod.NPCType("Blisterling2") || target.type == base.mod.NPCType("InfectionHive") || target.type == base.mod.NPCType("IrradiatedBehemoth") || target.type == base.mod.NPCType("SludgyBlob") || target.type == base.mod.NPCType("SludgyBoi2") || target.type == base.mod.NPCType("Stage2Scientist") || target.type == base.mod.NPCType("Stage2ScientistBoss") || target.type == base.mod.NPCType("Stage3Scientist") || target.type == base.mod.NPCType("WalterInfected") || target.type == base.mod.NPCType("XenoChomper2") || target.type == base.mod.NPCType("XenomiteBeast") || target.type == base.mod.NPCType("SpikyRadioactiveSlime") || target.type == base.mod.NPCType("SneezyInfectedFlinx") || target.type == base.mod.NPCType("RadiumRampager") || target.type == base.mod.NPCType("RadiumDigger2Tail") || target.type == base.mod.NPCType("RadiumDigger2Head") || target.type == base.mod.NPCType("RadiumDigger2Body") || target.type == base.mod.NPCType("RadioactiveSlime") || target.type == base.mod.NPCType("NuclearSlime") || target.type == base.mod.NPCType("InfectedSwarmer") || target.type == base.mod.NPCType("InfectedSnowFlinx") || target.type == base.mod.NPCType("InfectedChicken") || target.type == base.mod.NPCType("GreenPigron") || target.type == base.mod.NPCType("DecayedGhoul") || target.type == base.mod.NPCType("BobTheBlob") || target.type == base.mod.NPCType("Injector") || target.type == base.mod.NPCType("BileBoomer") || target.type == base.mod.NPCType("IrradiatedSpear") || target.type == base.mod.NPCType("VirusJelly") || target.type == base.mod.NPCType("Superbug") || target.type == base.mod.NPCType("BloatedFaceMonster") || target.type == base.mod.NPCType("BloatedGoldfish") || target.type == base.mod.NPCType("IrradiatedWorldFeederTail") || target.type == base.mod.NPCType("IrradiatedWorldFeederHead") || target.type == base.mod.NPCType("IrradiatedWorldFeederBody") || target.type == base.mod.NPCType("NerveParasite") || target.type == base.mod.NPCType("RadioactiveSlimer") || target.type == base.mod.NPCType("Xenoling") || target.type == base.mod.NPCType("Superbug2") || target.type == base.mod.NPCType("Blisterface2") || target.type == base.mod.NPCType("IrradiatedBehemoth2") || target.type == base.mod.NPCType("PZ2BodyCover") || target.type == base.mod.NPCType("PZ2Fight") || target.type == base.mod.NPCType("Stage3Scientist2") || target.type == base.mod.NPCType("SeedGrowth") || target.type == base.mod.NPCType("SoI"))
			{
				damage *= 3;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[base.projectile.type];
			spriteBatch.Draw(texture, base.projectile.Center - Main.screenPosition, null, Color.White, base.projectile.rotation, new Vector2((float)(texture.Width / 2), (float)(texture.Height / 2)), 1f, (base.projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}
	}
}
