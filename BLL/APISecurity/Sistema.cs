﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.APISecurity
{
    public class Sistema
    {
        public int IdSistema { get; set; }

        public string NombreSistema { get; set; }
    }
}
