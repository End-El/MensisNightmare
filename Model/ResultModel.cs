using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR2025.Model
{
    public class ResultModel
    {
        public int Stage1Result { get; set; } = 0; //Тахистоскоп
        public void AddScore(int score)
        {
            Stage1Result += score; 
        }
        public void AverageScore()
        {
            Stage1Result = (int)Math.Ceiling(Stage1Result / 160.0 * 100); //переводим в среднюю иточность, в процентах
        }

        public int Stage2Result { get; set; } = 0; //Лурия
        public void LuriaScore(int score)
        {
            if (score > Stage2Result) 
                Stage2Result = score;
        }

        public int Stage3Result { get; set; } = 0; //Диапазон цифр
        public int Stage4Result { get; set; } = 0; //Бернштейн
        public int Stage5Result { get; set; } = 0; //Матрица
    }
}
