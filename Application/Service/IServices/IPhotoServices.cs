﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface IPhotoServices
    {
        Task<string> UploadPhoToAsync(IFormFile file);

    }
}
