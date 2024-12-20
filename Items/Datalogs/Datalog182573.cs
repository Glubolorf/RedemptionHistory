using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog182573 : ModItem
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
			base.DisplayName.SetDefault("Data Log #182573");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Holy...]\n[c/aee6f3:After exploring Alkonost's surface, I've finally found something other than ice!]\n[c/aee6f3:Took so long since I can't last down there for more than half a minute.]\n[c/aee6f3:From the looks of things, it looks man-made. Or I guess alien-made... Hehe.]\n[c/aee6f3:First time I've felt this amused in forever, but anyways, the structure.]\n[c/aee6f3:It was under the thick ice sheet so I had to drill quite far down.]\n[c/aee6f3:The water under there must be freezing, but curiosity is getting the better of me here.]\n[c/aee6f3:I have found an entrance, inside is just as cold though, so I should go back up into my ship before exploring further.']");
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
