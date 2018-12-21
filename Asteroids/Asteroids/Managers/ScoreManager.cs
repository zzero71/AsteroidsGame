﻿using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Asteroids
{
    public class ScoreManager
    {
        private static string FileName = "scores.xml"; 

        public List<Score> Highscores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager()
          : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Value).ToList(); 

            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            if (!File.Exists(FileName))
                return new ScoreManager();

            using (var reader = new StreamReader(new FileStream(FileName, FileMode.Open)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                var scores = (List<Score>)serilizer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            Highscores = Scores.Take(5).ToList();
        }

        public static void Save(ScoreManager scoreManager)
        {
            using (var writer = new StreamWriter(new FileStream(FileName, FileMode.Create)))
            {
                var serilizer = new XmlSerializer(typeof(List<Score>));

                serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
