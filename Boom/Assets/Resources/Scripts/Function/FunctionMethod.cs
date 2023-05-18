using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class FunctionMethod : MonoBehaviour
{
    public static Vector2 GetRelativePositionRandom(){
        int maxX = (int)GameDefine.X_MAX;
        int maxY = (int)GameDefine.Y_MAX;
        int minX = (int)GameDefine.X_MIN;
        int minY = (int)GameDefine.Y_MIN;
        int x = UnityEngine.Random.Range(0, maxX - minX + 1) + minX;
        int y = UnityEngine.Random.Range(0, maxY - minY + 1) + minY;
        return new Vector2(x, y);
    }

    public static IEnumerator EffectChangeScene(string pathScene, float effectTime){
        yield return new WaitForSeconds(effectTime);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(pathScene);
        while(!loadScene.isDone){
            yield return null;
        }
    }

    public static ArrayList ReadFile(string fileName){
        string fullPath = Application.persistentDataPath + "/" + fileName;
        ArrayList result = new ArrayList();
        string line;

        try{
            if(!File.Exists(fullPath)){
                CreateFile(Application.persistentDataPath, fileName);
                ArrayList lines = new ArrayList();
                lines.Add("SoundVolume : 1");
                lines.Add("MusicVolume : 1");
                WriteFile(fileName, lines, false);
            }

            StreamReader sr = new StreamReader(fullPath);
            line = sr.ReadLine();
            while(line != null){
                result.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
        } catch (Exception) {
            CreateFile(Application.persistentDataPath, fileName);
            ArrayList lines = new ArrayList();
            lines.Add("SoundVolume : 1");
            lines.Add("MusicVolume : 1");
            WriteFile(fileName, lines, false);
        }
        return result;
    }

    public static void WriteFile(string fileName, ArrayList lines, bool append){
        string fullPath = Application.persistentDataPath + "/" + fileName;
        try{
            StreamWriter sw = new StreamWriter(fullPath, append);
            foreach(string line in lines){
                sw.WriteLine(line);
            }
            sw.Close();
        } catch (Exception){
            CreateFile(Application.persistentDataPath, fileName);
            WriteFile(fileName, lines, append);
        }
    }

    // CREATE DIRECTORY
    public static void CreateDirectory(string path){
        try{
            if(Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        } catch(Exception){}
    }
    
    // CREATE FILE
    public static void CreateFile(string path, string fileName){
        string fullPath = path + "/" + fileName;
        try{
            if(File.Exists(fullPath)) return;
            File.Create(fullPath);
        } catch(Exception){}
    }

    // AUDIO
    public static ArrayList GetAudioVolumeData(){
        return ReadFile(GameDefine.SYSTEM_FILE_NAME);
    }
    public static float GetSoundVolume(){
        return float.Parse(((string)GetAudioVolumeData()[0]).Split(':')[1].Trim());
    }
    public static float GetMusicVolume(){
        return float.Parse(((string)GetAudioVolumeData()[1]).Split(':')[1].Trim());
    }

    // TIME
    public static string GetTime(int timer){
        int minute = timer / 60;
        int seconds = timer % 60;
        return ((minute < 10) ? "0" + minute : "" + minute)
            + ":" + ((seconds < 10) ? "0" + seconds : "" + seconds);
    }

    // CREATE GAME OBJECT
    public static GameObject CreateEffect(string effectName){
        return (GameObject)Instantiate(Resources.Load("Prefabs/Effect/"+effectName));
    }
}
