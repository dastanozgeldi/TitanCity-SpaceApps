using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Text.Json;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[Serializable]
public class AudioData
{
    public string audioContent;
}

public class TextToSpeechRequest : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private const string apiUrl = "https://texttospeech.googleapis.com/v1/text:synthesize";
    private const string apiKey = "AIzaSyDMRpuTcIR1NMIKf88zpGz3LkYr8CmSaXY";

    public IEnumerator GetAudioBytes(string chatText)
    {
        string jsonString = "{\n" +
                            "  \"audioConfig\": {\n" +
                            "    \"audioEncoding\": \"MP3\",\n" +
                            "    \"pitch\": 0,\n" +
                            "    \"speakingRate\": 1\n" +
                            "  },\n" +
                            "  \"input\": {\n" +
                            $"    \"text\": \"{chatText}\"\n" +
                            "  },\n" +
                            "  \"voice\": {\n" +
                            "    \"languageCode\": \"en-US\",\n" +
                            "    \"name\": \"en-US-Wavenet-D\"\n" +
                            "  }\n" +
                            "}";

        byte[] requestBody = Encoding.UTF8.GetBytes(jsonString);

        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(requestBody);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("X-Goog-Api-Key", apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;

            string audioContent = responseJson.Substring(21, responseJson.Length - 25);

            yield return StartCoroutine(ConvertBase64ToAudioClip(audioContent, _audioSource));
        }
        else Debug.LogError("Error: " + request.error);
    }

    IEnumerator ConvertBase64ToAudioClip(string base64EncodedMp3String, AudioSource audioSource)
    {
        var audioBytes = Convert.FromBase64String(base64EncodedMp3String);
        Debug.Log("Current Path " + Application.dataPath);
        var tempPath = Application.dataPath + "/Audios/tmpMP3Base64.mp3";
        File.WriteAllBytes(tempPath, audioBytes);

        WWW www = new WWW("file://" + tempPath);
        // Wait until the audio file is loaded
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            // Assign the loaded AudioClip to the AudioSource component
            var audioClip = www.GetAudioClip();
            // Now you can use the audioClip as you need, e.g., play it:
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else Debug.LogError("Error loading audio: " + www.error);
        // if (request.result.Equals(UnityWebRequest.Result.ConnectionError))
        //     Debug.LogError(request.error);
        // else
        // {
        //     audioSource.clip = DownloadHandlerAudioClip.GetContent(request);
        //     audioSource.Play();
        //     Debug.Log("Audio clip played");
        // }

        // File.Delete(tempPath);
    }


}
