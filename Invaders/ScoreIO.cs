using System;
using System.IO;
using System.Xml.Serialization;

namespace Invaders
{
    /// <summary>
    ///  Class that handles IO operations for score.
    ///  This class uses large amounts of code from an old project written by me, Olle Astré, in 2018.
    ///  Said project can be found here: https://github.com/Azivast/Highscore-Sorter.
    /// </summary>
    public class ScoreIO
    {
        // Structure of the save data.
        [Serializable]
        public struct SaveData
        {
            public string[] PlayerName;
            public int[] Score;
            public int Count; // amount of entries in the arrays

            // Constructor creates arrays based on the count
            public SaveData(int count)
            {
                PlayerName = new string[count];
                Score = new int[count];
                Count = count;
            }
        }

        // Load data from file.
        public static SaveData LoadData(string filePath)
        {
            SaveData data = new SaveData();
            
            // If file does not exist create it
            if (!File.Exists(filePath))
            {
                data = new SaveData(10);
                
                // Save the data to the file and return.
                DoSave(data, filePath);
                return data;
            }

            // Open the file.
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                // Read the data from the file.
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                data = (SaveData)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file.
                stream.Close();
            }

            return (data);
        }

        // Save data to file.
        public static void DoSave(SaveData data, string filePath)
        {
            // Open the file or create it if it doesn't exist.
            FileStream stream = File.Open(filePath, FileMode.Create);
            try
            {
                // Convert to XML and try to open the stream.
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                serializer.Serialize(stream, data);
            }
            finally
            {
                // Close the file.
                stream.Close();
            }
        }

        // Sort the high score.
        public static void SaveAndSortHighScore(string file, int score, string name)
        {
            // Load the stored data.
            SaveData data = LoadData(file);


            // Sorting algorithm. TODO: Read up on sorting algorithms. This is bubble sort?
            int scoreIndex = -1;
            for (int i = 0; i < data.Count; i++)
            {
                if (score > data.Score[i])
                {
                    scoreIndex = i;
                    break;
                }
            }

            if (scoreIndex > -1)
            {
                // New high score found ... do swaps.
                for (int i = data.Count - 1; i > scoreIndex; i--)
                {
                    data.Score[i] = data.Score[i - 1];
                    data.PlayerName[i] = data.PlayerName[i - 1];
                }

                data.Score[scoreIndex] = score;
                data.PlayerName[scoreIndex] = name;

                DoSave(data, file);
            }
        }
    }
}