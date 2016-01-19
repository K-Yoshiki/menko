using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AppUtils
{
	/// <summary>
	/// Unity用のXMLヘルパークラス
	/// </summary>
	public static class XmlSerializeHelper
	{
		/// <summary>
		/// XML形式のStringをデシリアライズします
		/// </summary>
		public static T DeserializeFromString<T>(string xmlText) where T : class
		{
			T result;
			var serializer = new XmlSerializer(typeof(T));

			// 読み込み時の設定.
			var settings = DefaultReaderSettings;

			// Stringからクラスへのデシリアライズ.
			using (var textReader = new StringReader(xmlText))
			{
				using (var xmlReader = XmlReader.Create(textReader, settings))
				{
					result = serializer.Deserialize(xmlReader) as T;
				}
			}
			return result ?? default(T);
		}

		/// <summary>
		/// XML形式のByte配列をデシリアライズします
		/// </summary>
		public static T DeserializeFromByte<T>(ref byte[] xmlBytes) where T : class
		{
			T result;
			var serialzer = new XmlSerializer(typeof(T));

			// 読み込み時の設定
			var settings = DefaultReaderSettings;

			// Byte配列からクラスへのデシリアライズ
			using (var stream = new MemoryStream(xmlBytes))
			{
				using (var xmlReader = XmlReader.Create(stream, settings))
				{
					result = serialzer.Deserialize(xmlReader) as T;
				}
			}
			return result ?? default(T);
		}

		/// <summary>
		/// シリアライズ可能なオブジェクトをXML形式のStringに変換します.
		/// </summary>
		public static void SerializeToString<T>(T obj, out string result) where T : class
		{
			var sb = new StringBuilder();
			var serializer = new XmlSerializer(typeof(T));
			
			// 書き込み時の設定.
			var settings = DefaultWriterSettings;
			
			// オブジェクトからStringへのシリアライズ.
			using (var textWriter = new StringWriter(sb))
			{
				using (var xmlWriter = XmlWriter.Create(textWriter, settings))
				{
					serializer.Serialize(xmlWriter, obj);
				}
			}
			result = sb.ToString();
		}

		/// <summary>
		/// シリアライズ可能なオブジェクトをXML形式のByte配列に変換します.
		/// </summary>
		public static void SerializeToByte<T>(T obj, out byte[] result) where T : class
		{
			var serializer = new XmlSerializer(typeof(T));

			// 書き込みの設定.
			var settings = DefaultWriterSettings;

			// オブジェクトからbyte配列へのシリアライズ
			using (var stream = new MemoryStream())
			{
				using (var xmlWriter = XmlWriter.Create(stream, settings))
				{
					serializer.Serialize(xmlWriter, obj);
				}
				result = stream.GetBuffer();
			}
		}

		/// <summary>
		/// デフォルトのXmlWriterSettingを取得.
		/// </summary>
		static XmlWriterSettings DefaultWriterSettings
		{
			get { return new XmlWriterSettings() { Encoding = Encoding.UTF8, Indent = false, OmitXmlDeclaration = false }; }
		}

		/// <summary>
		/// デフォルトのXmlReaderSettingsを取得.
		/// </summary>
		static XmlReaderSettings DefaultReaderSettings
		{
			get{ return new XmlReaderSettings() {}; }
		}
	}
}