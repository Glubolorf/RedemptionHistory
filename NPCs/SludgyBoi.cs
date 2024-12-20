using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SludgyBoi : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Irradiated Sludge");
			Main.npcFrameCount[base.npc.type] = 9;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 44;
			base.npc.damage = 80;
			base.npc.defense = 0;
			base.npc.lifeMax = 1350;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(0, 0, 8, 0);
			base.npc.knockBackResist = 0.7f;
			base.npc.aiStyle = 3;
			this.aiType = 489;
			this.animationType = 489;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<SludgyBoiBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
				this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
			}
			this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
			this.dust = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<SludgeSpoonDust>(), 0f, 0f, 0, default(Color), 1f);
		}

		private int dust;
	}
}
