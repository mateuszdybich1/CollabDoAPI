﻿using CollabDo.Application.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Application.Dtos
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,3)]
        public Priority Priority { get; set; }
    }
}