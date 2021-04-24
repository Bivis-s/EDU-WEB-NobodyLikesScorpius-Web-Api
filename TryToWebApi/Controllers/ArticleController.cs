using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController
    {
        [HttpGet("details")]
        public IEnumerable<Article> Get(int range, int zodiac)
        {
            Console.WriteLine((ZodiacType) zodiac);
            return new Article[]
            {
                new ("Учёные нашли лучший знак зодиака!" + zodiac, "Кто-то сказал, что водолеи лучше всех, мы им верим)", ZodiacType.Aquarius),
                new ("Астрологи объявили неделю ебашилова" + zodiac, "Вас ждёт хорошая продуктивная следующая неделя", ZodiacType.Pisces)
            };
        }
    }
}