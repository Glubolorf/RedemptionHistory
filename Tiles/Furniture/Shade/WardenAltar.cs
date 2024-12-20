using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Redemption.Tiles.Furniture.Shade
{
	public class WardenAltar : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[(int)base.Type] = true;
			Main.tileLavaDeath[(int)base.Type] = false;
			Main.tileNoAttach[(int)base.Type] = true;
			Main.tileTable[(int)base.Type] = false;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				16,
				16
			};
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorWall = true;
			TileObjectData.addTile((int)base.Type);
			this.dustType = 7;
			this.minPick = 500;
			this.mineResist = 30f;
			this.disableSmartCursor = true;
			base.CreateMapEntryName(null);
			base.AddMapEntry(new Color(40, 40, 60), null);
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			return false;
		}

		public override bool NewRightClick(int i, int j)
		{
			Player p = Main.player[Main.myPlayer];
			Item[] inventory = p.inventory;
			for (int k = 0; k < inventory.Length; k++)
			{
				if (inventory[k].type == ModContent.ItemType<DaggerOfOathkeeper>() && !NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()))
				{
					if (Main.netMode != 1)
					{
						int index = NPC.NewNPC((int)(((float)i + 1.5f) * 16f), j * 16, ModContent.NPCType<WardenIdle>(), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[index].netUpdate2 = true;
					}
					else
					{
						if (Main.netMode == 0)
						{
							return false;
						}
						Redemption.WriteToPacket(Redemption.Inst.GetPacket(256), 0, new object[]
						{
							(byte)p.whoAmI,
							ModContent.NPCType<WardenIdle>(),
							"The Warden Has Been Summoned!",
							(int)(((float)i + 1.5f) * 16f),
							j * 16
						}).Send(-1, -1);
					}
				}
			}
			return true;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModContent.ItemType<DaggerOfOathkeeper>();
		}

		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}
