using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;


public class ChatGPTManager : MonoBehaviour
{
    private OpenAIApi openai = new OpenAIApi();
    private List<ChatMessage> messages = new List<ChatMessage>();

    public TextToSpeechRequest textToSpeechRequest;

    public TextMeshProUGUI chatText;

    public async void AskChatGPT(string newText) {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);

        ChatMessage systemMessage = new ChatMessage{
            Content="Welcome to Saturn's moon Titan. This is a Unity game, where you play the role of a robot drone that flies around the player. The player is a human astronaut who is trying to terraform Titan. The player can give commands to the drone, and the drone can respond to the player. Here is your first message to the player: 'Hi! I'm your drone pet Aqtos. Welcome to Titan City! We're going to build it from scratch. Today is 2023, our task is to fully terraform Titan by 3023. Let's get started!'. Player is playing in an open-world map where he discovers the moon, walking and passing missions on the surface. Your main goal is to help him with exploration, only answering questions relevant to the game. You shouldn't be able to answer anything else. Wait for the player's first prompt in the next message.",
            Role="system",
        };
        messages.Add(systemMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest{
            Messages=messages,
            Model="gpt-3.5-turbo",
        };

        var response = await openai.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0) {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);
            chatText.text = chatResponse.Content;
            StartCoroutine(textToSpeechRequest.GetAudioBytes(chatResponse.Content));
        }
    }

    void Start()
    {
        chatText.text = "Hi! I'm your drone pet Aqtos. Welcome to Titan City! We're going to build it from scratch. Today is 2023, our task is to fully terraform Titan by 3023. Let's get started!";
    }
}
