using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog999735 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Datalogs/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #999735");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'I haven't done one of these in forever, but I should explain what happened.]\n[c/aee6f3:I was travelling to the next solar system, when suddenly some wormhole appeared.]\n[c/aee6f3:The SoS couldn't turn fast enough so it got sucked in. Wormholes are like portals of the universe,]\n[c/aee6f3:so I expected to just reach the end instantly, but no, I was stuck in the wormhole for almost 1000 years.]\n[c/aee6f3:God it was boring, but I had the androids to keep me company. Unfortunately, I don't know where I am now.]\n[c/aee6f3:I can't tell how far away I am from home, but I see a nearby star, so hopefully there's some planets.']");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 9;
		}
	}
}
