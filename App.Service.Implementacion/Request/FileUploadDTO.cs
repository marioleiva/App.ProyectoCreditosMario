﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;



namespace App.Service.Implementacion.Request
{
    public class FileUploadDTO
    {
        public IFormFile File { get; set; } = null!;
    }
}
