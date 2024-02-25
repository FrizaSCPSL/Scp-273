using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;

namespace Scp273
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        
        public bool Debug { get; set; } = false;
        public string Hint1 { get; set; } = "SCP-273 - человек-феникс. При смерти вы возраждаетесь спустя 20 секунд.";
        public string Hint2 { get; set; } = "Вы восстали из пепла.";
        
        public string Prefix { get; set; } = "Scp-273";
        
        public string Cinfo { get; set; } = "Человек-Феникс.";
        
    }
}