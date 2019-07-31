using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageCompression.ViewModel
{
    public class SettingsVM
    {
          
        [Required(ErrorMessage = "Serwer SMTP nie może pozostać pusty")]
        [DataType(DataType.Text)]
        [Display(Name = "Serwer SMTP")] 
        public string smtpServer { get; set; }

        [Required(ErrorMessage = "Hasło nie może pozostać puste")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string password { get; set; }

        [Required(ErrorMessage = "Login do serwera SMTP nie może pozostać pusty")]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwa użytkownika SMTP")]
        public string smtpUserName { get; set; }

        [Required(ErrorMessage = "Port do serwera SMTP nie może pozostać pusty")]
        [DataType(DataType.Text)]
        [Display(Name = "Port serwera SMTP")]
        public int smtpPort { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Odbiorcy logów")]
        public string emailRecipients { get; set; }
    }
}
