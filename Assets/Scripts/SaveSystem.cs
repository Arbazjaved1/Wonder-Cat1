using System;
using UnityEngine;

public static class SaveSystem
{
	public static void DeleteFile()
	{
		PlayerPrefs.DeleteAll();
	}

	public static bool KeyExists(string key)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return true;
		}
		return false;
	}

	public static void Save(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
	}

	public static int Load(string key, int Defaultvalue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return PlayerPrefs.GetInt(key);
		}
		Save(key, Defaultvalue);
		return Defaultvalue;
	}

	public static void Save(string key, bool value)
	{
		if (value)
		{
			PlayerPrefs.SetInt(key, 1);
		}
		else
		{
			PlayerPrefs.SetInt(key, 0);
		}
	}

	public static bool Load(string key, bool Defaultvalue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			if (PlayerPrefs.GetInt(key) == 1)
			{
				return true;
			}
			return false;
		}
		Save(key, Defaultvalue);
		return Defaultvalue;
	}

	public static void Save(string key, float value)
	{
		PlayerPrefs.SetFloat(key, value);
	}

	public static float Load(string key, float Defaultvalue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return PlayerPrefs.GetFloat(key);
		}
		Save(key, Defaultvalue);
		return Defaultvalue;
	}

	public static void Save(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
	}

	public static string Load(string key, string Defaultvalue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return PlayerPrefs.GetString(key);
		}
		Save(key, Defaultvalue);
		return Defaultvalue;
	}

	public static void Save(string key, Color value)
	{
		PlayerPrefsX.SetColor(key, value);
	}

	public static Color Load(string key, Color Defaultvalue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			Defaultvalue = PlayerPrefsX.GetColor(key);
			return Defaultvalue;
		}
		Save(key, Defaultvalue);
		return Defaultvalue;
	}

	public static void Save(string key, Color[] value)
	{
		PlayerPrefsX.SetColorArray(key, value);
	}

	public static Color[] Load(string key, Color[] Defaultvalue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			Defaultvalue = PlayerPrefsX.GetColorArray(key);
			return Defaultvalue;
		}
		Save(key, Defaultvalue);
		return Defaultvalue;
	}

	public static void Save(string key, DateTime value)
	{
		Save(key, value.ToBinary().ToString());
	}

	public static DateTime Load(string key, DateTime Defaultvalue)
	{
		if (PlayerPrefs.HasKey(key))
		{
			return DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString(key)));
		}
		Save(key, Defaultvalue);
		return Defaultvalue;
	}
}
