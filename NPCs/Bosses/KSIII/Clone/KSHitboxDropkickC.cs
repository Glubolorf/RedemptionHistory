using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII.Clone
{
	public class KSHitboxDropkickC : KSHitbox
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 58;
			base.projectile.height = 118;
			base.projectile.timeLeft = 90;
		}

		public override void AI()
		{
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body_Clone>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.Center = npc2.Center;
			base.projectile.velocity = Vector2.Zero;
			if (npc2.ai[1] != 3f || npc2.ai[0] != 5f)
			{
				base.projectile.Kill();
			}
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<StaticStunDebuff>(), 120, true);
		}
	}
}
