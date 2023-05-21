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

    // ACCOUNT FILE
    public static bool IsExistsFile(string path, string fileName){
        try{
            return File.Exists(path + "/" + fileName);
        } catch (Exception) {return false;}
    }
    public static void CreateFile2(string path, string fileName){
        try{
            if(!IsExistsFile(path, fileName))
            File.Create(path + "/" + fileName);
        } catch (Exception) {
        }
    }
    public static ArrayList ReadFile2(string path, string fileName){
        ArrayList result = new ArrayList();
        try{
            if(!IsExistsFile(path, fileName)) CreateFile2(path, fileName);
            StreamReader sr = new StreamReader(path + "/" + fileName);
            string line = sr.ReadLine();
            while(line != null){
                result.Add(line);
                line = sr.ReadLine();
            }
            sr.Close();
        } catch(Exception){
            return result;
        }
        return result;
    }
    public static bool WriteFile2(string path, string fileName, bool append, ArrayList data){
        try{
            if(!IsExistsFile(path, fileName)) CreateFile2(path, fileName);
            StreamWriter sw = new StreamWriter(path + "/" + fileName, append);
            foreach(Account line in data){
                sw.WriteLine(line.ToLine());
            }
            sw.Close();
            return true;
        } catch (Exception){
            return false;
        }
    }
    public static bool WriteFilePlayerCoin(string path, string fileName, bool append, ArrayList data){
        try{
            if(!IsExistsFile(path, fileName)) CreateFile2(path, fileName);
            StreamWriter sw = new StreamWriter(path + "/" + fileName, append);
            foreach(PlayerCoin line in data){
                sw.WriteLine(line.ToLine());
            }
            sw.Close();
            return true;
        } catch (Exception){
            return false;
        }
    }
    public static bool WriteFile2(string path, string fileName, bool append, string data){
        try{
            if(!IsExistsFile(path, fileName)) CreateFile2(path, fileName);
            StreamWriter sw = new StreamWriter(path + "/" + fileName, append);
            sw.WriteLine(data);
            sw.Close();
            return true;
        } catch (Exception){
            return false;
        }
    }
    public static ArrayList GetAccountList(){
        ArrayList result = new ArrayList();
        ArrayList accounts = ReadFile2(Application.persistentDataPath, GameDefine.ACCOUNT_PLAYER_FILE);
        foreach(string account in accounts){
            result.Add(new Account(account.Split(':')[0].Trim(), account.Split(':')[1].Trim()));
        }
        return result;
    }
    public static bool CheckLogin(string username, string password){
        Account account = new Account(username, password);
        foreach(Account acc in GetAccountList()){
            if(acc.Equals(account)) return true;
        }
        return false;
    }
    public static bool IsExistsAccount(string username){
        ArrayList accounts = GetAccountList();
        foreach(Account acc in accounts){
            if(acc.username == username) return true;
        }
        return false;
    }
    public static bool AddAccount(string username, string password){
        if(IsExistsAccount(username)) return false;
        Account account = new Account(username, password);
        ArrayList data = new ArrayList();
        data.Add(account);
        return WriteFile2(Application.persistentDataPath, 
            GameDefine.ACCOUNT_PLAYER_FILE, 
            true,
            data);
    }

    // LOGIN STATE
    public static string GetUsernameLoged(){
        ArrayList data = ReadFile2(Application.persistentDataPath, GameDefine.LOGIN_STATE_FILE);
        if(data.Count != 0)
        return (string)data[0];
        return "";
    }
    public static void WriteLogin(string username){
        WriteFile2(Application.persistentDataPath, GameDefine.LOGIN_STATE_FILE, false, username);
    }
    public static void WriteLogout(){
        WriteFile2(Application.persistentDataPath, GameDefine.LOGIN_STATE_FILE, false, "");
    }

    // PLAYER COIN
    public static ArrayList GetPlayerCoinList(){
        ArrayList data = ReadFile2(Application.persistentDataPath, GameDefine.PALYER_COIN_FILE);
        ArrayList result = new ArrayList();
        foreach(string line in data){
            result.Add(new PlayerCoin(line.Split(':')[0].Trim(), 
                int.Parse(line.Split(':')[1].Trim())));
        }
        return result;
    }
    public static bool CheckExistsPlayerCoin(string username){
        foreach(PlayerCoin pc in GetPlayerCoinList()){
            if(pc.username == username) return true;
        }
        return false;
    }
    public static void UpdatePlayerCoin(string username, int coin){
        if(username == "" || username == null) return;
        if(!CheckExistsPlayerCoin(username)){
            WriteFile2(Application.persistentDataPath,
                GameDefine.PALYER_COIN_FILE,
                true,
                (new PlayerCoin(username, coin)).ToLine());
        } else {
            ArrayList pcl = GetPlayerCoinList();
            int index = -1;
            for(int i = 0; i < pcl.Count; i++){
                if(((PlayerCoin)pcl[i]).username == username) index = i;
                break;
            }
            if(index != -1){
                pcl[index] = new PlayerCoin(username, 
                    ((PlayerCoin)pcl[index]).coin + coin);

                WriteFilePlayerCoin(Application.persistentDataPath,
                    GameDefine.PALYER_COIN_FILE,
                    false,
                    pcl);
            }
        }
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
