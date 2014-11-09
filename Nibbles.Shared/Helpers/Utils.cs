using System;
using CocosSharp;

namespace Nibbles.Shared.Helpers
{
	public static  class Utils
	{
		public static CCPoint GetRandomPosition (CCSize spriteSize, CCSize visibleBoundsWorldspaceSize)
		{
			var randomX = CCRandom.Next (40, (int)visibleBoundsWorldspaceSize.Width - 40);
			var randomY = CCRandom.Next (40, (int)visibleBoundsWorldspaceSize.Height - 40);



			return new CCPoint ((float)randomX, (float)randomY);
		}
	}
}

