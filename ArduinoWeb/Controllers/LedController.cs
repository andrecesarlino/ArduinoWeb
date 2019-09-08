using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArduinoWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Ports;

namespace ArduinoWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedController : ControllerBase
    {
        public static List<Led> lista = new List<Led>();
        SerialPort Porta = new SerialPort("COM3", 9600);

        [AcceptVerbs("GET")]
        public List<Led> GetArduinos()
        {
            return lista;
        }

        [AcceptVerbs("POST")]
        public string PostLed(Led led)
        {
            Porta.Open();
            String teste = Convert.ToString(led.led);
            
            if (teste == "a")
            {
                Porta.Write(teste);
                lista.Add(led);
                Porta.Close();
                return "Ligado";
            }else if (teste == "b")
            {
                Porta.Write(teste);
                lista.Add(led);
                Porta.Close();
                return "Desligado"; 
            } else
            {
                return "Valor inválido";
            }
        }

        [AcceptVerbs("PUT")]
        public string PutLed(Led led)
        {
            Porta.Open();
            String teste = Convert.ToString(led.led);
            
            Porta.Write(teste);
            lista.Where(l => l.id == led.id).Select(o => { o.led = led.led; return o; }).ToList();
            Porta.Close();
            
            if(teste == "a")
            {
                return "Alterado para '" + led.led + "' ligado";
            } else
            {
                return "Alterado para '" + led.led + "' desligado";
            }

        }
    }
}