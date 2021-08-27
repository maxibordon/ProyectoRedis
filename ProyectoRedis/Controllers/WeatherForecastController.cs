using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller

    {
        private readonly IDistributedCache _distributedCache;
        private IConfiguration _configRoot;

        public WeatherForecastController(IDistributedCache distributedCache,IConfiguration configRoot)
        {
            _distributedCache = distributedCache;
            _configRoot = configRoot;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string suministros = null;
            string respuesta = string.Empty;
            var cacheKey = "listaSuministroPrueba";           
            var resultadoWS= await _distributedCache.GetAsync(cacheKey); //Recuperamos por clave sino lo encontramos invocamos servicio
            if (resultadoWS!=null)
            {
                suministros = Encoding.UTF8.GetString(resultadoWS); 
            }
            else
            {
                suministros = GetFromWs();
                int minutes = Convert.ToInt32(_configRoot["timeoutWS"]);  //Configuramos timeout por ventana deslizante           
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(minutes));
                byte[] data = Encoding.UTF8.GetBytes(suministros);
                await _distributedCache.SetAsync(cacheKey, data, options);              

            }
            return Ok(suministros);
 


        }

        private string GetFromWs()
        {
            string json = null;
            using (StreamReader r = new StreamReader("respuesta.json"))
            {
               json = r.ReadToEnd();
               
            }
            return json;
        } 



    }
}
