using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR2025.Model
{
    public class ResultModel
    {
        public int Stage1Result { get; set; } = 0;
        public void AddScore(int score)
        {
            Stage1Result += score;
        }
    }
}
