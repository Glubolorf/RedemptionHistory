using System;
using Microsoft.Xna.Framework;
using Redemption.Dusts;
using Redemption.NPCs.Lab;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Lab
{
	public class HiveSpawnerTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 1;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorBottom = new AnchorData(11, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile((int)base.Type);
			this.dustType = ModContent.DustType<SludgeSpoonDust>();
			this.minPick = 300;
			this.mineResist = 10f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(40, 120, 40), null);
			this.animationFrameHeight = 18;
		}

		public override void NearbyEffects(int i, int j, bool closer)
		{
			Player player = Main.LocalPlayer;
			float dist = Vector2.Distance(player.Center / 16f, new Vector2((float)i + 0.5f, (float)j + 0.5f));
			if (dist <= 10f && dist > 1f && Main.rand.Next(100) == 0 && NPC.CountNPCS(ModContent.NPCType<InfectionHive>()) == 0)
			{
				if (Main.netMode != 1)
				{
					int index = NPC.NewNPC(i * 16, j * 16, ModContent.NPCType<InfectionHive>(), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[index].netUpdate2 = true;
					return;
				}
				if (Main.netMode != 0)
				{
					Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 2, new object[]
					{
						(byte)player.whoAmI,
						ModContent.NPCType<InfectionHive>(),
						i * 16,
						j * 16
					}).Send(-1, -1);
				}
			}
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frameCounter++;
			if (frameCounter > 30)
			{
				frameCounter = 0;
				frame++;
				if (frame > 1)
				{
					frame = 0;
				}
			}
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
