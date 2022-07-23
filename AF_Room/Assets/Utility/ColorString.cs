using UnityEngine;

public static class ColorString
{

	// Return the message, surrounded by RTF/HTML color tags.
	// this is suitable for use in Debug.Log, or other places rich text is supported.
	//
	// this call: Colorize("message", Color.red)
	// returns: <color=#ff000000>message</color>
	public static string Colorize(string text, Color color)
	{	
		return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + text + "</color>";
	}

	// Strings that begin with '#' will be parsed as hexadecimal in the following way:
	// # RGB (becomes RRGGBB)
	// # RRGGBB
	// # RGBA (becomes RRGGBBAA)
	// # RRGGBBAA
	// Strings that do not begin with '#' will be parsed as literal colors, with the following supported:
	// red, cyan, blue, darkblue, lightblue, purple, yellow, lime, fuchsia, white, silver, grey, black, orange, brown, maroon, green, olive, navy, teal, aqua, magenta
	// see: https://docs.unity3d.com/ScriptReference/ColorUtility.TryParseHtmlString.html
	public static string Colorize(string text, string htmlString)
	{
		Color parsedColor;
		if (!ColorUtility.TryParseHtmlString(htmlString, out parsedColor))
			parsedColor = Color.red;

		return Colorize(text, parsedColor);
	}
}
