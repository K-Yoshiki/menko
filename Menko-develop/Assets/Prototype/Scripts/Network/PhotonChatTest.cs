using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MenkoiMonster.Network;

public class PhotonChatTest : MonoBehaviour
{
	public RectTransform contentRoot;
	public Text contentTemplate;
	public InputField inputField;
	public List<Text> contents;

	void Awake()
	{
		PhotonManager.Instance.AddRPCEvent("ThrowMessage", ThrowMessage);
	}

	void OnDestory()
	{
		PhotonManager.Instance.RemoveRPCEvent("ThrowMessage");
	}

	public void Send()
	{
		if (!string.IsNullOrEmpty(inputField.text))
		{
			PhotonManager.Instance.SendRPC("ThrowMessage", PhotonTargets.All, inputField.text);
			inputField.text = "";
		}
	}

	void ThrowMessage(object[] parameters)
	{
		var messageStr = parameters[0] as string;
		var content = Instantiate(contentTemplate);
		content.text = messageStr;
		contents.Add(content);
		var contentTransform = content.transform;
		contentTransform.SetParent(contentRoot);
		contentTransform.localScale = Vector3.one;
	}
}