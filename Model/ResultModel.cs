using Microsoft.EntityFrameworkCore;
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
            Stage1Result = (int)Math.Ceiling(Stage1Result / 16.0 * 100); //переводим в среднюю иточность, в процентах поменять на 160
        }

        public int Stage2Result { get; set; } = 0; //Лурия
        public void LuriaScore(int score)
        {
            if (score > Stage2Result) 
                Stage2Result = score;
        }

        public void SetDescription()
        {
            if (Stage1Result > 60 && Stage2Result > 6 && Stage3Result > 5 && Stage4Result > 3)
                Description = "По результатам тестирования сильные стороны это объём восприятия, устная и зрительная кратковременная память. Когнитивные функции находятся на высоком уровне. Слабые стороны не выявлены.";
            else if (Stage1Result < 50 && Stage2Result < 5 && Stage3Result < 4 && Stage4Result < 3)
                Description = "По результатам тестирования слабые стороны это объём восприятия и недостаточная кратковременная память (как визуальная, так и аудиальная). Общий уровень когнитивной нагрузки низкий. Рекомендуется пересказ прочитанного, диктанты, а также упражнения на слуховое и зрительное восприятие.";
            else if (Stage1Result < 50 && Stage2Result > 6 && Stage3Result > 5 && Stage4Result < 4)
                Description = "По результатам тестирования сильные стороны это кратковременная память, особенно аудиальная. Слабые — объём и скорость визуального восприятия. Для развития восприятия используйте флеш-карточки на время, быстрые зрительные диктанты, работу с визуальным материалом.";
            else if (Stage1Result > 60 && Stage2Result < 5 && Stage3Result < 4 && Stage4Result > 4)
                Description = "По результатам тестирования сильные стороны это визуальный захват информации, хороший объем и скорость восприятия. Слабые стороны: недостаточная кратковременная память, как слуховая, так и визуальная. Желательно работать над улучшением памяти с помощью повторения, ассоциаций и визуализации. Эффективны упражнения типа \"Найди пары\"";
            else
                Description = "По результатам тестирования выявлен средний уровень развития когнитивных функций. Объём восприятия и кратковременная память (как визуальная, так и слуховая) находятся в пределах нормы. Заметных сильных или слабых сторон не выявлено — функциональность мозга сбалансированна, но без выраженных преимуществ. Для улучшения когнитивной активности и профилактики возрастных изменений рекомендуется регулярно тренировать внимание и память.";
        }

        public void SetTotal()
        {
            TotalResult = Math.Ceiling((Stage1Result + (Stage2Result * 10) + (Stage3Result * 10) + (Stage4Result / 6.0 * 100) + Stage5Result) / 6);
        }

        public int Stage3Result { get; set; } = 0; //Диапазон цифр
        public int Stage4Result { get; set; } = 0; //Бернштейн
        public int Stage5Result { get; set; } = 0; //Матрица
        public double TotalResult { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

    }

    public class AppDbContext : DbContext
    {
        public DbSet<VkrResultsEntry> Results { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BakunevVKR;Username=postgres;Password=157");
        }
    }

    public class VkrResultsEntry
    {
        public int Id { get; set; } // будет автоинкрементироваться

        public string Name { get; set; }
        public int Age { get; set; }

        public double Stage1Result { get; set; }
        public double Stage2Result { get; set; }
        public double Stage3Result { get; set; }
        public double Stage4Result { get; set; }
        public double Stage5Result { get; set; }
        public double TotalResult { get; set; }
        public string Description { get; set; }
    }
}
