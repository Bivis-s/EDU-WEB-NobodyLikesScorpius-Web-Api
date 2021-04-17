using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TryToWebApi.objects;

namespace TryToWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController
    {
        [HttpGet]
        public IEnumerable<Article> Get()
        {
            return new Article[]
            {
                new ("Учёные нашли лучший знак зодиака!", "Кто-то сказал, что водолеи лучше всех, мы им верим)", ZodiacType.Aquarius),
                new ("Астрологи объявили неделю ебашилова", "Вас ждёт хорошая продуктивная следующая неделя", ZodiacType.Pisces)
            };
        }
    }
}